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
    //ランキング表示する人数
    //※※減らすと範囲外のデータが消滅します※※
    public const int _num = 30;


    public const int EASY = 0;
    public const int NOMAL = 1;
    public const int HARD = 2;

    [SerializeField, Header("難易度のフォルダのパス")]
    private string _levelDataPath;

    [SerializeField, Header("ミナライデータ名前")]
    private string _easyPath;
    [SerializeField, Header("ショクニンデータ名前")]
    private string _nomalPath;
    [SerializeField, Header("オヤカタデータ名前")]
    private string _hardPath;




    [SerializeField, Header("テスト：スコアテキストUIプレハブ")]
    protected GameObject scoreUI;
    [SerializeField, Header("テスト：難易度")]
    public int _level = EASY;


    [SerializeField, Header("１位のスプライト")]
    private Sprite _rankSprite1;
    [SerializeField, Header("２位のスプライト")]
    private Sprite _rankSprite2;
    [SerializeField, Header("３位のスプライト")]
    private Sprite _rankSprite3;
    [SerializeField, Header("４位以降のスプライト")]
    private Sprite _rankSprite4;

    [SerializeField, Header("ランキングボード")]
    protected GameObject _boad;

    [SerializeField, Header("ランキングのサイズ")]
    protected float _size;




    public List<RankData> _data;
    protected List<GameObject> _obj;

    private string _dataPath;
    private string _levelName;


    //ランキング生成
    public void CreateRanking(int level)
    {
        if (level == EASY)
        {
            _dataPath = _levelDataPath + _easyPath;
            _levelName = "ミナライ";

        }
        else if (level == NOMAL)
        {
            _dataPath = _levelDataPath + _nomalPath;
            _levelName = "ショクニン";

        }
        if (level == HARD)
        {
            _dataPath = _levelDataPath + _hardPath;
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
        Debug.Log("ViewRanking");
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
            if(i == 0) { obj.name = "Top"; }
            else if (i == _data.Count - 1) { obj.name = "Bottom"; }
            else { obj.name = i.ToString(); }
            var image = obj.GetComponent<Image>();
            var text_name = obj.transform.Find("Name").GetComponent<Text>();
            var text_score = obj.transform.Find("Score").GetComponent<Text>();
            var text_rank = obj.transform.Find("Rank").GetComponent<Text>();
            text_name.text = _data[i].name;
            text_score.text = _data[i].score.ToString() + "P";
            text_rank.text = "";
            if (_data[i].rank == 1) { image.sprite = _rankSprite1; }
            else if (_data[i].rank == 2) { image.sprite = _rankSprite2; }
            else if (_data[i].rank == 3) { image.sprite = _rankSprite3; }
            else { text_rank.text = _data[i].rank.ToString(); }
            //キャンバスの子オブジェクトに設定
            obj.transform.SetParent(_boad.transform);
            obj.transform.transform.localScale = new Vector3 (_size,_size);
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
        if(System.IO.Directory.Exists(_levelDataPath) == false)
        {
            System.IO.Directory.CreateDirectory(_levelDataPath);
        }
        if(System.IO.File.Exists(_dataPath) == false)
        {
            System.IO.File.Create(_dataPath);
            //CreateEmptyRankData();
        }
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

    public void InsertData(RankData d)
    {
        //挿入して、最下位を削除
        _data.Insert(d.rank, d);
        _data.Remove(_data[_data.Count - 1]);

        SaveFile();

    }

    //public void CreateEmptyRankData()
    //{

    //    StreamWriter writer = new StreamWriter(_dataPath, false);
    //    for (var i = 0; i < _num; i++)
    //    {
    //        RankData d;
    //        d.name = "-------";
    //        d.rank = 1;
    //        d.score = 0;
    //        string jsonstr = JsonUtility.ToJson(d);
    //        writer.WriteLine(jsonstr);
    //        writer.Flush();

    //    }
    //    writer.Close();

    //}



}
