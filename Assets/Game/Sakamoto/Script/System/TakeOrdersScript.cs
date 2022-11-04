using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("�����̃I�[�_�[�g�̔ԍ�")]
    [SerializeField] int _orderNum;
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase syntheticData;
    [Header("�I�[�_�[���o���Ă���X�N���v�g")]
    [SerializeField] OrderScript _orderCs;
    [Header("�\������X�v���C�g")]
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Tooltip("���݃I�[�_�[���o���Ă���f�[�^")]
    ItemSynthetic _nowSyntheticData;
    [Tooltip("���̃I�[�_�[�̐ݒ�")]
    PhaseOrder _orderSetting;
    [Tooltip("�I�[�_�[�̐������Ԃ��Ǘ�����")]
    Coroutine _orderCor;

    void Start()
    {
        _spriteRenderer.enabled = false;
    }

    void Update()
    {

    }

    /// <summary>
    /// �I�[�_���󂯂�֐�
    /// </summary>
    public void TakeOrders(ItemSynthetic synthetic, PhaseOrder phase) 
    {
        _nowSyntheticData = synthetic;
        _orderSetting = phase;
        Debug.Log("TakeOrder");
        //�I�[�_�[�̐������Ԍv���J�n
        _orderCor = StartCoroutine(TakeOrdersStart());
    }

    /// <summary>
    ///�@�󂯂��I�[�_�[���X�^�[�g����
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeOrdersStart() 
    {
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_orderSetting.OrderTime);
        //�I�[�_�[���s
        Debug.Log("�I�[�_�[���s");
        //�܂������̔ԍ���n���ăI�[�_�[�f�[�^���폜
        _orderCs.OrderDataDelete(_orderNum);
        _spriteRenderer.enabled = false;
        //�X�R�A�����炷�����������ɏ���

    }

}
