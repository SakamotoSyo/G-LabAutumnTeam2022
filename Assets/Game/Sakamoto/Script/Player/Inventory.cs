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
    public ItemData _itemInventory { get; private set;}

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
        _itemInventory = item;
        _itemSprite.sprite = item._itemSprite;
        _itemSprite.enabled = true;
    }

    /// <summary>�����Ă���A�C�e���̃f�[�^��Ԃ�</summary>
    public ItemData ReceiveItems()
    {
        //�����Ă���A�C�e���������Ȃ����ēn��
        _itemObj.SetActive(false);
        var returnItem = _itemInventory;
        Debug.Log("Null�ɂ��܂���");
        _itemInventory = null;
        return returnItem;
    }

}
