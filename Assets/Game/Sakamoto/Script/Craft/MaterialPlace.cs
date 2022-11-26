using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlace : MonoBehaviour, IAddItem
{
    [Header("アイテムを表示するために使うSpriteRender")]
    [SerializeField] SpriteRenderer _sr;
    ItemInformation _itemData;

    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_itemData != null && itemInfo == null)
        {
            _sr.sprite = null;
            var ritem = _itemData;
            _itemData = null;
            //アイテムがNullになった時アイテムを返す
            return ritem;
        }
        //既にアイテムが置かれていた場合渡されるアイテムをそのまま返す
        if (_itemData != null || itemInfo == null)
        {
            return itemInfo;
        }
        //アイテムデータがないとき
        if (_itemData == null)
        {
            _itemData = itemInfo;
            if (itemInfo.Present)
            {
                _sr.sprite = itemInfo.Item.PresentSprite;
            }
            else 
            {
                _sr.sprite = itemInfo.Item.ItemSprite;
            }
            
        }

        return null;
    }
}
