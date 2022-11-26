using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("アイテムを生成する場所")]
    [SerializeField] GameObject _itemObj;
    [Header("アイテムのSpriteを表示する場所")]
    [SerializeField] SpriteRenderer _itemSprite;
    [Header("アイテムを表示する場所")]
    [SerializeField] Transform[] _transformArray;
    [SerializeField] PlayerInput _playerInput;

    /// <summary>
    /// "現在持っているアイテムのデータ
    /// </summary>
    public ItemData ItemInventory { get; private set;}

    void Awake()
    {
        _itemSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        InventoryPosition();  
    }

    /// <summary>
    /// アイテムをセットして生成する
    /// </summary>
    /// <param name="item"></param>
    public void SetItemData(ItemData item)
    {
        //受け取ったデータのSpriteをセットする
        ItemInventory = item;
        _itemSprite.sprite = item.ItemSprite;
        _itemSprite.enabled = true;
    }

    /// <summary>持っているアイテムのデータを返す</summary>
    public ItemData ReceiveItems()
    {
        //持っているアイテムを見えなくして渡す
        _itemSprite.enabled = false;
        var returnItem = ItemInventory;
        ItemInventory = null;
        return returnItem;
    }

    /// <summary>
    /// インベントリの場所を設定する
    /// </summary>
    public void InventoryPosition() 
    {
        if (_playerInput.LastMoveDir.y == 1)
        {
            gameObject.transform.position = _transformArray[2].position;
            _itemSprite.sortingOrder = 1;
        }
        else if (_playerInput.LastMoveDir.y == -1)
        {
            gameObject.transform.position = _transformArray[3].position;
            _itemSprite.sortingOrder = 100;
        }
        else if (_playerInput.LastMoveDir.x == 1)
        {
            gameObject.transform.position = _transformArray[0].position;
        }
        else if (_playerInput.LastMoveDir.x == -1) 
        {
            gameObject.transform.position = _transformArray[1].position;
        }
    }

}
