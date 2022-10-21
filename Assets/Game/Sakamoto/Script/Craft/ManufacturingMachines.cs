using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ManufacturingMachines : MonoBehaviour
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
    string _item1;
    string _item2;
    string _item3;
    string _resultSynthetic;
    /// <summary>
    /// ���H�J�n���\�b�h
    /// </summary>
    public void StartManufacture() 
    {
        if (_manufactureBool && !_manufactureing) 
        {
            //�������I�������̂ŃA�C�e����n��
            ItemManufacture(_item1, _item2, _item3);
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
