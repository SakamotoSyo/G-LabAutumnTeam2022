using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingScript : MonoBehaviour, IAddItem
{
    [Header("�I�[�_�[���Ǘ�����X�N���v�g")]
    [SerializeField] OrderScript _orderScript;
    /// <summary>
    /// �A�C�e���󂯓n���̏���
    /// </summary>
    /// <param name="itemInfo">�A�C�e��</param>
    /// <returns></returns>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        for (int i = 0; i < _orderScript.OrderDatas.Length; i++)
        {
            if (_orderScript.OrderDatas[i] == null) continue;

            if (_orderScript.OrderDatas[i].ResultItem == itemInfo.Item.ItemName) 
            {
                //�I�[�_�[����Y���̃A�C�e�����폜
                _orderScript.OrderComplete(itemInfo);
                //�X�R�A�̒ǉ�����
                GameManager.AddScore(itemInfo.Item.ItemScore);
            }

        }
        return null;
    }
}
