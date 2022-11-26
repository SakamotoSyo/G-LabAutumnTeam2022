using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingScript : MonoBehaviour, IAddItem
{
    [Header("オーダーを管理するスクリプト")]
    [SerializeField] OrderScript _orderScript;
    /// <summary>
    /// アイテム受け渡しの処理
    /// </summary>
    /// <param name="item">アイテム</param>
    /// <returns></returns>
    public ItemData ReceiveItems(ItemData item)
    {
        for (int i = 0; i < _orderScript.OrderDatas.Length; i++)
        {
            if (_orderScript.OrderDatas[i].ResultItem == item.ItemName) 
            {
                //オーダーから該当のアイテムを削除
                _orderScript.OrderComplete(item);
                //スコアの追加処理
                GameManager.AddScore(item.ItemScore);
            }

        }
        return null;
    }
}
