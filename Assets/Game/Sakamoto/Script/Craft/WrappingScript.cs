using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem
{
    [Header("����ނ̂ɂ����鎞��")]
    [SerializeField] int _wrappingTime;

    [Tooltip("�󂯎�����A�C�e����ۑ����Ă����ꏊ")]
    ItemData _itemData;

    public ItemData ReceiveItems(ItemData item)
    {
        //���ɃA�C�e�����u����Ă����ꍇ���̎����̃A�C�e����Ԃ�
        if (_itemData) 
        {
            return item;
        }

        return item;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
