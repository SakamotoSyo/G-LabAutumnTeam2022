using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    [Header("次のフェーズまでにかかる時間")]
    [SerializeField] float _phaseTime;
    [Header("フェーズごとの細かな設定をするList")]
    [Range(0,5),SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();

    [Tooltip("現在出されているオーダーを格納するList")]
    List<ItemData> _orderDatas = new List<ItemData>();

    void Start()
    {
        
    }

    void Update()
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
