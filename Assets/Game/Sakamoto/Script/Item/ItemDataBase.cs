using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SakamotoScriptable/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
   public List<ItemData> Data = new List<ItemData>();
}
