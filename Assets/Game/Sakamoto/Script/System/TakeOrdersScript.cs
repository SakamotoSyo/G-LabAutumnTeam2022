using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("�����̃I�[�_�[�g�̔ԍ�")]
    [SerializeField] int _orderNum;
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase syntheticData;
    [Header("�I�[�_�[���o���Ă���X�N���v�g")]
    [SerializeField] OrderScript _orderCs;
    [Header("�\������X�v���C�g")]
    [SerializeField] Image _image;
    public IObservable<ItemSynthetic> CurrentSynthetic => _nowSyntheticData;
    [Tooltip("���݃I�[�_�[���o���Ă���f�[�^")]
    private readonly ReactiveProperty<ItemSynthetic> _nowSyntheticData = new();
    public IObservable<float> MaxOrderTime => _maxOrderTime;
    private readonly ReactiveProperty<float> _maxOrderTime = new();
    public IObservable<float> CountOrderTime => _countOrderTime;
    private readonly ReactiveProperty<float> _countOrderTime = new();

    [Tooltip("���̃I�[�_�[�̐ݒ�")]
    PhaseOrder _orderSetting;
    [Tooltip("�I�[�_�[�̐������Ԃ��Ǘ�����")]
    Coroutine _orderCor;

    void Start()
    {
        _image.enabled = false;
    }

    void Update()
    {
       
    }

    /// <summary>
    /// �I�[�_���󂯂�֐�
    /// </summary>
    public void TakeOrders(ItemSynthetic synthetic, PhaseOrder phase) 
    {
        _nowSyntheticData.Value = synthetic;
        _orderSetting = phase;
        //�I�[�_�[�̐������Ԍv���J�n
        _orderCor = StartCoroutine(TakeOrdersStart());

    }

    /// <summary>
    ///�@�󂯂��I�[�_�[���X�^�[�g����
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeOrdersStart() 
    {
        _image.enabled = true;
        _maxOrderTime.Value = _orderSetting.OrderTime;
        _countOrderTime.Value = _orderSetting.OrderTime;

        while (_countOrderTime.Value > 0) 
        {
            _countOrderTime.Value -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("�I�[�_�[���s");
        //�܂������̔ԍ���n���ăI�[�_�[�f�[�^���폜
        _nowSyntheticData.Value = null;
        _orderCs.OrderDataDelete(_orderNum);
        _image.enabled = false;
        //�X�R�A�����炷�����������ɏ���

    }

    /// <summary>
    /// �󂯂��I�[�_�[���I������
    /// </summary>
    public void TakeOrderFalse() 
    {
        _nowSyntheticData.Value = null;
        _image.enabled = false;
    }

}
