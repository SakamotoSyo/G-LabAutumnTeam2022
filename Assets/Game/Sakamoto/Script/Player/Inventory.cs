using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("PlayerHp")]
    [SerializeField] PlayerHp _playerHp;
    [Header("�A�C�e����Sprite��\������ꏊ")]
    [SerializeField] SpriteRenderer _itemSprite;
    [Header("�A�C�e����\������ꏊ")]
    [SerializeField] Transform[] _transformArray;
    [Header("�v���[���g�̉摜")]
    [SerializeField] Sprite _presentSprite;
    [SerializeField] PlayerInput _playerInput;

    bool _isDead = false;

    bool _isPresent;
    /// <summary>
    /// "���ݎ����Ă���A�C�e���̃f�[�^
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
    /// �A�C�e�����Z�b�g���Đ�������
    /// </summary>
    /// <param name="item"></param>
    public void SetItemData(ItemInformation item)
    {
        //�󂯎�����f�[�^��Sprite���Z�b�g����
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

    /// <summary>�����Ă���A�C�e���̃f�[�^��Ԃ�</summary>
    public ItemInformation ReceiveItems()
    {
        //�����Ă���A�C�e���������Ȃ����ēn��
        _itemSprite.enabled = false;
        var returnItem = ItemInventory;
        ItemInventory = null;
        return returnItem;
    }

    /// <summary>
    /// �C���x���g���̏ꏊ��ݒ肷��
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
    [Tooltip("�v���[���g�̉摜")]
    private Sprite _presentSprite;

    public int PartsNum => _partsNum;
    [Tooltip("���̃A�C�e���𐻑����邽�߂Ɏg�������i�̐�")]
    int _partsNum;
    public bool IsFineQuality => _isFineQuality;
    [Tooltip("�㎿���ǂ���")]
    bool _isFineQuality = false;
    public int ItemScore => _itemScore;
    [Tooltip("���̃A�C�e���̃X�R�A")]
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
