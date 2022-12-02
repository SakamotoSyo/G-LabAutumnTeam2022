using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("時間経過によって変わる製造所のSprite")]
    [SerializeField] Sprite[] _millSprite = new Sprite[4];
    [Header("熱暴走時のSprite")]
    [SerializeField] Sprite _thermalRunawaySprite;
    [Header("製造所のSpriteRenderer")]
    [SerializeField] SpriteRenderer _millRenderer;
    [Header("アイテムの製造時間")]
    [SerializeField] float _waitSeconds;
    [Header("何秒たったら熱暴走で爆発するか")]
    [SerializeField] float _runawayTime = 5;
    [Header("製造が始まるまでの猶予時間")]
    [SerializeField] float _craftStartTime = 10;

    [Tooltip("製造中かどうか")]
    bool _manufactureing;
    [Tooltip("上質なものかどうか")]
    bool _isFineQuality;
    [Tooltip("上質なものが回収できる時間")]
    readonly float _fineQualityTime = 5;
    [Tooltip("アイテムを保存しておく変数")]
    ItemInformation[] _itemArray;
    [Tooltip("合成後のアイテム")]
    ItemInformation _resultSynthetic = new ItemInformation (null, false);
    [Tooltip("製造機が保存できるアイテムの数")]
    readonly int _itemSaveNum = 3;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;
    Coroutine _fineQualityCor;

    void Start()
    {
        _itemArray = new ItemInformation[_itemSaveNum];

        for (int i = 0; i < _itemSaveNum; i++) 
        {
            _itemArray[i] = new ItemInformation(null, false);
        }
    }


    void Update()
    {
        ////テスト用後で消す
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartManufacture();
        //}
    }

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="itemInfo"></param>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        if (_manufactureing) return itemInfo;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic.Item != null && itemInfo == null)
        {
            //アイテムがNullになった時アイテムを返す
            StopCoroutine(_runawayCoroutine);
            StopCoroutine(_fineQualityCor);
            if (_isFineQuality) 
            {
                Debug.Log("上質です");
                _resultSynthetic.SetFineQuality(true);
            }
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _millRenderer.sprite = _millSprite[0];
            return returnItem;
        }
        else if (_resultSynthetic.Item != null)
        {
            return itemInfo;
        }


        //アイテムがマシンの許与量を超えていたらアイテムを返す
        if (_itemArray[_itemSaveNum - 1].Item != null)
        {
            Debug.Log("アイテムを返すマス");
            return itemInfo;
        }
        //アイテムがNullではないとき
        if (itemInfo != null)
        {
            Debug.Log("入ってきた");
            //アイテムが入ってないところに入れる
            for (int i = 0; i < _itemSaveNum; i++)
            {
                if (_itemArray[i].Item == null)
                {
                    _itemArray[i] = itemInfo;
                    //アイテムが入ったことでクラフト待機スタート
                    StandbyCraft();
                    break;
                }
            }
            return null;
        }

        return itemInfo;

    }


    #region 製造待機コルーチン
    /// <summary>
    /// アイテムが入ったことで呼ばれるコルーチン開始関数
    /// </summary>
    void StandbyCraft()
    {
        if (_startCoroutine != null)
        {
            StopCoroutine(_startCoroutine);
            Debug.Log("コルーチンとめた");
            _startCoroutine = StartCoroutine(StandbyCraftCor());
        }
        else
        {
            _startCoroutine = StartCoroutine(StandbyCraftCor());
        }
    }

    /// <summary>
    /// クラフトがスタートするために待機時間
    /// </summary>
    /// <returns></returns>
    IEnumerator StandbyCraftCor()
    {
        yield return new WaitForSeconds(_craftStartTime);
        Debug.Log("クラフトスタート");
        //途中でCoroutineが中断されなかったらCraft開始
        ItemManufacture();
        //熱暴走待機開始
        _runawayCoroutine = StartCoroutine(ManufactureDeley());
        _manufactureing = true;
    }
    #endregion

    /// <summary>
    /// 製造が開始されたら製造完了まで待機する
    /// </summary>
    /// <returns></returns>
    IEnumerator ManufactureDeley()
    {

        Debug.Log("製造中wait");
        //時間経過によって製造所のSpriteを変える
        for (int i = 0; i < _millSprite.Length; i++) 
        {
            yield return new WaitForSeconds(_waitSeconds/_millSprite.Length);
            _millRenderer.sprite = _millSprite[i]; 
        }
        
        Debug.Log("製造中終わり");
        //var cor = FineQualityTime();
        _fineQualityCor = StartCoroutine(FineQualityTime());
        StartCoroutine(ThermalRunaway());
        _manufactureing = false;

    }


    /// <summary>
    /// アイテムを製造するメソッド
    /// </summary>
    void ItemManufacture()
    {
        //合成用の配列を作る
        string[] itemArray = new string[_itemSaveNum];
        for (int i = 0; i < _itemSaveNum; i++)
        {
            if (_itemArray[i].Item != null)
            {
                itemArray[i] = _itemArray[i].Item.ItemName;
            }
            else
            {
                itemArray[i] = "なし";
            }
        }

        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i].Item1.Contains(itemArray[0][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[1][0]))
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                _resultSynthetic.SetParts(PartsCheck(itemArray));
                //Debug.Log($"結果は{_resultSynthetic}");
                break;
            }
            //Debug.Log($"結果判定中:アイテム１{itemArray[0]}、アイテム２:{itemArray[1]}、アイテム３:{itemArray[2]}");
        }
        Array.Clear(_itemArray, 0, _itemArray.Length);
    }


    /// <summary>
    /// 上質なものがゲットできる時間
    /// </summary>
    /// <returns></returns>
    private IEnumerator FineQualityTime() 
    {
        _isFineQuality = true;
        yield return new WaitForSeconds(_fineQualityTime);
        _isFineQuality = false;
    }

    IEnumerator ThermalRunaway() 
    {
        Debug.Log("暴走待機");
        yield return new WaitForSeconds(_runawayTime);
        _millRenderer.sprite = _thermalRunawaySprite;
        Debug.Log("暴走");
    }


    /// <summary>
    /// 部品がいくつ含まれているか調べる
    /// </summary>
    /// <param name="itemNameArray"></param>
    /// <returns></returns>
    private int PartsCheck(string[] itemNameArray) 
    {
        int partsNum = 0;
        for (int i = 0; i < 2; i++) 
        {
            if (itemNameArray[i].Contains("部品")) 
            {
                partsNum++;
            }
        }

        return partsNum;
    }

}


////アイテムがマシンの許与量を超えていたらfalseを返す
//if (_itemList.Count >= _itemSaveNum)
//{
//    return item;
//}

////受け取ることができる状態ならListに格納してtrueを返す
////合成アイテムが入っているかつPlayerがアイテムを持っているときは通らない
//if (_resultSynthetic == null || item == null)
//{
//    //製造中は入ってこない
//    if (_manufactureBool && item == null)
//    {
//        _itemList.Add(item);
//        return null;
//    }
//    //StartManufacture(item);
//    //一時的にローカル変数にアイテムの情報を渡す
//    var Item = _resultSynthetic;
//    _resultSynthetic = null;
//    return Item;
//}

//return item;
