using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("�A�C�e���̍����f�[�^")]
    [SerializeField] ItemSyntheticDataBase syntheticData;

    [Tooltip("���݃I�[�_�[���o���Ă���f�[�^")]
    ItemSynthetic _nowSyntheticData;
    [Tooltip("����I�ɃI�[�_�[���o���ׂ̃R���[�`��")]
    Coroutine _orderCor;
    [Tooltip("�I�[�_�[���o�����o")]
    float _orderTime;

    void Start()
    {
        GameManager.GameStart += StartOrder;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �I�[�_���󂯂�֐�
    /// </summary>
    public void TakeOrders() 
    {

    }

    /// <summary>
    /// �Q�[���J�n�Ă΂��I�[�_�[
    /// </summary>
    void StartOrder() 
    {

    }

    /// <summary>
    /// ��莞�Ԍ�ɃI�[�_�[���o��
    /// </summary>
    /// <returns></returns>
    IEnumerator RegularOrders() 
    {
        yield return new WaitForSeconds(_orderTime);
        //����
        TakeOrders();
    }


}
