using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class TakeOrdarView : MonoBehaviour
{
    [Header("�I�[�_�[�\��SpriteRenderer")]
    [SerializeField] Image[] _orderSpriteArray = new Image[4];
    [SerializeField] ItemDataBase _itemDataBase;

    [Header("Time��RectTransform")]
    [SerializeField] RectTransform _rectCurrent;
    [Tooltip("Time�o�[�Œ��̒���")]
    float _maxHpWidth;
    [Tooltip("Time�o�[�̍ő�l")]
    float _maxTime;

    private void Awake()
    {
        _maxHpWidth = _rectCurrent.sizeDelta.x;
    }

    public void SetRenderer(ItemSynthetic syntheticData)
    {
        if (syntheticData != null)
        {
            Debug.Log(syntheticData.ResultItem);
            _orderSpriteArray[0].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.ResultItem).ToArray()[0].ItemSprite;
            _orderSpriteArray[1].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item1).ToArray()[0].ItemSprite;
            if (syntheticData.Item2 != "�Ȃ�")
            {
                _orderSpriteArray[2].sprite = _itemDataBase.ItemDataList.Where(x => x.ItemName == syntheticData.Item2).ToArray()[0].ItemSprite;
            }

        }
    }

    public void MaxTimeSet(float MaxTime)
    {
       _maxTime = MaxTime;
    }

    public void SetTimeCurrent(float currentTime) 
    {
        //�o�[�̒������X�V
        _rectCurrent.SetWidth(GetWidth(currentTime));
        if (currentTime < 0)
        {
            ActiveBool(false);
        }
        else 
        {
            ActiveBool(true);
        }
    }

    private float GetWidth(float value)
    {
        float width = Mathf.InverseLerp(0, _maxTime, value);
        return Mathf.Lerp(0, _maxHpWidth, width);
    }

    private void ActiveBool(bool active) 
    {
        for (int i = 0; i < _orderSpriteArray.Length; i++)
        {
            _orderSpriteArray[i].enabled = active;
        }
    }

}

public static class UIExtensions
{
    /// <summary>
    /// ���݂̒l��Rect�ɃZ�b�g����
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}
