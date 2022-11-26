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
    /// <param name="item">�A�C�e��</param>
    /// <returns></returns>
    public ItemData ReceiveItems(ItemData item)
    {
        for (int i = 0; i < _orderScript.OrderDatas.Length; i++)
        {
            if (_orderScript.OrderDatas[i].ResultItem == item.ItemName) 
            {
                //�I�[�_�[����Y���̃A�C�e�����폜
                _orderScript.OrderComplete(item);
                //�X�R�A�̒ǉ�����
                GameManager.AddScore(item.ItemScore);
            }

        }
        return null;
    }
}
