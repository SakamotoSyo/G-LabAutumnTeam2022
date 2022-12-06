using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGenerator : MonoBehaviour
{
    #region 定義
    GameObject[] _items_box;//素材のデータを保存する配列の作成
    private int _itemCnt = 0; //生成したプレファブ数
    static public bool _stopFrag = true;//ItemGeneratorの停止用フラグ(falseで停止)
    private float _elapsed;//経過時間
    public GameObject _item_1;
    public GameObject _item_2;
    public GameObject _item_3;
    [Header("生成間隔"), SerializeField]float _interval = 1.0f;
    [Header("生成確立"), SerializeField]static float _probability = 1.0f;
    [Header("生成上限"), SerializeField]int _limitNum = 2;
    [Header("生存時間"), SerializeField]float _liveTime = 0;
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
            ;//何もしない
        }

    }
    #region 生成
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
            }
        }
    }
    #endregion
    
}
