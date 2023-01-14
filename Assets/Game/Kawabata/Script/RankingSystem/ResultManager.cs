using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

[System.Serializable]
public class ResultManager : MonoBehaviour
{

    static public int _p_score = 0;
    
    [SerializeField, Header("�J�ڐ�^�C�g���V�[���̖��O")]
    string TitleScene;

    [SerializeField, Header("���͉�ʂ�canvas")]
    private GameObject _inputCanvas;

    [SerializeField, Header("�����L���O��ʂ̃L�����o�X")]
    private GameObject _rankingCanvas;

    [SerializeField, Header("���U���g��ʂ�canvas")]
    private GameObject _resultCanvas;

    private int _p_rank = -1;

    private int _r_level;

    public int Level
    {
        get { return _r_level; }
    } 

    [SerializeField, Header("�����L���O�X�N���v�g")]
    private GameObject _rankingManager;
    public ChangeRanking _ranking;
    private bool _resultinit = false;

    // Start is called before the first frame update
    void Start()
    {
        _r_level = LevelSelect._level;
        _ranking = _rankingManager.GetComponent<ChangeRanking>();
        _ranking.CreateRanking(_r_level);
        _p_score = GameManager.ScoreNum;

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
        Debug.Log($"{p_data.score}�ł�");
        p_data.rank = 1;

        _ranking._p_data = p_data;



        //�����L���O�\��
        _inputCanvas.SetActive(false);
        _ranking.InsertData(p_data);
        _ranking.Selected(_r_level);
        GoResultCanvas();

    }

    //���U���g���
    public void GoResultCanvas()
    {
        if (!_resultinit)
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_result_rank);
            _resultinit = true;
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundPlayType.SE_select);
        }
        _rankingCanvas.SetActive(false);
        _resultCanvas.SetActive(true);
        var scoreText = _resultCanvas.transform.Find("Score").GetComponent<Text>();
        var rankText = _resultCanvas.transform.Find("Rank").GetComponent<Text>();
        var titleText = _resultCanvas.transform.Find("Title").GetComponent<Text>();
        //�X�R�A�\��
        scoreText.text = _p_score.ToString();
        //���ʕ\�� rank��0�n�܂�Ő����Ă���̂�+1���Ȃ��珈��
        var rank = _p_rank + 1;
        //�����L���O�͈͊O��������"-----"�ʂƂ���
        if(rank >= Ranking._num || rank <= 0) { rankText.text = "-----"; }
        else { rankText.text = (rank).ToString(); }

        //�̍��ɂ��Ă͏ڍׂ𕷂��Ă��Ȃ��̂ł܂������Ă��܂���
    }

    public void BackRankingBoad()
    {
        AudioManager.Instance.PlaySound(SoundPlayType.SE_select);
        _resultCanvas.SetActive(false);
        _rankingCanvas.SetActive(true);
    }

    public void GoTitle()
    {
        AudioManager.Instance.PlaySound(SoundPlayType.SE_enter);
        AudioManager.Instance.Reset();
        //�V�[���J��
        //Debug.Log("�^�C�g����ʂɑJ�ڂ��܂�");
        SceneManager.LoadScene(TitleScene);
    }


}
