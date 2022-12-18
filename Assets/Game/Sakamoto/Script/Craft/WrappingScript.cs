using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem, ICraftItem
{
    [Header("包装が始まった時の煙")]
    [SerializeField] Sprite _smokeSprite;
    [SerializeField] SpriteRenderer _sr;
    [SerializeField] Sprite[] _presentSprites = new Sprite[4];

    [Tooltip("加工中かどうか")]
    bool _manufactureing;
    [Tooltip("アイテムを保存しておく変数")]
    ItemInformation _itemData;
    [Tooltip("合成後のアイテム")]
    ItemInformation _resultSynthetic;

    /// <summary>
    /// Itemを受け取るメソッド
    /// アイテムを渡せるかどうか戻り値で返すのでそれで今の持ち物を破棄するか判断してほしい
    /// </summary>
    /// <param name="itemInfo"></param>
    public ItemInformation ReceiveItems(ItemInformation itemInfo)
    {
        if (_manufactureing) return itemInfo;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_resultSynthetic != null && itemInfo == null)
        {
            //アイテムがNullになった時アイテムを返す
            var returnItem = _resultSynthetic;
            _resultSynthetic = null;
            _sr.sprite = null;
            return returnItem;
        }
        else if (_resultSynthetic != null)
        {
            return itemInfo;
        }


        //アイテムがマシンの許与量を超えていたらまたはアイテムが入っていなかったらアイテムを返す
        if (_itemData != null || itemInfo == null)
        {
            Debug.Log("アイテムを返すマス");
            return itemInfo;
        }
        //アイテムがNullではないときかつクラフトできるものだったら
        if (_itemData == null && itemInfo.Item.Packing && !itemInfo.Present)
        {
            Debug.Log("入ってきた");
            //アイテムが入ったことでクラフト待機スタート
            _itemData = itemInfo;
            _sr.sprite = itemInfo.Item.ItemSprite;
            return null;
        }

        return itemInfo;

    }
    /// <summary>
    /// クラフトをスタートする
    /// </summary>
    public float Craft()
    {
        if (_itemData == null) return 0;
        Debug.Log("クラフトスタート");
        AudioManager.Instance.PlaySound(SoundPlayType.SE_packing);
        _manufactureing = true;
        //加工が始まったら煙を出す
        _sr.sprite = _smokeSprite;
        return _itemData.Item.CraftTime;

    }

    /// <summary>
    /// クラフトが終った時に呼ぶ
    /// </summary>
    public void CraftEnd()
    {
        _resultSynthetic = new ItemInformation( _itemData.Item, true);
        _sr.sprite = PresentJudge();
        _resultSynthetic.PresentSet(PresentJudge());
        _itemData = null;
        _manufactureing = false;
    }


    Sprite PresentJudge() 
    {
        if (_itemData.Item.ItemParts == 0) 
        {
            _resultSynthetic.SetItemScore(0);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && !_itemData.IsFineQuality && _itemData.PartsNum == 0)
        {
            _resultSynthetic.SetItemScore(10);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && !_itemData.IsFineQuality && _itemData.PartsNum == 1)
        {
            _resultSynthetic.SetItemScore(30);
            return _presentSprites[0];
        }
        else if (_itemData.Item.ItemParts == 1 && _itemData.IsFineQuality && _itemData.PartsNum == 1)
        {
            _resultSynthetic.SetItemScore(50);
            return _presentSprites[1];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.Item.ItemParts == 0)
        {
            _resultSynthetic.SetItemScore(50);
            return _presentSprites[1];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.Item.ItemParts == 1)
        {
            _resultSynthetic.SetItemScore(100);
            return _presentSprites[2];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.PartsNum == 2 && _itemData.IsFineQuality)
        {
            _resultSynthetic.SetItemScore(200);
            return _presentSprites[3];
        }
        else if (_itemData.Item.ItemParts == 2 && _itemData.PartsNum == 2 && !_itemData.IsFineQuality)
        {
            _resultSynthetic.SetItemScore(150);
            return _presentSprites[2];
        }

        return _presentSprites[0];
    }
}
