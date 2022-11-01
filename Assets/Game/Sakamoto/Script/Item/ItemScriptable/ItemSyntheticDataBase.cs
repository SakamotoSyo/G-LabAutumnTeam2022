using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SakamotoScriptable/SyntheticDataBase")]
public class ItemSyntheticDataBase : ScriptableObject
{
    public List<ItemSynthetic> SyntheticList = new List<ItemSynthetic>();
}
