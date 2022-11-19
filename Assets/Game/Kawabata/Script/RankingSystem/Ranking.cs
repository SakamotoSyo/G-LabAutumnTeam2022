using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RankData
{
    public int rank;
    public string name;
    public int score;
}

[System.Serializable]
public class Ranking : MonoBehaviour
{

    private List<RankData> _data;
    private List<GameObject> _obj;

    [SerializeField, Header("テスト：スコアテキストUIプレハブ")]
    private GameObject scoreUI;

    [SerializeField,Header("テスト：受け取るスコア")]
    private int _p_score = 0;
    private int _p_rank;

    [SerializeField, Header("テスト：表示する数")]
    private int _num = 10;

    [SerializeField, Header("入力画面のcanvas")]
    private GameObject _canvas;


    private string dataPath;
    private void Awake()
    {
        dataPath = Application.dataPath + "/Game/Kawabata/JSON/TestJson.json";
    }


    // Start is called before the first frame update
    void Start()
    {
        _data = new List<RankData>();
        _obj = new List<GameObject>();

        //データファイル読み込み
        LoadFile();
        //for (var i = 0; i < _num; i++)
        //{
        //    var d = new RankData();
        //    d.name = "Num" + i.ToString();
        //    d.score = Random.Range(10, 10000);
        //    _data.Add(d);
        //}

        //並び替え処理
        var c = new System.Comparison<RankData>(Conpare);
        _data.Sort(c);

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
            _canvas.SetActive(true); 
        }//ランクインしていなかったらそのままランキング表示
        else { CriateRanking(); }
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
        CriateRanking();

    }

    //ゲームオブジェクト生成
    private void CriateRanking()
    {
        _canvas.SetActive(false);
        for (var i = 0; i < _data.Count; i++)
        {
            _data[i].rank = i + 1;
            //順位タイ表示
            if(i != 0)
            {
                if (_data[i].score == _data[i - 1].score) { _data[i].rank = _data[i - 1].rank; }
            }
            var obj = Instantiate(scoreUI);
            obj.name = i + _data[i].name;
            var text = obj.GetComponent<Text>();
            text.text = _data[i].rank + "  Name: " + _data[i].name + "  Score: " + _data[i].score;
            obj.transform.SetParent(transform);
            _obj.Add(obj);
        }

        //ファイル書き出しして終了
        SaveFile();

    }

    //ファイル書き出し
    void SaveFile()
    {

        StreamWriter writer = new StreamWriter(dataPath,false);
        for(var i = 0; i < _data.Count; i++)
        {
            string jsonstr = JsonUtility.ToJson(_data[i]);
            writer.WriteLine(jsonstr);
            writer.Flush();

        }
        writer.Close();
    }

    //ファイル読み込み
    //Debug.Logの結果からみて何故か２回呼び出されている
    void LoadFile()
    {
        var reader = new StreamReader(dataPath,false);
        for(var i = 0; i < _num; i++)
        {
            string datastr = reader.ReadLine();
            var data = new RankData();
            data.score = 0;
            data.name = "-----";
            if(datastr != null)
            {
                data = JsonUtility.FromJson<RankData>(datastr);
            }
            _data.Add(data);
        }
        reader.Close();
        return;
    }

    //ソート用の関数、降順
    static int Conpare(RankData a, RankData b)
    {
        return b.score - a.score;
    }


}
