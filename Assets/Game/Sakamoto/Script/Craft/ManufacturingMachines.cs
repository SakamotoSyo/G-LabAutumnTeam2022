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
    [Header("�A�C�e���̐�������")]
    [SerializeField] float _waitSeconds;
    [Header("���b��������M�\���Ŕ������邩")]
    [SerializeField] float _runawayTime = 5;
    [Header("�������n�܂�܂ł̗P�\����")]
    [SerializeField] float _craftStartTime = 10;

    [Tooltip("���������ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemData[] _itemArray;
    [Tooltip("������̃A�C�e��")]
    ItemData _resultSynthetic;
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 3;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;

    void Start()
    {
        _itemArray = new ItemData[_itemSaveNum];
    }


    void Update()
    {
        ////�e�X�g�p��ŏ���
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartManufacture();
        //}
    }

    /// <summary>
    /// Item���󂯎�郁�\�b�h
    /// �A�C�e����n���邩�ǂ����߂�l�ŕԂ��̂ł���ō��̎�������j�����邩���f���Ăق���
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        if (_manufactureing) return item;

        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        //�����A�C�e����Ԃ�
        if (_resultSynthetic != null && item == null)
        {
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            StopCoroutine(_runawayCoroutine);
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            return returnItem;
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
        //�A�C�e����Null�ł͂Ȃ��Ƃ�
        if (item != null)
        {
            Debug.Log("�����Ă���");
            //�A�C�e���������ĂȂ��Ƃ���ɓ����
            for (int i = 0; i < _itemSaveNum; i++)
            {
                if (_itemArray[i] == null)
                {
                    _itemArray[i] = item;
                    //�A�C�e�������������ƂŃN���t�g�ҋ@�X�^�[�g
                    StandbyCraft();
                    break;
                }
            }
            return null;
        }

        return item;

    }


    #region �����ҋ@�R���[�`��
    /// <summary>
    /// �A�C�e�������������ƂŌĂ΂��R���[�`���J�n�֐�
    /// </summary>
    void StandbyCraft()
    {
        if (_startCoroutine != null)
        {
            StopCoroutine(_startCoroutine);
            Debug.Log("�R���[�`���Ƃ߂�");
            _startCoroutine = StartCoroutine(StandbyCraftCor());
        }
        else
        {
            _startCoroutine = StartCoroutine(StandbyCraftCor());
        }
    }

    /// <summary>
    /// �N���t�g���X�^�[�g���邽�߂ɑҋ@����
    /// </summary>
    /// <returns></returns>
    IEnumerator StandbyCraftCor()
    {
        yield return new WaitForSeconds(_craftStartTime);
        Debug.Log("�N���t�g�X�^�[�g");
        //�r����Coroutine�����f����Ȃ�������Craft�J�n
        ItemManufacture();
        //�M�\���ҋ@�J�n
        _runawayCoroutine = StartCoroutine(ManufactureDeley());
        _manufactureing = true;
    }
    #endregion

    /// <summary>
    /// �������J�n���ꂽ�琻�������܂őҋ@����
    /// </summary>
    /// <returns></returns>
    IEnumerator ManufactureDeley()
    {

        Debug.Log("������wait");
        yield return new WaitForSeconds(_waitSeconds);
        Debug.Log("�������I���");
        _manufactureing = false;

    }


    /// <summary>
    /// �A�C�e���𐻑����郁�\�b�h
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
                StartCoroutine(ThermalRunaway());
                //Debug.Log($"���ʂ�{_resultSynthetic}");
                break;
            }
            //Debug.Log($"���ʔ��蒆:�A�C�e���P{itemArray[0]}�A�A�C�e���Q:{itemArray[1]}�A�A�C�e���R:{itemArray[2]}");
        }
        Array.Clear(_itemArray, 0, _itemArray.Length);
    }


    IEnumerator ThermalRunaway() 
    {
        Debug.Log("�\���ҋ@");
        yield return new WaitForSeconds(_runawayTime);
        Debug.Log("�\��");
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
