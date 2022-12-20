using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    /// <summary>現在出されているオーダーを格納するList</summary>
    public ItemSynthetic[] OrderDatas => _orderDatas;

    [Header("次のフェーズまでにかかる時間")]
    [SerializeField] float _phaseTime;
    [Header("フェーズごとの細かな設定をするList")]
    [SerializeField] List<PhaseOrder> _phaseSetting = new List<PhaseOrder>();
    [Header("合成データ")]
    [SerializeField] ItemSyntheticDataBase _itemSyntheticData;

    [Tooltip("現在出されているオーダーを格納するList")]
    ItemSynthetic[] _orderDatas = new ItemSynthetic[5];
    [Header("オーダーをだすところ")]
    [SerializeField] TakeOrdersScript[] _takeOrdersCs = new TakeOrdersScript[4];

    [Tooltip("現在のフェーズ")]
    int _phaseNum = 0;
    bool _isStart;

    Coroutine _orderCor;
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
                var odrItem = _phaseSetting[_phaseNum].ItemDataList[Random.Range(0, _phaseSetting[_phaseNum].ItemDataList.Count)];
                _orderDatas[0] = odrItem;
                //注文を出す
                _takeOrdersCs[0].TakeOrders(odrItem, _phaseSetting[_phaseNum]);
                Debug.Log("注文は言った");
                //注文を出したので一度コルーチンを止めてやり直し
                StopCoroutine(_orderCor);
                _orderCor = StartCoroutine(OrderCor());
            }


        }
    }

    /// <summary>
    /// ゲーム開始時オーダーを開始する
    /// </summary>
    void StartOrder()
    {
        _orderCor = StartCoroutine(OrderCor());
        _isStart = true;
    }

    /// <summary>
    /// 一定時間後に注文を出すコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator OrderCor()
    {
        //現在のフェーズ分待機する
        yield return new WaitForSeconds(_phaseSetting[_phaseNum].OrderInterval);
        for (int i = 0; i < _phaseSetting[_phaseNum].OrderNum; i++)
        {
            Debug.Log(_phaseSetting[_phaseNum].OrderNum);
            if (_orderDatas[i] == null)
            {
                //合成データからランダムに合成データを取得
                var odrItem = _phaseSetting[_phaseNum].ItemDataList[Random.Range(0, _phaseSetting[_phaseNum].ItemDataList.Count)];
                _orderDatas[i] = odrItem;
                //注文を出す
                _takeOrdersCs[i].TakeOrders(odrItem, _phaseSetting[_phaseNum]);

                 _orderCor = StartCoroutine(OrderCor());
               
                break;
            }
        }
    }

    /// <summary>
    /// 現在のオーダーのデータを削除する
    /// </summary>
    public void OrderDataDelete(int dataNum)
    {
        Debug.Log("データを削除しました");
        _orderDatas[dataNum] = null;

    }

    /// <summary>
    /// オーダーが完了したときに受け付ける処理
    /// 別関数から呼び出す
    /// </summary>
    public void OrderComplete(ItemInformation item)
    {
        for (int i = 0; i < _orderDatas.Length; i++)
        {
            if (_orderDatas[i] == null)
            {
                continue;
            }
            else if (item.Item.ItemName == _orderDatas[i].ResultItem)
            {
                _orderDatas[i] = null;
                _takeOrdersCs[i].TakeOrderFalse();
                Debug.Log(item.Item.ItemName);
                //スコアを足す処理
                break;
            }
        }

        //もし何もなかったらの処理を書きたかったらここに追記
    }
}

[System.Serializable]
public class PhaseOrder
{
    [Tooltip("オーダーを出す感覚")]
    public float OrderInterval;
    [Tooltip("オーダーを失敗までの時間")]
    public float OrderTime;
    [Tooltip("オーダーをいくつまで出すか")]
    public int OrderNum;
    [Tooltip("このフェーズ出すアイテム合成データ")]
    public List<ItemSynthetic> ItemDataList = new List<ItemSynthetic>();
}
