using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGenerator : MonoBehaviour
{
    #region ��`
    GameObject[] _items_box;//�f�ނ̃f�[�^��ۑ�����z��̍쐬
    private int _itemCnt = 0; //���������v���t�@�u��
    static public bool _stopFrag = true;//ItemGenerator�̒�~�p�t���O(false�Œ�~)
    private float _elapsed;//�o�ߎ���
    public GameObject _item_1;
    public GameObject _item_2;
    public GameObject _item_3;
    [Header("�����Ԋu"), SerializeField]float _interval = 1.0f;
    [Header("�����m��"), SerializeField]static float _probability = 1.0f;
    [Header("�������"), SerializeField]int _limitNum = 2;
    [Header("��������"), SerializeField]float _liveTime = 0;
    #endregion
    
    void Start()
    {
        _items_box = new GameObject[] {_item_1,_item_2,_item_3};
        
    }

    // Update is called once per frame
    void Update()
    {
        _elapsed += Time.deltaTime;
        if ( _stopFrag )Generato();
        else if (!_stopFrag) 
        {
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
            
                Debug.Log(_itemCnt);
                GameObject res = Instantiate(_items_box[_itemCnt]);
                res.AddComponent<Rigidbody2D>();
                _itemCnt++;
                if (_itemCnt == 3) _itemCnt = 0;   
                res.AddComponent<ItemMoveCon>();
            }
        }
    }
    #endregion
    
}