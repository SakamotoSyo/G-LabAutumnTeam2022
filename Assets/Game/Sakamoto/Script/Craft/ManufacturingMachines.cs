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
    [Header("火の粉のPrefab")]
    [SerializeField] GameObject _firePrefab;
    [Header("火の粉が飛び散る範囲")]
    [SerializeField] Transform[] _fireTransform = new Transform[1];
    [Header("時間経過によって変わる製造所のSprite")]
    [SerializeField] Sprite[] _millSprite = new Sprite[4];
    [Header("通常時のSprite")]
    [SerializeField] Sprite _standardSprite;
    [Header("熱暴走時のSprite")]
    [SerializeField] Sprite _thermalRunawaySprite;
    [Header("製造所のSpriteRenderer")]
    [SerializeField] SpriteRenderer _millRenderer;
    [Header("アイテムの製造時間")]
    [SerializeField] float _waitSeconds;
    [Header("製造が始まるまでの猶予時間")]
    [SerializeField] float _craftStartTime = 10;
    [Header("EffectのAnimator")]
    [SerializeField] Animator _effectAnim;
    [Header("EffectのSubAnimator")]
    [SerializeField] Animator _subEffectAnim;

    [Tooltip("熱暴走する時間")]
    float _runawayTime = 5;
    [Tooltip("アイテムを入れたりとったり出来るか")]
    bool _isLook;
    [Tooltip("上質なものかどうか")]
    bool _isFineQuality;
    [Tooltip("上質なものが回収できる時間")]
    readonly float _fineQualityTime = 5;
    [Tooltip("爆発するまでの時間")]
    readonly int _explosionTime = 5;
    [Tooltip("爆発にかける時間")]
    readonly int _explosionEndTime = 3;
    [Tooltip("修理が終わるまでの時間")]
    readonly int _repairTime = 30;
    [Tooltip("アイテムを保存しておく変数")]
    ItemInformation[] _itemArray;
    [Tooltip("合成後のアイテム")]
    ItemInformation _resultSynthetic = new ItemInformation(null, false);
    [Tooltip("製造機が保存できるアイテムの数")]
    readonly int _itemSaveNum = 2;
    Coroutine _startCoroutine;
    Coroutine _craftStartCoroutine;
    Coroutine _fineQualityCor;
    Coroutine _runawayCoroutine;
    Coroutine _explosionCoroutine;

    void Start()
    {
        _itemArray = new ItemInformation[_itemSaveNum];
        ArrayInit();
    }

    private void ArrayInit()
    {
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
        if (_isLook) return itemInfo;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic.Item != null && itemInfo == null)
        {
            //アイテムがNullになった時アイテムを返す
            //不必要なコルーチンを止める
            CraftStopCor();

            if (_isFineQuality)
            {
                Debug.Log("上質です");
                _resultSynthetic.SetFineQuality(true);
            }
            var returnItem = _resultSynthetic;
            _resultSynthetic = new ItemInformation(null, false);
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
        if (itemInfo == null)
        {
            return itemInfo;
        }
        if (itemInfo.Item.Craft) 
        {
            Debug.Log("入ってきた");
            //アイテムが入ってないところに入れる
            for (int i = 0; i < _itemSaveNum; i++)
            {
                if (_itemArray[i].Item == null)
                {
                    if (i == 0)
                    {
                        _runawayTime = itemInfo.Item.RunawayTime;
                        Debug.Log(itemInfo.Item.RunawayTime);
                    }
                    else 
                    {
                        _runawayTime += itemInfo.Item.RunawayTime;
                        Debug.Log(_runawayTime);
                    }
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
        _effectAnim.SetBool("Craft", true);
        _subEffectAnim.SetBool("Thunder", true);
        //クラフト開始
        _craftStartCoroutine = StartCoroutine(ManufactureDeley());
        _isLook = true;
    }
    #endregion

    /// <summary>
    /// 製造が開始されたら製造完了まで待機する
    /// </summary>
    /// <returns></returns>
    IEnumerator ManufactureDeley()
    {
        //時間経過によって製造所のSpriteを変える
        for (int i = 0; i < _millSprite.Length; i++)
        {
            yield return new WaitForSeconds(_waitSeconds / _millSprite.Length);
            //音を出す
            if (i == _millSprite.Length - 1)
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_lastlamp);
            }
            else
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_lamp);
            }

            _millRenderer.sprite = _millSprite[i];
        }

        _effectAnim.SetBool("Craft", false);
        _subEffectAnim.SetBool("Thunder", false);
        //var cor = FineQualityTime();
        //途中でCoroutineが中断されなかったらCraft開始
        ItemManufacture();
        _isLook = false;
        _craftStartCoroutine = null;
        _runawayCoroutine = StartCoroutine(ThermalRunaway());
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
            if (_syntheticData.SyntheticList[i].Item1.Contains(itemArray[0][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[1][0]) || _syntheticData.SyntheticList[i].Item1.Contains(itemArray[1][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[0][0]))
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                Debug.Log(_syntheticData.SyntheticList[i].ResultItem);
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                _resultSynthetic.SetParts(PartsCheck(itemArray));
                if (_resultSynthetic.PartsNum == _resultSynthetic.Item.ItemParts) 
                {
                    _fineQualityCor = StartCoroutine(FineQualityTime());
                }
                //Debug.Log($"結果は{_resultSynthetic}");
                break;
            }
            //Debug.Log($"結果判定中:アイテム１{itemArray[0]}、アイテム２:{itemArray[1]}、アイテム３:{itemArray[2]}");
        }
        ArrayInit();
    }


    /// <summary>
    /// 上質なものがゲットできる時間
    /// </summary>
    /// <returns></returns>
    private IEnumerator FineQualityTime()
    {
        Debug.Log("上質");
        _isFineQuality = true;
        yield return new WaitForSeconds(_fineQualityTime);
        _isFineQuality = false;
    }

    /// <summary>
    /// 暴走するまでの時間
    /// </summary>
    /// <returns></returns>
    IEnumerator ThermalRunaway()
    {
        yield return new WaitForSeconds(5);
        _millRenderer.sprite = _thermalRunawaySprite;
        _effectAnim.SetBool("RunAway", true);
        yield return new WaitForSeconds(_runawayTime);
        _explosionCoroutine = StartCoroutine(Explosion());
    }

    /// <summary>
    /// 爆発までの時間
    /// </summary>
    /// <returns></returns>
    IEnumerator Explosion()
    {
        _isLook = true;
        Debug.Log("爆発");
        FireGeneration();
        _resultSynthetic = new ItemInformation(null, false);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_explosion);
        _effectAnim.SetBool("RunAway", false);
        _effectAnim.SetBool("Explosion", true);
        yield return new WaitForSeconds(_explosionTime);
        StartCoroutine(RepairTime());
    }

    IEnumerator RepairTime()
    {
        _effectAnim.SetBool("Explosion", false);
        _effectAnim.SetBool("Repair", true);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_repair);
        yield return new WaitForSeconds(_repairTime);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_repair_end);
        _isLook = false;
        _effectAnim.SetBool("Repair", false);
        _millRenderer.sprite = _standardSprite;
    }

    /// <summary>
    /// 火の粉を生成する
    /// </summary>
    private void FireGeneration() 
    {
        for (int i = 0; i < 2; i++) 
        {
            var x = UnityEngine.Random.Range(_fireTransform[0].position.x, _fireTransform[1].position.x);
            var y = UnityEngine.Random.Range(_fireTransform[0].position.y, _fireTransform[1].position.y);
            Instantiate(_firePrefab, new Vector2(x, y), gameObject.transform.rotation);
        }
       
    }

    private void CraftStopCor()
    {
        if (_craftStartCoroutine != null)
        {
            StopCoroutine(_craftStartCoroutine);
        }

        if (_runawayCoroutine != null)
        {
            StopCoroutine(_runawayCoroutine);
            Debug.Log("暴走止めた");
            _effectAnim.SetBool("RunAway", false);
            _millRenderer.sprite = _standardSprite;
        }

        if (_explosionCoroutine != null)
        {
            StopCoroutine(_explosionCoroutine);
            _effectAnim.SetBool("RunAway", false);
            _millRenderer.sprite = _standardSprite;
        }

        if (_fineQualityCor != null)
        {
            StopCoroutine(_fineQualityCor);
        }

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
