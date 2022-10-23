using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField]ItemSyntheticDataBase _syntheticData;

    [Header("�A�C�e���̉��H����")]
    [SerializeField] WaitForSeconds _waitSeconds;

    [Tooltip("�������I���������ǂ���")]
    bool _manufactureBool;
    [Tooltip("���������ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    List<ItemData> _itemList = new List<ItemData>();
    [Tooltip("������̃A�C�e��")]
    string _resultSynthetic;
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 3;

    /// <summary>
    /// Item���󂯎�郁�\�b�h
    /// </summary>
    /// <param name="item"></param>
    public bool ReceiveItems(ItemData item)
    {
        //�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����false��Ԃ�
        if (_itemList.Count >= _itemSaveNum) 
        {
            return false;
        }

        //�󂯎�邱�Ƃ��ł����ԂȂ�List�Ɋi�[����true��Ԃ�
        _itemList.Add(item);
        return true;
    }


    /// <summary>
    /// ���H�J�n���\�b�h
    /// </summary>
    public void StartManufacture() 
    {
        if (_manufactureBool && !_manufactureing)
        {
            //�������I�������̂ŃA�C�e����n��

        }
        else if (!_manufactureBool && !_manufactureing)
        {
            //�����J�n
            ItemManufacture(_itemList[0].name, _itemList[1].name, _itemList[2].name);
        }
        else if (!_manufactureBool && _manufactureing) 
        {
            //�������I����Ă��Ȃ��Đ������ȏꍇReturn��Ԃ�
            return;
        }

    }


    /// <summary>
    /// �A�C�e�����쐬���郁�\�b�h
    /// </summary>
    string ItemManufacture(string item1 = "�Ȃ�", string item2 = "�Ȃ�", string item3 = "�Ȃ�") 
    {
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++) 
        {
            //�A�C�e���̖��O����v������
            if (_syntheticData.SyntheticList[i]._item1 == item1 && _syntheticData.SyntheticList[i]._item2 == item2
                && _syntheticData.SyntheticList[i]._item3 == item3) 
            {
                _resultSynthetic = _syntheticData.SyntheticList[i]._resultItem;
                break;
            }
        }

        return _resultSynthetic; 
    }
}
