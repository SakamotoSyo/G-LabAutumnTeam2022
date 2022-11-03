using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    [Header("���̃t�F�[�Y�܂łɂ����鎞��")]
    [SerializeField] float _phaseTime;
    [Header("�t�F�[�Y���Ƃׂ̍��Ȑݒ������List")]
    [Range(0,5),SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();
    [Header("�����f�[�^")]
    [SerializeField] ItemSyntheticDataBase _itemSyntheticData;

    [Tooltip("���ݏo����Ă���I�[�_�[���i�[����List")]
    ItemSynthetic[] _orderDatas = new ItemSynthetic[4];
    [Tooltip("�I�[�_�[�������Ƃ���")]
    TakeOrdersScript[] _takeOrdersCs = new TakeOrdersScript[4];
    
    [Tooltip("���݂̃t�F�[�Y")]
    float _phaseNum;
    bool _isStart;

    void Start()
    {
        GameManager.GameStart += StartOrder;
    }

    void Update()
    {
        if (_isStart) 
        {
            var NowOrder = 0;
            for (int i = 0; i < _orderDatas.Length; i++) 
            {
                if (_orderDatas[i] != null) 
                {
                    NowOrder++;
                }
            }

            //�I�[�_�[�������Ȃ������ꍇ
            if (NowOrder == 0)
            {
                //�����f�[�^���烉���_���ɍ����f�[�^���擾
                var odrItem = _itemSyntheticData.GetRandamSyntheticData();
                _orderDatas[0] = odrItem;
                //�������o��
                _takeOrdersCs[0].TakeOrders();
            }


        }
    }

    /// <summary>
    /// �Q�[���J�n���I�[�_�[���J�n����
    /// </summary>
    void StartOrder() 
    {

    }
}

class PhaseOrder
{
    [Header("�I�[�_�[���o�����o")]
    [SerializeField] float _orderInterval;
    [Header("�I�[�_�[�����s�܂ł̎���")]
    [SerializeField] float _orderTime;
    [Header("���̃t�F�[�Y�o���A�C�e��")]
    [SerializeField] List<ItemData> _itemDataList = new List<ItemData>();
} 
