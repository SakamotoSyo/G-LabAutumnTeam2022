using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("アイテムのSpriteを表示する場所")]
    [SerializeField] SpriteRenderer _itemSprite;
    [Header("アイテムを表示する場所")]
    [SerializeField] Transform[] _transformArray;
    [Header("プレゼントの画像")]
    [SerializeField] Sprite _presentSprite;
    [SerializeField] PlayerInput _playerInput;

    bool _isPresent;
    /// <summary>
    /// "現在持っているアイテムのデータ
    /// </summary>
    public ItemInformation ItemInventory { get; private set; }

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
    public void SetItemData(ItemInformation item)
    {
        //受け取ったデータのSpriteをセットする
        ItemInventory = item;
        if (item.Present)
        {
            _itemSprite.sprite = item.Item.PresentSprite;
        }
        else 
        {
            _itemSprite.sprite = item.Item.ItemSprite;
        }
        _itemSprite.enabled = true;
    }

    /// <summary>持っているアイテムのデータを返す</summary>
    public ItemInformation ReceiveItems()
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


[System.Serializable]
public class ItemInformation 
{
    public ItemData Item => _item;  
    [SerializeField]ItemData _item;
    public bool Present => _isPresent;
    [SerializeField]bool _isPresent;

    public ItemInformation(ItemData item, bool b) 
    {
        _item = item;
        _isPresent = b;
    }
}
