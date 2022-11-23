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
        var rank = _ranking.GetComponent<ResultRanking>();
        var p_name = this.GetComponent<InputField>().text;
        //Debug.Log($"ƒ‰ƒ“ƒNƒCƒ“‚¨‚ß‚Å‚Æ‚¤@Score:{rank._p_score}, {rank._p_rank}ˆÊ")
        rank.AddPlayerScore(p_name);
        return;
    }
}
