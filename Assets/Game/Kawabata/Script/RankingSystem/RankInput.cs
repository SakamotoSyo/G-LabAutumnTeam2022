using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankInput : MonoBehaviour
{
    [SerializeField, Header("Ranking")]
    private GameObject _ranking;


    public void InputPlayerData()
    {
        var rank = _ranking.GetComponent<Ranking>();
        var p_name = this.GetComponent<InputField>().text;
        //Debug.Log($"ランクインおめでとう　Score:{rank._p_score}, {rank._p_rank}位")
        rank.AddPlayerScore(p_name);
        return;
    }
}
