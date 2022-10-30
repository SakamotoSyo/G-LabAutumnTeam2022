using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    [Header("���̃t�F�[�Y�܂łɂ����鎞��")]
    [SerializeField] float _phaseTime;
    [Header("�t�F�[�Y���Ƃׂ̍��Ȑݒ������List")]
    [Range(0,5),SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();

    [Tooltip("���ݏo����Ă���I�[�_�[���i�[����List")]
    List<ItemData> _orderDatas = new List<ItemData>();

    void Start()
    {
        
    }

    void Update()
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
