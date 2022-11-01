using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("アイテムを生成する場所")]
    [SerializeField] GameObject _itemObj;
    [Header("アイテムのSpriteを表示する場所")]
    [SerializeField] SpriteRenderer _itemSprite;

    /// <summary>
    /// "現在持っているアイテムのデータ
    /// </summary>
    public ItemData _itemInventory { get; private set;}

    void Awake()
    {
        _itemSprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// アイテムをセットして生成する
    /// </summary>
    /// <param name="item"></param>
    public void SetItemData(ItemData item)
    {
        //受け取ったデータのSpriteをセットする
        _itemInventory = item;
        _itemSprite.sprite = item._itemSprite;
        _itemSprite.enabled = true;
    }

    /// <summary>持っているアイテムのデータを返す</summary>
    public ItemData ReceiveItems()
    {
        //持っているアイテムを見えなくして渡す
        _itemObj.SetActive(false);
        var returnItem = _itemInventory;
        Debug.Log("Nullにしました");
        _itemInventory = null;
        return returnItem;
    }

}
