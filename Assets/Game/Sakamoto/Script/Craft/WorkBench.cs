using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkBench : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("�A�C�e���̃f�[�^")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("�A�C�e���̐�������")]
    [SerializeField] float _craftTime;
    [Header("���H���n�܂�܂ł̗P�\����")]
    [SerializeField] float _craftStartTime = 10;
    [SerializeField] Animator _workAnim;
    [SerializeField] SpriteRenderer _sr;

    [Tooltip("���H�����ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemInformation _itemData;
    [Tooltip("������̃A�C�e��")]
    ItemInformation _resultSynthetic;
    Coroutine _startCoroutine;
    Coroutine _runawayCoroutine;

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
        if (_resultSynthetic != null && itemInfo == null)
        {
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _sr.sprite = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return itemInfo;
        }


        //�A�C�e�����}�V���̋��^�ʂ𒴂��Ă�����܂��̓A�C�e���������Ă��Ȃ�������A�C�e����Ԃ�
        if (_itemData != null || itemInfo == null)
        {
            Debug.Log("�A�C�e����Ԃ��}�X");
            return itemInfo;
        }
        //�A�C�e����Null�ł͂Ȃ��Ƃ����N���t�g�ł�����̂�������
        if (_itemData == null && itemInfo.Item.Processing)
        {
            Debug.Log("�����Ă���");
            //�A�C�e�������������ƂŃN���t�g�ҋ@�X�^�[�g
            _itemData = itemInfo;
            _sr.sprite = itemInfo.Item.ItemSprite;
            return null;
        }

        return itemInfo;

    }

    /// <summary>
    /// �A�C�e�������H���郁�\�b�h
    /// </summary>
    void ItemManufacture()
    {
    
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //�A�C�e���̖��O����v������
            if (_syntheticData.SyntheticList[i].Item1 == _itemData.Item.ItemName)
            {
                //�����f�[�^�x�[�X����String�̃f�[�^���擾����
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = new ItemInformation(_itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0], false);
                //Debug.Log($"���ʂ�{_resultSynthetic}");
                break;
            }
            //Debug.Log($"���ʔ��蒆:�A�C�e���P{itemArray[0]}�A�A�C�e���Q:{itemArray[1]}�A�A�C�e���R:{itemArray[2]}");
        }

        _itemData = null;
    }

    /// <summary>
    /// �N���t�g���X�^�[�g����
    /// </summary>
    public float Craft()
    {
        if (_itemData == null) return 0;
        _manufactureing = true;
        //���H���n�܂����牌���o��
        _workAnim.SetBool("WorkCraft", true);
        ChooseSe(_itemData.Item.ItemName);
        return _itemData. Item.CraftTime;

    }

    /// <summary>
    /// �N���t�g���I�������ɌĂ�
    /// </summary>
    public void CraftEnd()
    {
        ItemManufacture();
        _workAnim.SetBool("WorkCraft", false);
        _workAnim.SetTrigger("Comp");
        _manufactureing = false;
        _sr.sprite = _resultSynthetic.Item.ItemSprite;
        Debug.Log(_resultSynthetic.Item.ItemSprite);
    }

    void ChooseSe(string name) 
    {
        if (name[0] == '��')
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_wood);

        }
        else if (name[0] == '�S')
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_iron);
        }
        else if (name[0] == '�z') 
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_processing_cloth);
        }
    }
}
