using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    /// <summary>���ݏo����Ă���I�[�_�[���i�[����List</summary>
    public ItemSynthetic[] OrderDatas => _orderDatas;

    [Header("���̃t�F�[�Y�܂łɂ����鎞��")]
    [SerializeField] float _phaseTime;
    [Header("�t�F�[�Y���Ƃׂ̍��Ȑݒ������List")]
    [SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();
    [Header("�����f�[�^")]
    [SerializeField] ItemSyntheticDataBase _itemSyntheticData;

    [Tooltip("���ݏo����Ă���I�[�_�[���i�[����List")]
    ItemSynthetic[] _orderDatas = new ItemSynthetic[5];
    [Header("�I�[�_�[�������Ƃ���")]
    [SerializeField] TakeOrdersScript[] _takeOrdersCs = new TakeOrdersScript[4];

    [Tooltip("���݂̃t�F�[�Y")]
    int _phaseNum = 0;
    bool _isStart;

    Coroutine _orderCor;
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
                var odrItem = _phaseSetting[_phaseNum].ItemDataList[Random.Range(0, _phaseSetting[_phaseNum].ItemDataList.Count)];
                _orderDatas[0] = odrItem;
                //�������o��
                _takeOrdersCs[0].TakeOrders(odrItem, _phaseSetting[_phaseNum]);
                Debug.Log("�����͌�����");
                //�������o�����̂ň�x�R���[�`�����~�߂Ă�蒼��
                StopCoroutine(_orderCor);
                _orderCor = StartCoroutine(OrderCor());
            }


        }
    }

    /// <summary>
    /// �Q�[���J�n���I�[�_�[���J�n����
    /// </summary>
    void StartOrder()
    {
        _orderCor = StartCoroutine(OrderCor());
        _isStart = true;
    }

    /// <summary>
    /// ��莞�Ԍ�ɒ������o���R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator OrderCor()
    {
        //���݂̃t�F�[�Y���ҋ@����
        yield return new WaitForSeconds(_phaseSetting[_phaseNum].OrderInterval);
        for (int i = 0; i < _phaseSetting[_phaseNum].OrderNum; i++)
        {
            Debug.Log(_phaseSetting[_phaseNum].OrderNum);
            if (_orderDatas[i] == null)
            {
                //�����f�[�^���烉���_���ɍ����f�[�^���擾
                var odrItem = _phaseSetting[_phaseNum].ItemDataList[Random.Range(0, _phaseSetting[_phaseNum].ItemDataList.Count)];
                _orderDatas[i] = odrItem;
                //�������o��
                _takeOrdersCs[i].TakeOrders(odrItem, _phaseSetting[_phaseNum]);

                 _orderCor = StartCoroutine(OrderCor());
               
                break;
            }
        }
    }

    /// <summary>
    /// ���݂̃I�[�_�[�̃f�[�^���폜����
    /// </summary>
    public void OrderDataDelete(int dataNum)
    {
        Debug.Log("�f�[�^���폜���܂���");
        _orderDatas[dataNum] = null;

    }

    /// <summary>
    /// �I�[�_�[�����������Ƃ��Ɏ󂯕t���鏈��
    /// �ʊ֐�����Ăяo��
    /// </summary>
    public void OrderComplete(ItemInformation item)
    {
        for (int i = 0; i < _orderDatas.Length; i++)
        {
            if (_orderDatas[i] == null)
            {
                continue;
            }
            else if (item.Item.ItemName == _orderDatas[i].ResultItem)
            {
                _orderDatas[i] = null;
                _takeOrdersCs[i].TakeOrderFalse();
                Debug.Log(item.Item.ItemName);
                //�X�R�A�𑫂�����
                break;
            }
        }

        //���������Ȃ�������̏������������������炱���ɒǋL
    }
}

[System.Serializable]
public class PhaseOrder
{
    [Tooltip("�I�[�_�[���o�����o")]
    public float OrderInterval;
    [Tooltip("�I�[�_�[�����s�܂ł̎���")]
    public float OrderTime;
    [Tooltip("�I�[�_�[�������܂ŏo����")]
    public int OrderNum;
    [Tooltip("���̃t�F�[�Y�o���A�C�e�������f�[�^")]
    public List<ItemSynthetic> ItemDataList = new List<ItemSynthetic>();
}
