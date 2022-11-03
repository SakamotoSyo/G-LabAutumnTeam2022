using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    [Header("次のフェーズまでにかかる時間")]
    [SerializeField] float _phaseTime;
    [Header("フェーズごとの細かな設定をするList")]
    [Range(0,5),SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();
    [Header("合成データ")]
    [SerializeField] ItemSyntheticDataBase _itemSyntheticData;

    [Tooltip("現在出されているオーダーを格納するList")]
    ItemSynthetic[] _orderDatas = new ItemSynthetic[4];
    [Tooltip("オーダーをだすところ")]
    TakeOrdersScript[] _takeOrdersCs = new TakeOrdersScript[4];
    
    [Tooltip("現在のフェーズ")]
    float _phaseNum;
    bool _isStart;

    void Start()
    {
        GameManager.GameStart += StartOrder;
    }

    void Update()
    {
        if (_isStart) 
        {
            var NowOrder = 0;
            for (int i = 0; i < _orderDatas.Length; i++) 
            {
                if (_orderDatas[i] != null) 
                {
                    NowOrder++;
                }
            }

            //オーダーが何もなかった場合
            if (NowOrder == 0)
            {
                //合成データからランダムに合成データを取得
                var odrItem = _itemSyntheticData.GetRandamSyntheticData();
                _orderDatas[0] = odrItem;
                //注文を出す
                _takeOrdersCs[0].TakeOrders();
            }


        }
    }

    /// <summary>
    /// ゲーム開始時オーダーを開始する
    /// </summary>
    void StartOrder() 
    {

    }
}

class PhaseOrder
{
    [Header("オーダーを出す感覚")]
    [SerializeField] float _orderInterval;
    [Header("オーダーを失敗までの時間")]
    [SerializeField] float _orderTime;
    [Header("このフェーズ出すアイテム")]
    [SerializeField] List<ItemData> _itemDataList = new List<ItemData>();
} 
