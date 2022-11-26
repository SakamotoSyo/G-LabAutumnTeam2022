using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultManager : MonoBehaviour
{

    [SerializeField, Header("���͉�ʂ�canvas")]
    private GameObject _inputCanvas;

    [SerializeField, Header("�����L���O��ʂ̃L�����o�X")]
    private GameObject _rankingCanvas;

    [SerializeField, Header("���U���g��ʂ�canvas")]
    private GameObject _resultCanvas;

    [SerializeField, Header("�e�X�g�F�󂯎��X�R�A")]
    private int _p_score = 0;
    private int _p_rank = -1;

    [SerializeField, Header("�e�X�g�F�󂯎���Փx")]
    private int _r_level;

    [SerializeField, Header("�����L���O�X�N���v�g")]
    private GameObject _rankingManager;
    private ChangeRanking _ranking;


    // Start is called before the first frame update
    void Start()
    {
        _ranking = _rankingManager.GetComponent<ChangeRanking>();
        _ranking.CreateRanking(_r_level);

        var data = new List<RankData>(_ranking._data);

        //����̃X�R�A�̏��ʂ�����
        for (var i = data.Count - 1; i >= 0; i--)
        {
            if (data[i].score > _p_score)
            {
                _p_rank = i + 1;
                break;
            }
        }

        //�P�O�ʈȓ�����������͉�ʕ\��
        if (_p_rank < data.Count-1)
        {
            Debug.Log($"�����N�C�����߂łƂ� Score:{_p_score}, {_p_rank + 1}��");
            _rankingCanvas.SetActive(false);
            _inputCanvas.SetActive(true); 
        }//�����N�C�����Ă��Ȃ������炻�̂܂܃����L���O�\��
        else { _ranking.Selected(_r_level); }
    }

    //���͂��ꂽ���O�����Ƀv���C���[�������L���O�ɒǉ�����
    public void AddPlayerScore(string n)
    {
        //�v���C���[�̃f�[�^��p�ӂ���
        var p_data = new RankData();
        p_data.name = n;
        p_data.score = _p_score;
        p_data.rank = _p_rank;

        _ranking._p_data = p_data;



        //�����L���O�\��
        _inputCanvas.SetActive(false);
        _ranking.InsertData(p_data);
        _ranking.Selected(_r_level);

    }

    public void GoResultCanvas()
    {
        _rankingCanvas.SetActive(false);
        _resultCanvas.SetActive(true);
        var scoreText = _resultCanvas.transform.Find("Score").GetComponent<Text>();
        var rankText = _resultCanvas.transform.Find("Rank").GetComponent<Text>();
        var titleText = _resultCanvas.transform.Find("Title").GetComponent<Text>();
        scoreText.text = _p_score.ToString();
        if(_p_rank == -1) { rankText.text = "-----"; }
        else { rankText.text = _p_rank.ToString(); }
    }

    public void BackRankingBoad()
    {
        _resultCanvas.SetActive(false);
        _rankingCanvas.SetActive(true);
    }

    public void GoTitle()
    {
        //�V�[���J��
        Debug.Log("�^�C�g����ʂɑJ�ڂ��܂�");
    }


}
