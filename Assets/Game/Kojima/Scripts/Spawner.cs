using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("生成間隔"), SerializeField]
    float _interval = 1.0f;
    [Header("生成確立"), SerializeField]
    static float _probability = 1.0f;
    [Header("生存時間"), SerializeField]
    float _liveTime = 0;
    [Header("生成上限"), SerializeField]
    int _limitNum;

    private GameObject item = null;
    [Header("生成オブジェクト"), SerializeField]
    private GameObject _createItem = null;



    private float _elapsed;//経過時間

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
            //生成上限
            if (transform.childCount >= _limitNum) { return; }
            //アイテムの生成
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
