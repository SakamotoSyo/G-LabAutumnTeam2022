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
    
    [SerializeField, Header("遷移先タイトルシーンの名前")]
    string TitleScene;

    [SerializeField, Header("入力画面のcanvas")]
    private GameObject _inputCanvas;

    [SerializeField, Header("ランキング画面のキャンバス")]
    private GameObject _rankingCanvas;

    [SerializeField, Header("リザルト画面のcanvas")]
    private GameObject _resultCanvas;

    private int _p_rank = -1;

    private int _r_level;

    public int Level
    {
        get { return _r_level; }
    } 

    [SerializeField, Header("ランキングスクリプト")]
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

        //今回のスコアの順位を決定
        for (var i = data.Count - 1; i >= 0; i--)
        {
            if (data[i].score > _p_score)
            {
                _p_rank = i + 1;
                break;
            }
        }

        //１０位以内だったら入力画面表示
        if (_p_rank < data.Count-1)
        {
            Debug.Log($"ランクインおめでとう Score:{_p_score}, {_p_rank + 1}位");
            _rankingCanvas.SetActive(false);
            _inputCanvas.SetActive(true); 
        }//ランクインしていなかったらそのままランキング表示
        else { _ranking.Selected(_r_level); }
    }

    //入力された名前を元にプレイヤーをランキングに追加する
    public void AddPlayerScore(string n)
    {
        //プレイヤーのデータを用意する
        var p_data = new RankData();
        p_data.name = n;
        p_data.score = _p_score;
        Debug.Log($"{p_data.score}です");
        p_data.rank = 1;

        _ranking._p_data = p_data;



        //ランキング表示
        _inputCanvas.SetActive(false);
        _ranking.InsertData(p_data);
        _ranking.Selected(_r_level);
        GoResultCanvas();

    }

    //リザルト画面
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
        //スコア表示
        scoreText.text = _p_score.ToString();
        //順位表示 rankは0始まりで数えているので+1しながら処理
        var rank = _p_rank + 1;
        //ランキング範囲外だったら"-----"位とする
        if(rank >= Ranking._num || rank <= 0) { rankText.text = "-----"; }
        else { rankText.text = (rank).ToString(); }

        //称号については詳細を聞いていないのでまだ書いていません
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
        //シーン遷移
        //Debug.Log("タイトル画面に遷移します");
        SceneManager.LoadScene(TitleScene);
    }


}
