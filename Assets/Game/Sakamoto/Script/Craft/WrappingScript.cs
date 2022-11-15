using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem
{
    [Header("����ނ̂ɂ����鎞��")]
    [SerializeField] int _wrappingTime;
    [Header("�v���[���g�̉摜")]
    [SerializeField] Sprite _presentSp;

    [Tooltip("�󂯎�����A�C�e����ۑ����Ă����ꏊ")]
    ItemData _itemData;
    [Tooltip("���ݐ��쒆���ǂ���")]
    bool _manufactureing = false;
    [Tooltip("���삪�����������ǂ���")]
    bool _compCraft;
    Coroutine _wrappnigCor;
    WaitForSeconds _wrappingWait;

    void Start()
    {
       _wrappingWait = new WaitForSeconds(_wrappingTime);
    }

    /// <summary>
    /// �A�C�e���̎󂯓n��
    /// </summary>
    /// <param name="item">�n���ꂽ�A�C�e��</param>
    /// <returns>�A�C�e��</returns>
    public ItemData ReceiveItems(ItemData item)
    {
        if (_manufactureing && !item.Packing) return item;

        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        //�����A�C�e����Ԃ�
        if (_compCraft && item == null)
        {
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            StopCoroutine(_wrappnigCor);
            _compCraft = false;
            var saveItem = _itemData;
            _itemData = null;
            return saveItem;
        }

        //���ɃA�C�e�����u����Ă����ꍇ�n�����A�C�e�������̂܂ܕԂ�
        if (_itemData != null) 
        {
            return item;
        }

        //�A�C�e���f�[�^���Ȃ��Ƃ�
        if (_itemData == null) 
        {
            _itemData = item;
            _wrappnigCor = StartCoroutine(WrappingCraft());
        }

        return null;
    }

    IEnumerator WrappingCraft() 
    {
        yield return _wrappingWait;
        //�����Ă���A�C�e���f�[�^�ɕ�ς݂̖��߂��o��
        _itemData.IsPacking = true;
        _itemData.ItemSprite = _presentSp;
        _compCraft = true;

    }

}
