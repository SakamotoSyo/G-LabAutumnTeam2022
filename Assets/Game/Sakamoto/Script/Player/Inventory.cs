using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("�A�C�e����Sprite��\������ꏊ")]
    [SerializeField] SpriteRenderer _itemSprite;
    [Header("�A�C�e����\������ꏊ")]
    [SerializeField] Transform[] _transformArray;
    [Header("�v���[���g�̉摜")]
    [SerializeField] Sprite _presentSprite;
    [SerializeField] PlayerInput _playerInput;

    bool _isPresent;
    /// <summary>
    /// "���ݎ����Ă���A�C�e���̃f�[�^
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
    /// �A�C�e�����Z�b�g���Đ�������
    /// </summary>
    /// <param name="item"></param>
    public void SetItemData(ItemInformation item)
    {
        //�󂯎�����f�[�^��Sprite���Z�b�g����
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
