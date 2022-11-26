using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlace : MonoBehaviour, IAddItem
{
    [Header("�A�C�e����\�����邽�߂Ɏg��SpriteRender")]
    [SerializeField] SpriteRenderer _sr;
    ItemData _itemData;

    public ItemData ReceiveItems(ItemData item)
    {

        //������̃A�C�e�������邩��Player���A�C�e���������Ă��Ȃ��Ƃ�
        //�����A�C�e����Ԃ�
        if (_itemData && item == null)
        {
            _sr.sprite = null;
            var ritem = _itemData;
            _itemData = null;
            //�A�C�e����Null�ɂȂ������A�C�e����Ԃ�
            return ritem;
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
            _sr.sprite = item.ItemSprite;
        }

        return null;
    }
}
