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
    [Header("�΂̕���Prefab")]
    [SerializeField] GameObject _firePrefab;
    [Header("�΂̕�����юU��͈�")]
    [SerializeField] Transform[] _fireTransform = new Transform[1];
    [Header("���Ԍo�߂ɂ���ĕς�鐻������Sprite")]
    [SerializeField] Sprite[] _millSprite = new Sprite[4];
    [Header("�ʏ펞��Sprite")]
    [SerializeField] Sprite _standardSprite;
    [Header("�M�\������Sprite")]
    [SerializeField] Sprite _thermalRunawaySprite;
    [Header("��������SpriteRenderer")]
    [SerializeField] SpriteRenderer _millRenderer;
    [Header("�A�C�e���̐�������")]
    [SerializeField] float _waitSeconds;
    [Header("�������n�܂�܂ł̗P�\����")]
    [SerializeField] float _craftStartTime = 10;
    [Header("Effect��Animator")]
    [SerializeField] Animator _effectAnim;
    [Header("Effect��SubAnimator")]
    [SerializeField] Animator _subEffectAnim;

    [Tooltip("�M�\�����鎞��")]
    float _runawayTime = 5;
    [Tooltip("�A�C�e������ꂽ��Ƃ�����o���邩")]
    bool _isLook;
    [Tooltip("�㎿�Ȃ��̂��ǂ���")]
    bool _isFineQuality;
    [Tooltip("�㎿�Ȃ��̂�����ł��鎞��")]
    readonly float _fineQualityTime = 5;
    [Tooltip("��������܂ł̎���")]
    readonly int _explosionTime = 5;
    [Tooltip("�����ɂ����鎞��")]
    readonly int _explosionEndTime = 3;
    [Tooltip("�C�����I���܂ł̎���")]
    readonly int _repairTime = 30;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemInformation[] _itemArray;
    [Tooltip("������̃A�C�e��")]
    ItemInformation _resultSynthetic = new ItemInformation(null, false);
    [Tooltip("�����@���ۑ��ł���A�C�e���̐�")]
    readonly int _itemSaveNum = 2;
    Coroutine _startCoroutine;
    Coroutine _craftStartCoroutine;
    Coroutine _fineQualityCor;
    Coroutine _runawayCoroutine;
    Coroutine _explosionCoroutine;

    void Start()
    {
        _itemArray = new ItemInformation[_itemSaveNum];
        ArrayInit();
    }

    private void ArrayInit()
    {
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
        if (_isLook) return itemInfo;

        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        //�����A�C�e����Ԃ�
        if (_resultSynthetic.Item != null && itemInfo == null)
        {
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            //�s�K�v�ȃR���[�`�����~�߂�
            CraftStopCor();

            if (_isFineQuality)
            {
                Debug.Log("�㎿�ł�");
                _resultSynthetic.SetFineQuality(true);
            }
            var returnItem = _resultSynthetic;
            _resultSynthetic = new ItemInformation(null, false);
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
        if (itemInfo == null)
        {
            return itemInfo;
        }
        if (itemInfo.Item.Craft) 
        {
            Debug.Log("�����Ă���");
            //�A�C�e���������ĂȂ��Ƃ���ɓ����
            for (int i = 0; i < _itemSaveNum; i++)
            {
                if (_itemArray[i].Item == null)
                {
                    if (i == 0)
                    {
                        _runawayTime = itemInfo.Item.RunawayTime;
                        Debug.Log(itemInfo.Item.RunawayTime);
                    }
                    else 
                    {
                        _runawayTime += itemInfo.Item.RunawayTime;
                        Debug.Log(_runawayTime);
                    }
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
        _effectAnim.SetBool("Craft", true);
        _subEffectAnim.SetBool("Thunder", true);
        //�N���t�g�J�n
        _craftStartCoroutine = StartCoroutine(ManufactureDeley());
        _isLook = true;
    }
    #endregion

    /// <summary>
    /// �������J�n���ꂽ�琻�������܂őҋ@����
    /// </summary>
    /// <returns></returns>
    IEnumerator ManufactureDeley()
    {
        //���Ԍo�߂ɂ���Đ�������Sprite��ς���
        for (int i = 0; i < _millSprite.Length; i++)
        {
            yield return new WaitForSeconds(_waitSeconds / _millSprite.Length);
            //�����o��
            if (i == _millSprite.Length - 1)
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_lastlamp);
            }
            else
            {
                AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_lamp);
            }

            _millRenderer.sprite = _millSprite[i];
        }

        _effectAnim.SetBool("Craft", false);
        _subEffectAnim.SetBool("Thunder", false);
        //var cor = FineQualityTime();
        //�r����Coroutine�����f����Ȃ�������Craft�J�n
        ItemManufacture();
        _isLook = false;
        _craftStartCoroutine = null;
        _runawayCoroutine = StartCoroutine(ThermalRunaway());
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
            if (_syntheticData.SyntheticList[i].Item1.Contains(itemArray[0][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[1][0]) || _syntheticData.SyntheticList[i].Item1.Contains(itemArray[1][0]) && _syntheticData.SyntheticList[i].Item2.Contains(itemArray[0][0]))
            {
                //�����f�[�^�x�[�X����String�̃f�[�^���擾����
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                Debug.Log(_syntheticData.SyntheticList[i].ResultItem);
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                _resultSynthetic.SetParts(PartsCheck(itemArray));
                if (_resultSynthetic.PartsNum == _resultSynthetic.Item.ItemParts) 
                {
                    _fineQualityCor = StartCoroutine(FineQualityTime());
                }
                //Debug.Log($"���ʂ�{_resultSynthetic}");
                break;
            }
            //Debug.Log($"���ʔ��蒆:�A�C�e���P{itemArray[0]}�A�A�C�e���Q:{itemArray[1]}�A�A�C�e���R:{itemArray[2]}");
        }
        ArrayInit();
    }


    /// <summary>
    /// �㎿�Ȃ��̂��Q�b�g�ł��鎞��
    /// </summary>
    /// <returns></returns>
    private IEnumerator FineQualityTime()
    {
        Debug.Log("�㎿");
        _isFineQuality = true;
        yield return new WaitForSeconds(_fineQualityTime);
        _isFineQuality = false;
    }

    /// <summary>
    /// �\������܂ł̎���
    /// </summary>
    /// <returns></returns>
    IEnumerator ThermalRunaway()
    {
        yield return new WaitForSeconds(5);
        _millRenderer.sprite = _thermalRunawaySprite;
        _effectAnim.SetBool("RunAway", true);
        yield return new WaitForSeconds(_runawayTime);
        _explosionCoroutine = StartCoroutine(Explosion());
    }

    /// <summary>
    /// �����܂ł̎���
    /// </summary>
    /// <returns></returns>
    IEnumerator Explosion()
    {
        _isLook = true;
        Debug.Log("����");
        FireGeneration();
        _resultSynthetic = new ItemInformation(null, false);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_explosion);
        _effectAnim.SetBool("RunAway", false);
        _effectAnim.SetBool("Explosion", true);
        yield return new WaitForSeconds(_explosionTime);
        StartCoroutine(RepairTime());
    }

    IEnumerator RepairTime()
    {
        _effectAnim.SetBool("Explosion", false);
        _effectAnim.SetBool("Repair", true);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_repair);
        yield return new WaitForSeconds(_repairTime);
        AudioManager.Instance.PlaySound(SoundPlayType.SE_manufacture_repair_end);
        _isLook = false;
        _effectAnim.SetBool("Repair", false);
        _millRenderer.sprite = _standardSprite;
    }

    /// <summary>
    /// �΂̕��𐶐�����
    /// </summary>
    private void FireGeneration() 
    {
        for (int i = 0; i < 2; i++) 
        {
            var x = UnityEngine.Random.Range(_fireTransform[0].position.x, _fireTransform[1].position.x);
            var y = UnityEngine.Random.Range(_fireTransform[0].position.y, _fireTransform[1].position.y);
            Instantiate(_firePrefab, new Vector2(x, y), gameObject.transform.rotation);
        }
       
    }

    private void CraftStopCor()
    {
        if (_craftStartCoroutine != null)
        {
            StopCoroutine(_craftStartCoroutine);
        }

        if (_runawayCoroutine != null)
        {
            StopCoroutine(_runawayCoroutine);
            Debug.Log("�\���~�߂�");
            _effectAnim.SetBool("RunAway", false);
            _millRenderer.sprite = _standardSprite;
        }

        if (_explosionCoroutine != null)
        {
            StopCoroutine(_explosionCoroutine);
            _effectAnim.SetBool("RunAway", false);
            _millRenderer.sprite = _standardSprite;
        }

        if (_fineQualityCor != null)
        {
            StopCoroutine(_fineQualityCor);
        }

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
