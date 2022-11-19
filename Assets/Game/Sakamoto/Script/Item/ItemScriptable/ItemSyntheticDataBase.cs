using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SakamotoScriptable/SyntheticDataBase")]
public class ItemSyntheticDataBase : ScriptableObject
{
    public List<ItemSynthetic> SyntheticList = new List<ItemSynthetic>();

    /// <summary>
    /// �������X�g�̒��g�������_���ɕԂ�
    /// </summary>
    /// <returns></returns>
    public ItemSynthetic GetRandamSyntheticData()
    {
        return SyntheticList[UnityEngine.Random.Range(0, SyntheticList.Count)];
    }
}
