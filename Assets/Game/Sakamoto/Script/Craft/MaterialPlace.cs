using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlace : MonoBehaviour, IAddItem
{
    [Header("アイテムを表示するために使うSpriteRender")]
    [SerializeField] SpriteRenderer _sr;
    ItemData _itemData;

    public ItemData ReceiveItems(ItemData item)
    {

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_itemData && item == null)
        {
            _sr.sprite = null;
            var ritem = _itemData;
            _itemData = null;
            //アイテムがNullになった時アイテムを返す
            return ritem;
        }
        //既にアイテムが置かれていた場合渡されるアイテムをそのまま返す
        if (_itemData != null)
        {
            return item;
        }
        //アイテムデータがないとき
        if (_itemData == null)
        {
            _itemData = item;
            _sr.sprite = item.ItemSprite;
        }

        return null;
    }
}
