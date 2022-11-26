using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("包装が始まった時の煙")]
    [SerializeField] Sprite _smokeSprite;
    [SerializeField] SpriteRenderer _sr;

    [Tooltip("加工中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemInformation _itemData;
    [Tooltip("合成後のアイテム")]
    ItemInformation _resultSynthetic;

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
        if (_resultSynthetic != null && itemInfo == null)
        {
            //アイテムがNullになった時アイテムを返す
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _sr.sprite = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return itemInfo;
        }


        //アイテムがマシンの許与量を超えていたらまたはアイテムが入っていなかったらアイテムを返す
        if (_itemData != null || itemInfo == null)
        {
            Debug.Log("アイテムを返すマス");
            return itemInfo;
        }
        //アイテムがNullではないときかつクラフトできるものだったら
        if (_itemData == null && itemInfo.Item.Packing)
        {
            Debug.Log("入ってきた");
            //アイテムが入ったことでクラフト待機スタート
            _itemData = itemInfo;
            _sr.sprite = itemInfo.Item.ItemSprite;
            return null;
        }

        return itemInfo;

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
        return _itemData.Item.CraftTime;

    }

    /// <summary>
    /// クラフトが終った時に呼ぶ
    /// </summary>
    public void CraftEnd()
    {
        _sr.sprite = _itemData.Item.PresentSprite;
        _resultSynthetic = new ItemInformation( _itemData.Item, true);
        _itemData = null;
        _manufactureing = false;
    }

}
