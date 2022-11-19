using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingScript : MonoBehaviour, IAddItem
{
    [Header("包装を包むのにかかる時間")]
    [SerializeField] int _wrappingTime;
    [Header("プレゼントの画像")]
    [SerializeField] Sprite _presentSp;

    [Tooltip("受け取ったアイテムを保存しておく場所")]
    ItemData _itemData;
    [Tooltip("現在制作中かどうか")]
    bool _manufactureing = false;
    [Tooltip("制作が完了したかどうか")]
    bool _compCraft;
    Coroutine _wrappnigCor;
    WaitForSeconds _wrappingWait;

    void Start()
    {
       _wrappingWait = new WaitForSeconds(_wrappingTime);
    }

    /// <summary>
    /// アイテムの受け渡し
    /// </summary>
    /// <param name="item">渡されたアイテム</param>
    /// <returns>アイテム</returns>
    public ItemData ReceiveItems(ItemData item)
    {
        if (_manufactureing && !item.Packing) return item;

        //合成後のアイテムがあるかつPlayerがアイテムを持っていないとき
        //合成アイテムを返す
        if (_compCraft && item == null)
        {
            //アイテムがNullになった時アイテムを返す
            StopCoroutine(_wrappnigCor);
            _compCraft = false;
            var saveItem = _itemData;
            _itemData = null;
            return saveItem;
        }

        //既にアイテムが置かれていた場合渡されるアイテムをそのまま返す
        if (_itemData != null) 
        {
            return item;
        }

        //アイテムデータがないとき
        if (_itemData == null) 
        {
            _itemData = item;
            _wrappnigCor = StartCoroutine(WrappingCraft());
        }

        return null;
    }

    IEnumerator WrappingCraft() 
    {
        yield return _wrappingWait;
        //入っているアイテムデータに包装済みの命令を出す
        _itemData.IsPacking = true;
        _itemData.ItemSprite = _presentSp;
        _compCraft = true;

    }

}
