using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGenerator : MonoBehaviour
{
    
     GameObject[] _items_box;//�f�ނ̃f�[�^��ۑ�����z��̍쐬
    public GameObject _item_prefab;//�z��ɓ���邽�߂ɐ�������v���t�@�u
    private int _itemCnt; //���������v���t�@�u��
    static public bool _stopFrag = true;//ItemGenerator�̒�~�p�t���O(false�Œ�~)
    private float _elapsed;//�o�ߎ���
    public GameObject _item_1;
    public GameObject _item_2;
    public GameObject _item_3;
    Rigidbody2D _rb2d;
    [Header("�����Ԋu"), SerializeField]float _interval = 1.0f;
    [Header("�����m��"), SerializeField]static float _probability = 1.0f;
    [Header("�������"), SerializeField]int _limitNum;
    [Header("��������"), SerializeField]float _liveTime = 0;
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
    void Generato()
    {
        if (_elapsed > _interval)
        {
            _elapsed = 0;

            //�������
            if (transform.childCount >= _limitNum) { return; }
            //Instanciate
            GameObject gameObject = Instantiate(_item_prefab);

            ItemState();
        }
    }
    void ItemState()
    {
        // item.transform.SetParent(transform, false);
        _liveTime += Time.deltaTime;
        var x = Random.Range(-8, 8);
        var y = Random.Range(3, 10);
        _rb2d = _item_prefab.GetComponent<Rigidbody2D>();
        _rb2d.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        if (_liveTime > 2.0f)
        {
            Destroy(gameObject);
            Debug.Log("destory");
        }

    }

}
