using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemData")]
public class ItemData : MonoBehaviour
{
    [Header("加工できるかどうか")]
    [SerializeField] public bool Processing;

    [Header("製造できるかどうか")]
    [SerializeField] public bool Craft;

    [Header("アイテムデータのプレハブ")]
    [SerializeField] public GameObject ItemPrefab;

}
