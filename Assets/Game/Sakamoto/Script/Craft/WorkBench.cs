using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkBench : MonoBehaviour, IAddItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("アイテムの製造時間")]
    [SerializeField] float _waitSeconds;
    [Header("加工が始まるまでの猶予時間")]
    [SerializeField] float _craftStartTime = 10;

    [Tooltip("加工中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemData _itemData;
    [Tooltip("合成後のアイテム")]
    ItemData _resultSynthetic;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;

    void Start()
    {

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
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        if (_manufactureing && !item.Craft) return item;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic != null && item == null)
        {
            //アイテムがNullになった時アイテムを返す
            StopCoroutine(_runawayCoroutine);
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return item;
        }


        //アイテムがマシンの許与量を超えていたらアイテムを返す
        if (_itemData != null)
        {
            Debug.Log("アイテムを返すマス");
            return item;
        }
        //アイテムがNullではないとき
        if (_itemData == null)
        {
            Debug.Log("入ってきた");
            //アイテムが入ったことでクラフト待機スタート
            _itemData = item;
            StandbyCraft();
            return null;
        }

        return item;

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
    /// 加工が開始されたら製造完了まで待機する
    /// </summary>
    /// <returns></returns>
    IEnumerator ManufactureDeley()
    {

        Debug.Log("製造中wait");
        yield return new WaitForSeconds(_waitSeconds);
        Debug.Log("製造中終わり");
        _manufactureing = false;

    }


    /// <summary>
    /// アイテムを加工するメソッド
    /// </summary>
    void ItemManufacture()
    {
    
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i].Item1 == _itemData.ItemName)
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = _itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0];
                //Debug.Log($"結果は{_resultSynthetic}");
                break;
            }
            //Debug.Log($"結果判定中:アイテム１{itemArray[0]}、アイテム２:{itemArray[1]}、アイテム３:{itemArray[2]}");
        }

        _itemData = null;
    }

}
