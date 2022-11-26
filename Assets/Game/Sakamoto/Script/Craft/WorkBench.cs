using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkBench : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("アイテムの製造時間")]
    [SerializeField] float _craftTime;
    [Header("加工が始まるまでの猶予時間")]
    [SerializeField] float _craftStartTime = 10;
    [Header("加工が始まった時の煙")]
    [SerializeField] Sprite _smokeSprite;
    [SerializeField] SpriteRenderer _sr;

    [Tooltip("加工中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemData _itemData;
    [Tooltip("合成後のアイテム")]
    ItemData _resultSynthetic;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        if (_manufactureing) return item;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic != null && item == null)
        {
            //アイテムがNullになった時アイテムを返す
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _sr.sprite = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return item;
        }


        //アイテムがマシンの許与量を超えていたらまたはアイテムが入っていなかったらアイテムを返す
        if (_itemData != null || item == null)
        {
            Debug.Log("アイテムを返すマス");
            return item;
        }
        //アイテムがNullではないときかつクラフトできるものだったら
        if (_itemData == null && item.Processing)
        {
            Debug.Log("入ってきた");
            //アイテムが入ったことでクラフト待機スタート
            _itemData = item;
            _sr.sprite = item.ItemSprite;
            return null;
        }

        return item;

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

    /// <summary>
    /// クラフトをスタートする
    /// </summary>
    public float Craft()
    {
        if (_itemData == null) return 0;
        Debug.Log("クラフトスタート");
        _manufactureing = true;
        //加工が始まったら煙を出す
        _sr.sprite = _smokeSprite;
        return _craftTime;

    }

    /// <summary>
    /// クラフトが終った時に呼ぶ
    /// </summary>
    public void CraftEnd()
    {
        ItemManufacture();
        _sr.sprite = _resultSynthetic.ItemSprite;
        _manufactureing = false;
    }
}
