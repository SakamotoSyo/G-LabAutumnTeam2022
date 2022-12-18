using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("����n�܂������̉�")]
    [SerializeField] Sprite _smokeSprite;
    [SerializeField] SpriteRenderer _sr;
    [SerializeField] Sprite[] _presentSprites = new Sprite[4];

    [Tooltip("���H�����ǂ���")]
    bool _manufactureing;
    [Tooltip("�A�C�e����ۑ����Ă����ϐ�")]
    ItemInformation _itemData;
    [Tooltip("������̃A�C�e��")]
    ItemInformation _resultSynthetic;

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
        if (_itemData == null && itemInfo.Item.Packing && !itemInfo.Present)
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
    /// �N���t�g���X�^�[�g����
    /// </summary>
    public float Craft()
    {
        if (_itemData == null) return 0;
        Debug.Log("�N���t�g�X�^�[�g");
        AudioManager.Instance.PlaySound(SoundPlayType.SE_packing);
        _manufactureing = true;
        //���H���n�܂����牌���o��
        _sr.sprite = _smokeSprite;
        return _itemData.Item.CraftTime;

    }

    /// <summary>
    /// �N���t�g���I�������ɌĂ�
    /// </summary>
    public void CraftEnd()
    {
        _resultSynthetic = new ItemInformation( _itemData.Item, true);
        _sr.sprite = PresentJudge();
        _resultSynthetic.PresentSet(PresentJudge());
        _itemData = null;
        _manufactureing = false;
    }


    Sprite PresentJudge() 
    {
        if (_itemData.Item.ItemParts == 0) 
        {
            _resultSynthetic.SetItemScore(0);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && !_itemData.IsFineQuality && _itemData.PartsNum == 0)
        {
            _resultSynthetic.SetItemScore(10);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && !_itemData.IsFineQuality && _itemData.PartsNum == 1)
        {
            _resultSynthetic.SetItemScore(30);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && _itemData.IsFineQuality && _itemData.PartsNum == 1)
        {
            _resultSynthetic.SetItemScore(50);
            return _presentSprites[1];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.Item.ItemParts == 0)
        {
            _resultSynthetic.SetItemScore(50);
            return _presentSprites[1];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.Item.ItemParts == 1)
        {
            _resultSynthetic.SetItemScore(100);
            return _presentSprites[2];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.PartsNum == 2 && _itemData.IsFineQuality)
        {
            _resultSynthetic.SetItemScore(200);
            return _presentSprites[3];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.PartsNum == 2 && !_itemData.IsFineQuality)
        {
            _resultSynthetic.SetItemScore(150);
            return _presentSprites[2];
        }

        return _presentSprites[0];
    }
}
