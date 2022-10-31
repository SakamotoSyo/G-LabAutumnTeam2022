using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("�����Ԋu"), SerializeField]
    float _interval = 1.0f;
    [Header("�����m��"), SerializeField]
    static float _probability = 1.0f;
    [Header("��������"), SerializeField]
    float _liveTime = 0;
    [Header("�������"), SerializeField]
    int _limitNum;

    private GameObject item = null;
    [Header("�����I�u�W�F�N�g"), SerializeField]
    private GameObject _createItem = null;



    private float _elapsed;//�o�ߎ���

    Rigidbody2D _rb2d;
    void Start()
    {
        
    }

    
    void Update()
    {
        _elapsed = Time.deltaTime;
  
        if (_elapsed > _interval)
        {
            _elapsed = 0;
            Debug.Log("aaaaaa");
            //�������
            if (transform.childCount >= _limitNum) { return; }
            //�A�C�e���̐���
            item = Instantiate(_createItem.gameObject);
            ItemState();
        }
    }
    void ItemState()
    {
        
        _liveTime += Time.deltaTime;
        var x = Random.Range(-8, 8);
        var y = Random.Range(3, 10);
        _rb2d = item.GetComponent<Rigidbody2D>();
        _rb2d.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        if (_liveTime > 2.0f)
        {
            Destroy(gameObject);
            Debug.Log("destory");
        }

    }
}
