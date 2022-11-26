using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("アイテム名")]
    [SerializeField] public string ItemName;

    [Header("加工できるかどうか")]
    [SerializeField] public bool Processing;

    [Header("製造できるかどうか")]
    [SerializeField] public bool Craft;

    [Header("梱包できるか")]
    [SerializeField] public bool Packing;

    [Header("アイテムのスプライト")]
    [SerializeField] public Sprite ItemSprite;

    [Header("プレゼントの画像")]
    [SerializeField] public Sprite PresentSprite;

    [Header("製造所に入れたときのこの素材が暴走を起こすまでの時間")]
    [SerializeField] public float RunawayTime;

    [Header("このアイテムを出荷したことによるスコア")]
    [SerializeField] public int ItemScore;

    [Header("加工にかかる時間")]
    [SerializeField] public float CraftTime;

}
