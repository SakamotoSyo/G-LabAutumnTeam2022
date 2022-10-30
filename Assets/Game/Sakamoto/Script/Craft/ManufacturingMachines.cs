using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class ManufacturingMachines : MonoBehaviour, IAddItem
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase _syntheticData;
    [Header("アイテムのデータ")]
    [SerializeField] ItemDataBase _itemDataBase;
    [Header("アイテムの加工時間")]
    [SerializeField] float _waitSeconds;
    //[Header("爆発するまでの時間")]

    [Tooltip("製造が終了したかどうか")]
    bool _manufactureBool;
    [Tooltip("製造中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    List<ItemData> _itemList = new List<ItemData>();
    [Tooltip("合成後のアイテム")]
    ItemData _resultSynthetic;
    [Tooltip("製造機が保存できるアイテムの数")]
    readonly int _itemSaveNum = 3;

    async void Update()
    {
        await ManufactureDeley();
    }

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="item"></param>
    public ItemData ReceiveItems(ItemData item)
    {
        //アイテムがマシンの許与量を超えていたらfalseを返す
        if (_itemList.Count >= _itemSaveNum)
        {
            return item;
        }

        //受け取ることができる状態ならListに格納してtrueを返す
        //合成アイテムが入っているかつPlayerがアイテムを持っているときは通らない
        if (_resultSynthetic == null ||　item == null) 
        {
            //製造中は入ってこない
            if (_manufactureBool) 
            {
              _itemList.Add(item);
            }
            StartManufacture(item);
            //一時的にローカル変数にアイテムの情報を渡す
            var Item = _resultSynthetic;
            _resultSynthetic = null;
            return Item;
        }

        return item;
    }


    /// <summary>
    /// 加工開始メソッド
    /// </summary>
    void StartManufacture(ItemData item)
    {
        if (_manufactureBool && !_manufactureing)
        {
            //製造が終了しているのでアイテムを渡す
            return;

        }
        else if (!_manufactureBool && !_manufactureing)
        {
            //製造開始
            ItemManufacture(_itemList[0].name, _itemList[1].name, _itemList[2].name);
        }
        else if (!_manufactureBool && _manufactureing)
        {
            //製造が終わっていなくて製造中な場合Returnする
            _resultSynthetic = item;
            return;
        }

    }


    /// <summary>
    /// アイテムを作成するメソッド
    /// </summary>
    void ItemManufacture(string item1 = "なし", string item2 = "なし", string item3 = "なし")
    {
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++)
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i].Item1 == item1 && _syntheticData.SyntheticList[i].Item2 == item2
                && _syntheticData.SyntheticList[i].Item3 == item3)
            {
                //合成データベースからStringのデータを取得する
                var resultSynthetic = _syntheticData.SyntheticList[i].ResultItem;
                _resultSynthetic = _itemDataBase.ItemDataList.Where(x => x.ItemName == resultSynthetic).ToArray()[0];
                break;
            }
        }
        _itemList.Clear();
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
