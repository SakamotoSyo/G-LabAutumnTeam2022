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
    /// <param name="itemInfo">アイテム</param>
    /// <returns></returns>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        for (int i = 0; i < _orderScript.OrderDatas.Length; i++)
        {
            if (_orderScript.OrderDatas[i] == null) continue;

            if (_orderScript.OrderDatas[i].ResultItem == itemInfo.Item.ItemName) 
            {
                //オーダーから該当のアイテムを削除
                _orderScript.OrderComplete(itemInfo);
                //スコアの追加処理
                GameManager.AddScore(itemInfo.Item.ItemScore);
            }

        }
        return null;
    }
}
