using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("PlayerHp")]
    [SerializeField] PlayerHp _playerHp;
    [Header("アイテムのSpriteを表示する場所")]
    [SerializeField] SpriteRenderer _itemSprite;
    [Header("アイテムを表示する場所")]
    [SerializeField] Transform[] _transformArray;
    [Header("プレゼントの画像")]
    [SerializeField] Sprite _presentSprite;
    [SerializeField] PlayerInput _playerInput;

    bool _isDead = false;

    bool _isPresent;
    /// <summary>
    /// "現在持っているアイテムのデータ
    /// </summary>
    public ItemInformation ItemInventory { get; private set; }

    void Awake()
    {
        _playerHp.OnHealth += OnHelthChanged;
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
            _itemSprite.sprite = item.PresentSprite;
        }
        else if (item.IsFineQuality)
        {
            _itemSprite.sprite = item.Item.FineQualitySprite;
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

    private void OnHelthChanged(float amount) 
    {
        if (amount <= 0) 
        {
            _isDead = true;
            _itemSprite.enabled = false;
            ItemInventory = null;

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
    public Sprite PresentSprite => _presentSprite;
    [Tooltip("プレゼントの画像")]
    private Sprite _presentSprite;

    public int PartsNum => _partsNum;
    [Tooltip("このアイテムを製造するために使った部品の数")]
    int _partsNum;
    public bool IsFineQuality => _isFineQuality;
    [Tooltip("上質かどうか")]
    bool _isFineQuality = false;
    public int ItemScore => _itemScore;
    [Tooltip("このアイテムのスコア")]
    int _itemScore;

    public ItemInformation(ItemData item, bool b) 
    {
        _item = item;
        _isPresent = b;
    }

    public void SetParts(int add)
    {
        _partsNum += add;
    }

    public void SetFineQuality(bool set) 
    {
        _isFineQuality = set;
    }

    public void SetItemScore(int score) 
    {
        _itemScore = score;
    }

    public void PresentSet(Sprite sprite) 
    {
        _presentSprite = sprite;
    }
}
