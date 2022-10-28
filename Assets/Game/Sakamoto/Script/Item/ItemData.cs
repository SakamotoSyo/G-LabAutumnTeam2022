using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemData")]
public class ItemData : MonoBehaviour
{
    [Header("アイテム名")]
    [SerializeField] public string ItemName;

    [Header("加工できるかどうか")]
    [SerializeField] public bool Processing;

    [Header("製造できるかどうか")]
    [SerializeField] public bool Craft;

    [Header("梱包できるか")]
    [SerializeField] public bool Packing;

    [Header("アイテムデータのプレハブ")]
    [SerializeField] public GameObject ItemPrefab;

}
