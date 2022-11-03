using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrdersScript : MonoBehaviour
{
    [Header("アイテムの合成データ")]
    [SerializeField] ItemSyntheticDataBase syntheticData;

    [Tooltip("現在オーダーを出しているデータ")]
    ItemSynthetic _nowSyntheticData;
    [Tooltip("定期的にオーダーを出す為のコルーチン")]
    Coroutine _orderCor;
    [Tooltip("オーダーを出す感覚")]
    float _orderTime;

    void Start()
    {
        GameManager.GameStart += StartOrder;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// オーダを受ける関数
    /// </summary>
    public void TakeOrders() 
    {

    }

    /// <summary>
    /// ゲーム開始呼ばれるオーダー
    /// </summary>
    void StartOrder() 
    {

    }

    /// <summary>
    /// 一定時間後にオーダーを出す
    /// </summary>
    /// <returns></returns>
    IEnumerator RegularOrders() 
    {
        yield return new WaitForSeconds(_orderTime);
        //注文
        TakeOrders();
    }


}
