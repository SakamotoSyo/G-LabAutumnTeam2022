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

    [SerializeField, Header("�~�i���C�t�@�C���p�X")]
    private string _easyPath;
    [SerializeField, Header("�V���N�j���t�@�C���p�X")]
    private string _nomalPath;
    [SerializeField, Header("�I���J�^�t�@�C���p�X")]
    private string _hardPath;




    [SerializeField, Header("�e�X�g�F�X�R�A�e�L�X�gUI�v���n�u")]
    protected GameObject scoreUI;
    [SerializeField, Header("�e�X�g�F�\�����鐔")]
    private int _num = 10;
    [SerializeField, Header("�e�X�g�F��Փx")]
    public int _level = EASY;


    [SerializeField, Header("�P�ʂ̃X�v���C�g")]
    private Sprite _rankSprite1;
    [SerializeField, Header("�Q�ʂ̃X�v���C�g")]
    private Sprite _rankSprite2;
    [SerializeField, Header("�R�ʂ̃X�v���C�g")]
    private Sprite _rankSprite3;
    [SerializeField, Header("�S�ʈȍ~�̃X�v���C�g")]
    private Sprite _rankSprite4;

    [SerializeField, Header("�����L���O�{�[�h")]
    protected GameObject _boad;






    public List<RankData> _data;
    protected List<GameObject> _obj;

    private string _dataPath;
    private string _levelName;


    //�����L���O����
    public void CreateRanking(int level)
    {
        if (level == EASY)
        {
            _dataPath = _easyPath;
            _levelName = "�~�i���C";

        }
        else if (level == NOMAL)
        {
            _dataPath = _nomalPath;
            _levelName = "�V���N�j��";

        }
        if (level == HARD)
        {
            _dataPath = _hardPath;
            _levelName = "�I���J�^";

        }


        _data = new List<RankData>(LoadFile());
        _obj = new List<GameObject>();


        //���ёւ�����
        var c = new System.Comparison<RankData>(Conpare);
        _data.Sort(c);

    }



    //�Q�[���I�u�W�F�N�g�����A�����L���O�\��
    protected void ViewRanking()
    {
        Debug.Log("ViewRanking");
        for (var i = 0; i < _data.Count; i++)
        {
            //���ʁ@�O�ԁ��P�� �̂悤�ɕϊ�
            _data[i].rank = i + 1;
            //���ʃ^�C�̏���
            if (i != 0)
            {
                if (_data[i].score == _data[i - 1].score) { _data[i].rank = _data[i - 1].rank; }
            }
            //�X�R�A��\������I�u�W�F�N�g���C���X�^���X��
            //rank,name,score����ݒ�
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
            //�L�����o�X�̎q�I�u�W�F�N�g�ɐݒ�
            obj.transform.SetParent(_boad.transform);
            //���X�g�ɒǉ�
            _obj.Add(obj);
        }

        Debug.Log($"��Փx��{_levelName}�ł���");

        //�t�@�C�������o�����ďI��
        SaveFile();

    }

    //�t�@�C�������o��
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

    //�t�@�C���ǂݍ���
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

    //�\�[�g�p�̊֐��A�~��
    static int Conpare(RankData a, RankData b)
    {
        return b.score - a.score;
    }

    public void InsertData(RankData d)
    {
        //�}�����āA�ŉ��ʂ��폜
        _data.Insert(d.rank, d);
        _data.Remove(_data[_data.Count - 1]);

        SaveFile();

    }



}
