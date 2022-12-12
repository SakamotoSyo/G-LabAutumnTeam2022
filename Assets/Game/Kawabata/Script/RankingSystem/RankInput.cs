using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankInput : MonoBehaviour
{
    [SerializeField, Header("�����L���O�̃X�N���v�g�����Ă���I�u�W�F�N�g")]
    private GameObject _ranking;
    [SerializeField, Header("�����L���O��canvas")]
    private GameObject _rankingCanvas;



    public void InputPlayerData()
    {
        _rankingCanvas.SetActive(true);
        var rank = _ranking.GetComponent<ResultManager>();
        var p_name = this.GetComponent<InputField>().text;
        //Debug.Log($"�����N�C�����߂łƂ��@Score:{rank._p_score}, {rank._p_rank}��")
        rank.AddPlayerScore(p_name);
        rank._ranking.Selected(rank.Level);
        return;
    }
}
