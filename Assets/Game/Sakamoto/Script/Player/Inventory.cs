using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("�A�C�e���𐶐�����ꏊ")]
    [SerializeField] GameObject _itemObj;
    [Header("�A�C�e����Sprite��\������ꏊ")]
    [SerializeField] SpriteRenderer _itemSprite;

    /// <summary>
    /// "���ݎ����Ă���A�C�e���̃f�[�^
    /// </summary>
    public ItemData ItemInventory { get; private set;}

    void Awake()
    {
        _itemSprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �A�C�e�����Z�b�g���Đ�������
    /// </summary>
    /// <param name="item"></param>
    public void SetItemData(ItemData item)
    {
        //�󂯎�����f�[�^��Sprite���Z�b�g����
        ItemInventory = item;
        _itemSprite.sprite = item.ItemSprite;
        _itemSprite.enabled = true;
    }

    /// <summary>�����Ă���A�C�e���̃f�[�^��Ԃ�</summary>
    public ItemData ReceiveItems()
    {
        //�����Ă���A�C�e���������Ȃ����ēn��
        _itemSprite.enabled = false;
        var returnItem = ItemInventory;
        Debug.Log("Null�ɂ��܂���");
        ItemInventory = null;
        return returnItem;
    }

}
