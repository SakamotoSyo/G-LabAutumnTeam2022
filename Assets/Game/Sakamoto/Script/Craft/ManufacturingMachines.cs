using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("�A�C�e���̃f�[�^")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("�A�C�e���̉��H����")]
    [SerializeField] float _waitSeconds;
    [Header("���b��������M�\���Ŕ������邩")]
    [SerializeField] float _runawayTime = 5;

    [Tooltip("�������I���������ǂ���")]
    bool _manufactureBool;
    [Tooltip("���������ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemData[] _itemArray;
    [Tooltip("������̃A�C�e��")]
    ItemData _resultSynthetic;
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 3;

    float _runawayCount;

    void Start()
    {
        _itemArray = new ItemData[_itemSaveNum];
    }


    async void Update()
    {
        await ManufactureDeley();

        if (_resultSynthetic != null)
        {

        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            StartManufacture();
        }
    }

    /// <summary>
    /// Item���󂯎�郁�\�b�h
    /// �A�C�e����n���邩�ǂ����߂�l�ŕԂ��̂ł���ō��̎�������j�����邩���f���Ăق���
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        if (_resultSynthetic != null && item == null)
        {
            return _resultSynthetic;
        }
        else if (_resultSynthetic != null) 
        {
            return item;
        }


        //�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����A�C�e����Ԃ�
        if (_itemArray[2] != null) 
        {
            Debug.Log("�A�C�e����Ԃ��}�X");
            return item;
        }
        //�������͓����Ă��Ȃ�
        if (!_manufactureBool && item != null)
        {
            Debug.Log("�����Ă���");
            for (int i = 0; i < _itemSaveNum; i++) 
            {
                if (_itemArray[i] == null) 
                {
                    _itemArray[i] = item;
                    break;
                }
            }
            return null;
        }

        return item;

    }


    /// <summary>
    /// ���H�J�n���\�b�h
    /// </summary>
    void StartManufacture()
    {
        if (_manufactureBool && !_manufactureing)
        {
            //�������I�����Ă���̂ŃA�C�e����n��
            return;

        }
        else if (!_manufactureBool && !_manufactureing)
        {
            //�����J�n
            ItemManufacture();
        }
        

    }


    /// <summary>
    /// �A�C�e�����쐬���郁�\�b�h
    /// </summary>
    void ItemManufacture()
    {
        //�����p�̔z������
        string[] itemArray = new string[_itemSaveNum];
        for (int i = 0; i < _itemSaveNum; i++) 
        {
            if (_itemArray[i] != null)
            {
                itemArray[i] = _itemArray[i].ItemName;
            }
            else 
            {
                itemArray[i] = "�Ȃ�";
            }
        }
    
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //�A�C�e���̖��O����v������
            if (_syntheticData.SyntheticList[i].Item1 == itemArray[0] && _syntheticData.SyntheticList[i].Item2 == itemArray[1]
                && _syntheticData.SyntheticList[i].Item3 == itemArray[2])
            {
                //�����f�[�^�x�[�X����String�̃f�[�^���擾����
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = _itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0];
                //Debug.Log($"���ʂ�{_resultSynthetic}");
                break;
            }
            //Debug.Log($"���ʔ��蒆:�A�C�e���P{itemArray[0]}�A�A�C�e���Q:{itemArray[1]}�A�A�C�e���R:{itemArray[2]}");
        }
        Array.Clear(_itemArray, 0, _itemArray.Length);
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


////�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����false��Ԃ�
//if (_itemList.Count >= _itemSaveNum)
//{
//    return item;
//}

////�󂯎�邱�Ƃ��ł����ԂȂ�List�Ɋi�[����true��Ԃ�
////�����A�C�e���������Ă��邩��Player���A�C�e���������Ă���Ƃ��͒ʂ�Ȃ�
//if (_resultSynthetic == null || item == null)
//{
//    //�������͓����Ă��Ȃ�
//    if (_manufactureBool && item == null)
//    {
//        _itemList.Add(item);
//        return null;
//    }
//    //StartManufacture(item);
//    //�ꎞ�I�Ƀ��[�J���ϐ��ɃA�C�e���̏���n��
//    var Item = _resultSynthetic;
//    _resultSynthetic = null;
//    return Item;
//}

//return item;
