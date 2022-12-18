using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("自分のオーダー枠の番号")]
    [SerializeField] int _orderNum;
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase syntheticData;
    [Header("オーダーを出しているスクリプト")]
    [SerializeField] OrderScript _orderCs;
    [Header("表示するスプライト")]
    [SerializeField] Image _image;
    public IObservable<ItemSynthetic> CurrentSynthetic => _nowSyntheticData;
    [Tooltip("現在オーダーを出しているデータ")]
    private readonly ReactiveProperty<ItemSynthetic> _nowSyntheticData = new();
    public IObservable<float> MaxOrderTime => _maxOrderTime;
    private readonly ReactiveProperty<float> _maxOrderTime = new();
    public IObservable<float> CountOrderTime => _countOrderTime;
    private readonly ReactiveProperty<float> _countOrderTime = new();

    [Tooltip("このオーダーの設定")]
    PhaseOrder _orderSetting;
    [Tooltip("オーダーの制限時間を管理する")]
    Coroutine _orderCor;

    void Start()
    {
        _image.enabled = false;
    }

    void Update()
    {
       
    }

    /// <summary>
    /// オーダを受ける関数
    /// </summary>
    public void TakeOrders(ItemSynthetic synthetic, PhaseOrder phase) 
    {
        _nowSyntheticData.Value = synthetic;
        _orderSetting = phase;
        //オーダーの制限時間計測開始
        _orderCor = StartCoroutine(TakeOrdersStart());

    }

    /// <summary>
    ///　受けたオーダーをスタートする
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeOrdersStart() 
    {
        _image.enabled = true;
        _maxOrderTime.Value = _orderSetting.OrderTime;
        _countOrderTime.Value = _orderSetting.OrderTime;

        while (_countOrderTime.Value > 0) 
        {
            _countOrderTime.Value -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("オーダー失敗");
        //まず自分の番号を渡してオーダーデータを削除
        _nowSyntheticData.Value = null;
        _orderCs.OrderDataDelete(_orderNum);
        _image.enabled = false;
        //スコアを減らす処理をここに書く

    }

    /// <summary>
    /// 受けたオーダーを終了する
    /// </summary>
    public void TakeOrderFalse() 
    {
        _nowSyntheticData.Value = null;
        _image.enabled = false;
    }

}
