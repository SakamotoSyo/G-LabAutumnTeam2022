using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankInput : MonoBehaviour
{
    [SerializeField, Header("ランキングのスクリプトがついているオブジェクト")]
    private GameObject _ranking;
    [SerializeField, Header("ランキングのcanvas")]
    private GameObject _rankingCanvas;



    public void InputPlayerData()
    {
        _rankingCanvas.SetActive(true);
        var rank = _ranking.GetComponent<ResultManager>();
        var p_name = this.GetComponent<InputField>().text;
        //Debug.Log($"ランクインおめでとう　Score:{rank._p_score}, {rank._p_rank}位")
        rank.AddPlayerScore(p_name);
        rank._ranking.Selected(rank.Level);
        return;
    }
}
