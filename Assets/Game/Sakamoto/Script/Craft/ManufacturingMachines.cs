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
    [Header("���Ԍo�߂ɂ���ĕς�鐻������Sprite")]
    [SerializeField] Sprite[] _millSprite = new Sprite[4];
    [Header("�M�\������Sprite")]
    [SerializeField] Sprite _thermalRunawaySprite;
    [Header("��������SpriteRenderer")]
    [SerializeField] SpriteRenderer _millRenderer;
    [Header("�A�C�e���̐�������")]
    [SerializeField] float _waitSeconds;
    [Header("���b��������M�\���Ŕ������邩")]
    [SerializeField] float _runawayTime = 5;
    [Header("�������n�܂�܂ł̗P�\����")]
    [SerializeField] float _craftStartTime = 10;

    [Tooltip("���������ǂ���")]
    bool _manufactureing;
    [Tooltip("�㎿�Ȃ��̂��ǂ���")]
    bool _isFineQuality;
    [Tooltip("�㎿�Ȃ��̂�����ł��鎞��")]
    readonly float _fineQualityTime = 5;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemInformation[] _itemArray;
    [Tooltip("������̃A�C�e��")]
    ItemInformation _resultSynthetic = new ItemInformation (null, false);
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 3;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;
    Coroutine _fineQualityCor;

    void Start()
    {
        _itemArray = new ItemInformation[_itemSaveNum];

        for (int i = 0; i < _itemSaveNum; i++) 
        {
            _itemArray[i] = new ItemInformation(null, false);
        }
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
    /// <param name="itemInfo"></param>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        if (_manufactureing) return itemInfo;

        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        //�����A�C�e����Ԃ�
        if (_resultSynthetic.Item != null && itemInfo == null)
        {
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            StopCoroutine(_runawayCoroutine);
            StopCoroutine(_fineQualityCor);
            if (_isFineQuality) 
            {
                Debug.Log("�㎿�ł�");
                _resultSynthetic.SetFineQuality(true);
            }
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _millRenderer.sprite = _millSprite[0];
            return returnItem;
        }
        else if (_resultSynthetic.Item != null)
        {
            return itemInfo;
        }


        //�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����A�C�e����Ԃ�
        if (_itemArray[_itemSaveNum - 1].Item != null)
        {
            Debug.Log("�A�C�e����Ԃ��}�X");
            return itemInfo;
        }
        //�A�C�e����Null�ł͂Ȃ��Ƃ�
        if (itemInfo != null)
        {
            Debug.Log("�����Ă���");
            //�A�C�e���������ĂȂ��Ƃ���ɓ����
            for (int i = 0; i < _itemSaveNum; i++)
            {
                if (_itemArray[i].Item == null)
                {
                    _itemArray[i] = itemInfo;
                    //�A�C�e�������������ƂŃN���t�g�ҋ@�X�^�[�g
                    StandbyCraft();
                    break;
                }
            }
            return null;
        }

        return itemInfo;

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
        //���Ԍo�߂ɂ���Đ�������Sprite��ς���
        for (int i = 0; i < _millSprite.Length; i++) 
        {
            yield return new WaitForSeconds(_waitSeconds/_millSprite.Length);
            _millRenderer.sprite = _millSprite[i]; 
        }
        
        Debug.Log("�������I���");
        //var cor = FineQualityTime();
        _fineQualityCor = StartCoroutine(FineQualityTime());
        StartCoroutine(ThermalRunaway());
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
            if (_itemArray[i].Item != null)
            {
                itemArray[i] = _itemArray[i].Item.ItemName;
            }
            else
            {
                itemArray[i] = "�Ȃ�";
            }
        }

        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //�A�C�e���̖��O����v������
            if (_syntheticData.SyntheticList[i].Item1.Contains(itemArray[0][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[1][0]))
            {
                //�����f�[�^�x�[�X����String�̃f�[�^���擾����
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                _resultSynthetic.SetParts(PartsCheck(itemArray));
                //Debug.Log($"���ʂ�{_resultSynthetic}");
                break;
            }
            //Debug.Log($"���ʔ��蒆:�A�C�e���P{itemArray[0]}�A�A�C�e���Q:{itemArray[1]}�A�A�C�e���R:{itemArray[2]}");
        }
        Array.Clear(_itemArray, 0, _itemArray.Length);
    }


    /// <summary>
    /// �㎿�Ȃ��̂��Q�b�g�ł��鎞��
    /// </summary>
    /// <returns></returns>
    private IEnumerator FineQualityTime() 
    {
        _isFineQuality = true;
        yield return new WaitForSeconds(_fineQualityTime);
        _isFineQuality = false;
    }

    IEnumerator ThermalRunaway() 
    {
        Debug.Log("�\���ҋ@");
        yield return new WaitForSeconds(_runawayTime);
        _millRenderer.sprite = _thermalRunawaySprite;
        Debug.Log("�\��");
    }


    /// <summary>
    /// ���i�������܂܂�Ă��邩���ׂ�
    /// </summary>
    /// <param name="itemNameArray"></param>
    /// <returns></returns>
    private int PartsCheck(string[] itemNameArray) 
    {
        int partsNum = 0;
        for (int i = 0; i < 2; i++) 
        {
            if (itemNameArray[i].Contains("���i")) 
            {
                partsNum++;
            }
        }

        return partsNum;
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
