using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ManufacturingMachines : MonoBehaviour
{
    [Header("アイテムの合成データ")]
    [SerializeField]ItemSyntheticDataBase _syntheticData;

    [Header("アイテムの加工時間")]
    [SerializeField] WaitForSeconds _waitSeconds;

    [Tooltip("製造が終了したかどうか")]
    bool _manufactureBool;
    [Tooltip("製造中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]   
    string _item1;
    string _item2;
    string _item3;
    string _resultSynthetic;
    /// <summary>
    /// 加工開始メソッド
    /// </summary>
    public void StartManufacture() 
    {
        if (_manufactureBool && !_manufactureing) 
        {
            //製造が終了したのでアイテムを渡す
            ItemManufacture(_item1, _item2, _item3);
        }
    }


    /// <summary>
    /// アイテムを作成するメソッド
    /// </summary>
    string ItemManufacture(string item1 = "なし", string item2 = "なし", string item3 = "なし") 
    {
        for (int i = 0; i < _syntheticData.SyntheticList.Count; i++) 
        {
            //アイテムの名前が一致したら
            if (_syntheticData.SyntheticList[i]._item1 == item1 && _syntheticData.SyntheticList[i]._item2 == item2
                && _syntheticData.SyntheticList[i]._item3 == item3) 
            {
                _resultSynthetic = _syntheticData.SyntheticList[i]._resultItem;
                break;
            }
        }

        return _resultSynthetic; 
    }
}
