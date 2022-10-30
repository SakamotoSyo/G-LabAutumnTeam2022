using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("�A�C�e���̃f�[�^")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("�A�C�e���̉��H����")]
    [SerializeField] float _waitSeconds;
    //[Header("��������܂ł̎���")]

    [Tooltip("�������I���������ǂ���")]
    bool _manufactureBool;
    [Tooltip("���������ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    List<ItemData> _itemList = new List<ItemData>();
    [Tooltip("������̃A�C�e��")]
    ItemData _resultSynthetic;
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 3;

    async void Update()
    {
        await ManufactureDeley();
    }

    /// <summary>
    /// Item���󂯎�郁�\�b�h
    /// �A�C�e����n���邩�ǂ����߂�l�ŕԂ��̂ł���ō��̎�������j�����邩���f���Ăق���
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        //�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����false��Ԃ�
        if (_itemList.Count >= _itemSaveNum)
        {
            return item;
        }

        //�󂯎�邱�Ƃ��ł����ԂȂ�List�Ɋi�[����true��Ԃ�
        //�����A�C�e���������Ă��邩��Player���A�C�e���������Ă���Ƃ��͒ʂ�Ȃ�
        if (_resultSynthetic == null ||�@item == null) 
        {
            //�������͓����Ă��Ȃ�
            if (_manufactureBool) 
            {
              _itemList.Add(item);
            }
            StartManufacture(item);
            //�ꎞ�I�Ƀ��[�J���ϐ��ɃA�C�e���̏���n��
            var Item = _resultSynthetic;
            _resultSynthetic = null;
            return Item;
        }

        return item;
    }


    /// <summary>
    /// ���H�J�n���\�b�h
    /// </summary>
    void StartManufacture(ItemData item)
    {
        if (_manufactureBool && !_manufactureing)
        {
            //�������I�����Ă���̂ŃA�C�e����n��
            return;

        }
        else if (!_manufactureBool && !_manufactureing)
        {
            //�����J�n
            ItemManufacture(_itemList[0].name, _itemList[1].name, _itemList[2].name);
        }
        else if (!_manufactureBool && _manufactureing)
        {
            //�������I����Ă��Ȃ��Đ������ȏꍇReturn����
            _resultSynthetic = item;
            return;
        }

    }


    /// <summary>
    /// �A�C�e�����쐬���郁�\�b�h
    /// </summary>
    void ItemManufacture(string item1 = "�Ȃ�", string item2 = "�Ȃ�", string item3 = "�Ȃ�")
    {
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //�A�C�e���̖��O����v������
            if (_syntheticData.SyntheticList[i].Item1 == item1 && _syntheticData.SyntheticList[i].Item2 == item2
                && _syntheticData.SyntheticList[i].Item3 == item3)
            {
                //�����f�[�^�x�[�X����String�̃f�[�^���擾����
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = _itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0];
                break;
            }
        }
        _itemList.Clear();
    }


    /// <summary>
    /// �������J�n���ꂽ��ҋ@����
    /// </summary>
    /// <returns></returns>
    async UniTask ManufactureDeley()
    {
        if (_manufactureing)
        {
            Debug.Log("������wait");
            await UniTask.Delay(TimeSpan.FromSeconds(_waitSeconds));
            _manufactureing = false;
        }
    }
}
