using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGenerator : MonoBehaviour
{
    
     GameObject[] _items_box;//素材のデータを保存する配列の作成
    public GameObject _item_prefab;//配列に入れるために生成するプレファブ
    private int _itemCnt; //生成したプレファブ数
    static public bool _stopFrag = true;//ItemGeneratorの停止用フラグ(falseで停止)
    private float _elapsed;//経過時間
    public GameObject _item_1;
    public GameObject _item_2;
    public GameObject _item_3;
    Rigidbody2D _rb2d;
    [Header("生成間隔"), SerializeField]float _interval = 1.0f;
    [Header("生成確立"), SerializeField]static float _probability = 1.0f;
    [Header("生成上限"), SerializeField]int _limitNum;
    [Header("生存時間"), SerializeField]float _liveTime = 0;
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
            ;//何もしない
        }

    }
    void Generato()
    {
        if (_elapsed > _interval)
        {
            _elapsed = 0;

            //生成上限
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
