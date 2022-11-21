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


public class Ranking : MonoBehaviour
{
    public const int EASY = 0;
    public const int NOMAL = 1;
    public const int HARD = 2;

    [SerializeField, Header("ミナライファイルパス")]
    private string _easyPath;
    [SerializeField, Header("ショクニンファイルパス")]
    private string _nomalPath;
    [SerializeField, Header("オヤカタファイルパス")]
    private string _hardPath;




    [SerializeField, Header("テスト：スコアテキストUIプレハブ")]
    protected GameObject scoreUI;
    [SerializeField, Header("テスト：表示する数")]
    private int _num = 10;
    [SerializeField, Header("テスト：難易度")]
    public int _level = EASY;


    protected List<RankData> _data;
    protected List<GameObject> _obj;

    private string _dataPath;
    private string _levelName;


    //ランキング生成
    protected void CreateRanking(int level)
    {
        if (level == EASY)
        {
            _dataPath = _easyPath;
            _levelName = "ミナライ";

        }
        else if (level == NOMAL)
        {
            _dataPath = _nomalPath;
            _levelName = "ショクニン";

        }
        if (level == HARD)
        {
            _dataPath = _hardPath;
            _levelName = "オヤカタ";

        }


        _data = new List<RankData>(LoadFile());
        _obj = new List<GameObject>();


        //並び替え処理
        var c = new System.Comparison<RankData>(Conpare);
        _data.Sort(c);

    }



    //ゲームオブジェクト生成、ランキング表示
    protected void ViewRanking()
    {
        for (var i = 0; i < _data.Count; i++)
        {
            //順位　０番→１位 のように変換
            _data[i].rank = i + 1;
            //順位タイの処理
            if (i != 0)
            {
                if (_data[i].score == _data[i - 1].score) { _data[i].rank = _data[i - 1].rank; }
            }
            //スコアを表示するオブジェクトをインスタンス化
            //rank,name,score等を設定
            var obj = Instantiate(scoreUI);
            obj.name = i + _data[i].name;
            var text = obj.GetComponent<Text>();
            text.text = _data[i].rank + "  Name: " + _data[i].name + "  Score: " + _data[i].score;
            //キャンバスの子オブジェクトに設定
            obj.transform.SetParent(transform);
            //リストに追加
            _obj.Add(obj);
        }

        Debug.Log($"難易度は{_levelName}でした");

        //ファイル書き出しして終了
        SaveFile();

    }

    //ファイル書き出し
    protected void SaveFile()
    {

        StreamWriter writer = new StreamWriter(_dataPath, false);
        for (var i = 0; i < _data.Count; i++)
        {
            string jsonstr = JsonUtility.ToJson(_data[i]);
            writer.WriteLine(jsonstr);
            writer.Flush();

        }
        writer.Close();
    }

    //ファイル読み込み
    protected List<RankData> LoadFile()
    {
        var listdata = new List<RankData>();
        var reader = new StreamReader(_dataPath, false);
        for (var i = 0; i < _num; i++)
        {
            string datastr = reader.ReadLine();
            var data = new RankData();
            data.score = 0;
            data.name = "-----";
            if (datastr != null)
            {
                data = JsonUtility.FromJson<RankData>(datastr);
            }
            listdata.Add(data);
        }
        reader.Close();
        return listdata;
    }

    //ソート用の関数、降順
    static int Conpare(RankData a, RankData b)
    {
        return b.score - a.score;
    }


}
