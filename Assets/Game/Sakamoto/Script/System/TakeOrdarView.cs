using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class TakeOrdarView : MonoBehaviour
{
    [Header("オーダー表のSpriteRenderer")]
    [SerializeField] Image[] _orderImageArray = new Image[10];
    [SerializeField] ItemDataBase _itemDataBase;

    [Header("TimeのRectTransform")]
    [SerializeField] RectTransform _rectCurrent;
    [Tooltip("Timeバー最長の長さ")]
    float _maxHpWidth;
    [Tooltip("Timeバーの最大値")]
    float _maxTime;

    private void Awake()
    {
        _maxHpWidth = _rectCurrent.sizeDelta.x;
    }

    public void SetRenderer(ItemSynthetic syntheticData)
    {
        if (syntheticData != null)
        {
            ActiveBool(true);
            Debug.Log(syntheticData.ResultItem);
            _orderImageArray[0].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.ResultItem).ToArray()[0].ItemSprite;
            _orderImageArray[1].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item1).ToArray()[0].ItemSprite;
            _orderImageArray[2].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item1[0] + "素材").ToArray()[0].ItemSprite;
            if (syntheticData.Item2 == "なし")
            {
                _orderImageArray[3].enabled = false;
                _orderImageArray[4].enabled = false;
                _orderImageArray[3].sprite = null;
                _orderImageArray[4].sprite = null;
            }
            else
            {
                //_orderImageArray[2].enabled = true;
                _orderImageArray[3].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item2).ToArray()[0].ItemSprite;
                _orderImageArray[4].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item1[0] + "素材").ToArray()[0].ItemSprite;
            }

        }
        else 
        {
            ActiveBool(false);
        }

    }

    public void MaxTimeSet(float MaxTime)
    {
       _maxTime = MaxTime;
    }

    public void SetTimeCurrent(float currentTime) 
    {
        //バーの長さを更新
        _rectCurrent.SetWidth(GetWidth(currentTime));
        if (currentTime < 0)
        {
            ActiveBool(false);
            Debug.Log("時間切れ");
        }
    }

    private float GetWidth(float value)
    {
        float width = Mathf.InverseLerp(0, _maxTime, value);
        return Mathf.Lerp(0, _maxHpWidth, width);
    }

    private void ActiveBool(bool active) 
    {
        for (int i = 0; i < _orderImageArray.Length; i++)
        {
            _orderImageArray[i].enabled = active;
        }
    }

}

public static class UIExtensions
{
    /// <summary>
    /// 現在の値をRectにセットする
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}
