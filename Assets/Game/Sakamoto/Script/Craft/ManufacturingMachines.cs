using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("アイテムの加工時間")]
    [SerializeField] float _waitSeconds;
    [Header("何秒たったら熱暴走で爆発するか")]
    [SerializeField] float _runawayTime = 5;

    [Tooltip("製造が終了したかどうか")]
    bool _manufactureBool;
    [Tooltip("製造中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemData[] _itemArray;
    [Tooltip("合成後のアイテム")]
    ItemData _resultSynthetic;
    [Tooltip("製造機が保存できるアイテムの数")]
    readonly int _itemSaveNum = 3;

    float _runawayCount;

    void Start()
    {
        _itemArray = new ItemData[_itemSaveNum];
    }


    async void Update()
    {
        await ManufactureDeley();

        if (_resultSynthetic != null)
        {

        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            StartManufacture();
        }
    }

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        if (_resultSynthetic != null && item == null)
        {
            return _resultSynthetic;
        }
        else if (_resultSynthetic != null) 
        {
            return item;
        }


        //アイテムがマシンの許与量を超えていたらアイテムを返す
        if (_itemArray[2] != null) 
        {
            Debug.Log("アイテムを返すマス");
            return item;
        }
        //製造中は入ってこない
        if (!_manufactureBool && item != null)
        {
            Debug.Log("入ってきた");
            for (int i = 0; i < _itemSaveNum; i++) 
            {
                if (_itemArray[i] == null) 
                {
                    _itemArray[i] = item;
                    break;
                }
            }
            return null;
        }

        return item;

    }


    /// <summary>
    /// 加工開始メソッド
    /// </summary>
    void StartManufacture()
    {
        if (_manufactureBool && !_manufactureing)
        {
            //製造が終了しているのでアイテムを渡す
            return;

        }
        else if (!_manufactureBool && !_manufactureing)
        {
            //製造開始
            ItemManufacture();
        }
        

    }


    /// <summary>
    /// アイテムを作成するメソッド
    /// </summary>
    void ItemManufacture()
    {
        //合成用の配列を作る
        string[] itemArray = new string[_itemSaveNum];
        for (int i = 0; i < _itemSaveNum; i++) 
        {
            if (_itemArray[i] != null)
            {
                itemArray[i] = _itemArray[i].ItemName;
            }
            else 
            {
                itemArray[i] = "なし";
            }
        }
    
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i].Item1 == itemArray[0] && _syntheticData.SyntheticList[i].Item2 == itemArray[1]
                && _syntheticData.SyntheticList[i].Item3 == itemArray[2])
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = _itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0];
                //Debug.Log($"結果は{_resultSynthetic}");
                break;
            }
            //Debug.Log($"結果判定中:アイテム１{itemArray[0]}、アイテム２:{itemArray[1]}、アイテム３:{itemArray[2]}");
        }
        Array.Clear(_itemArray, 0, _itemArray.Length);
    }


    /// <summary>
    /// 製造が開始されたら待機する
    /// </summary>
    /// <returns></returns>
    async UniTask ManufactureDeley()
    {
        if (_manufactureing)
        {
            Debug.Log("製造中wait");
            await UniTask.Delay(TimeSpan.FromSeconds(_waitSeconds));
            _manufactureing = false;
        }
    }
}


////アイテムがマシンの許与量を超えていたらfalseを返す
//if (_itemList.Count >= _itemSaveNum)
//{
//    return item;
//}

////受け取ることができる状態ならListに格納してtrueを返す
////合成アイテムが入っているかつPlayerがアイテムを持っているときは通らない
//if (_resultSynthetic == null || item == null)
//{
//    //製造中は入ってこない
//    if (_manufactureBool && item == null)
//    {
//        _itemList.Add(item);
//        return null;
//    }
//    //StartManufacture(item);
//    //一時的にローカル変数にアイテムの情報を渡す
//    var Item = _resultSynthetic;
//    _resultSynthetic = null;
//    return Item;
//}

//return item;
