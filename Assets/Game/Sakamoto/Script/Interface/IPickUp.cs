using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp
{
    /// <summary>
    /// 自分自身のアイテムデータを返す
    /// </summary>
    /// <returns></returns>
    public ItemData PickUpItem();
}
