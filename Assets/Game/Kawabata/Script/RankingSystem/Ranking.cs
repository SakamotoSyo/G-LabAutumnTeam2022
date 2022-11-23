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


    protected List<RankData> _data;
    protected List<GameObject> _obj;

    private string _dataPath;
    private string _levelName;


    //�����L���O����
    protected void CreateRanking(int level)
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
            obj.name = i + _data[i].name;
            var text = obj.GetComponent<Text>();
            text.text = _data[i].rank + "  Name: " + _data[i].name + "  Score: " + _data[i].score;
            //�L�����o�X�̎q�I�u�W�F�N�g�ɐݒ�
            obj.transform.SetParent(transform);
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


}
