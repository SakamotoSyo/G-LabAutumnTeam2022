using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultManager : MonoBehaviour
{

    [SerializeField, Header("入力画面のcanvas")]
    private GameObject _inputCanvas;

    [SerializeField, Header("ランキング画面のキャンバス")]
    private GameObject _rankingCanvas;

    [SerializeField, Header("リザルト画面のcanvas")]
    private GameObject _resultCanvas;

    [SerializeField, Header("テスト：受け取るスコア")]
    private int _p_score = 0;
    private int _p_rank = -1;

    [SerializeField, Header("テスト：受け取る難易度")]
    private int _r_level;

    [SerializeField, Header("ランキングスクリプト")]
    private GameObject _rankingManager;
    private ChangeRanking _ranking;


    // Start is called before the first frame update
    void Start()
    {
        _ranking = _rankingManager.GetComponent<ChangeRanking>();
        _ranking.CreateRanking(_r_level);

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
        p_data.rank = _p_rank;

        _ranking._p_data = p_data;



        //ランキング表示
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
        //シーン遷移
        Debug.Log("タイトル画面に遷移します");
    }


}
