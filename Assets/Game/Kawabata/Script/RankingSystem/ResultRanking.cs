using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultRanking : Ranking
{

    [SerializeField, Header("入力画面のcanvas")]
    private GameObject _inputCanvas;


    [SerializeField,Header("テスト：受け取るスコア")]
    private int _p_score = 0;
    private int _p_rank;



    // Start is called before the first frame update
    void Start()
    {
        CreateRanking(_level);


        //今回のスコアの順位を決定
        for (var i = _data.Count - 1; i >= 0; i--)
        {
            if (_data[i].score > _p_score)
            {
                _p_rank = i + 1;
                break;
            }
        }

        //１０位以内だったら入力画面表示
        if (_p_rank < _data.Count-1)
        {
            Debug.Log($"ランクインおめでとう Score:{_p_score}, {_p_rank + 1}位");
            _inputCanvas.SetActive(true); 
        }//ランクインしていなかったらそのままランキング表示
        else { ViewRanking(); }
    }

    //入力された名前を元にプレイヤーをランキングに追加する
    public void AddPlayerScore(string n)
    {
        //プレイヤーのデータを用意する
        var p_data = new RankData();
        p_data.name = n;
        p_data.score = _p_score;


        //挿入して、最下位を削除
        _data.Insert(_p_rank, p_data);
        _data.Remove(_data[_data.Count-1]);

        //ランキング表示
        _inputCanvas.SetActive(false);
        ViewRanking();

    }






}
