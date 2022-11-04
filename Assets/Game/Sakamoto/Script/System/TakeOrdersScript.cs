using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("自分のオーダー枠の番号")]
    [SerializeField] int _orderNum;
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase syntheticData;
    [Header("オーダーを出しているスクリプト")]
    [SerializeField] OrderScript _orderCs;
    [Header("表示するスプライト")]
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Tooltip("現在オーダーを出しているデータ")]
    ItemSynthetic _nowSyntheticData;
    [Tooltip("このオーダーの設定")]
    PhaseOrder _orderSetting;
    [Tooltip("オーダーの制限時間を管理する")]
    Coroutine _orderCor;

    void Start()
    {
        _spriteRenderer.enabled = false;
    }

    void Update()
    {

    }

    /// <summary>
    /// オーダを受ける関数
    /// </summary>
    public void TakeOrders(ItemSynthetic synthetic, PhaseOrder phase) 
    {
        _nowSyntheticData = synthetic;
        _orderSetting = phase;
        Debug.Log("TakeOrder");
        //オーダーの制限時間計測開始
        _orderCor = StartCoroutine(TakeOrdersStart());
    }

    /// <summary>
    ///　受けたオーダーをスタートする
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeOrdersStart() 
    {
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_orderSetting.OrderTime);
        //オーダー失敗
        Debug.Log("オーダー失敗");
        //まず自分の番号を渡してオーダーデータを削除
        _orderCs.OrderDataDelete(_orderNum);
        _spriteRenderer.enabled = false;
        //スコアを減らす処理をここに書く

    }

}
