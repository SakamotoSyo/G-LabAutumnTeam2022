using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem
{
    [Header("包装を包むのにかかる時間")]
    [SerializeField] int _wrappingTime;

    [Tooltip("受け取ったアイテムを保存しておく場所")]
    ItemData _itemData;

    public ItemData ReceiveItems(ItemData item)
    {
        //既にアイテムが置かれていた場合今の自分のアイテムを返す
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
