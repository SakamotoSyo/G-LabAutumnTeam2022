using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CriWare;
public class ItemGenerator : MonoBehaviour
{
    #region ��`
    GameObject[] _items_box;//�f�ނ̃f�[�^��ۑ�����z��̍쐬
    private int _itemCnt = 0; //���������v���t�@�u��
    static public bool _stopFrag = true;//ItemGenerator�̒�~�p�t���O(false�Œ�~)
    private float _elapsed;//�o�ߎ���
    [SerializeField]public GameObject _item_1;
    [SerializeField]public GameObject _item_2;
    [SerializeField]public GameObject _item_3;
    [SerializeField] string _beltStop;
    [SerializeField] string _beltStart;
    [SerializeField] string _beltRun;
    [SerializeField]CriAtomSource _criSource;
    [Header("�����Ԋu"), SerializeField]float _interval = 1.0f;
    [Header("�����m��"), SerializeField]static float _probability = 1.0f;
    [Header("�������"), SerializeField]int _limitNum = 2;
    [Header("��������"), SerializeField]float _liveTime = 4.0f;

    #endregion

    void Start()
    {
        _items_box = new GameObject[] {_item_1,_item_2,_item_3};
        _criSource.cueName = _beltRun;
        _criSource.Play();

    }

    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_stopFrag)
        {
            Generato();
        }
        else if (!_stopFrag)
        {
            _criSource.cueName = _beltStop;
            ;//�������Ȃ�
        }

    }
    #region ����
    void Generato()
    {
        for (int i = 0; i < _limitNum; i++)
        {
            if (_elapsed > _interval)
            {
            _elapsed = 0;

            //Instanciate
                GameObject res = Instantiate(_items_box[_itemCnt], transform.position, transform.rotation);
                res.AddComponent<Rigidbody2D>();
                _itemCnt++;
                if (_itemCnt == 3) _itemCnt = 0;   
                res.AddComponent<ItemMoveCon>();
                Destroy(res, _liveTime);//��莞�Ԃō폜
            }
        }
    }
    #endregion
    
}
