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

    [SerializeField, Header("�e�X�g�F�X�R�A�e�L�X�gUI�v���n�u")]
    private GameObject scoreUI;

    [SerializeField,Header("�e�X�g�F�󂯎��X�R�A")]
    private int _p_score = 0;
    private int _p_rank;

    [SerializeField, Header("�e�X�g�F�\�����鐔")]
    private int _num = 10;

    [SerializeField, Header("���͉�ʂ�canvas")]
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

        //�f�[�^�t�@�C���ǂݍ���
        LoadFile();
        //for (var i = 0; i < _num; i++)
        //{
        //    var d = new RankData();
        //    d.name = "Num" + i.ToString();
        //    d.score = Random.Range(10, 10000);
        //    _data.Add(d);
        //}

        //���ёւ�����
        var c = new System.Comparison<RankData>(Conpare);
        _data.Sort(c);

        //����̃X�R�A�̏��ʂ�����
        for (var i = _data.Count - 1; i >= 0; i--)
        {
            if (_data[i].score > _p_score)
            {
                _p_rank = i + 1;
                break;
            }
        }

        //�P�O�ʈȓ�����������͉�ʕ\��
        if (_p_rank < _data.Count-1)
        {
            Debug.Log($"�����N�C�����߂łƂ� Score:{_p_score}, {_p_rank + 1}��");
            _canvas.SetActive(true); 
        }//�����N�C�����Ă��Ȃ������炻�̂܂܃����L���O�\��
        else { CriateRanking(); }
    }

    //���͂��ꂽ���O�����Ƀv���C���[�������L���O�ɒǉ�����
    public void AddPlayerScore(string n)
    {
        //�v���C���[�̃f�[�^��p�ӂ���
        var p_data = new RankData();
        p_data.name = n;
        p_data.score = _p_score;


        //�}�����āA�ŉ��ʂ��폜
        _data.Insert(_p_rank, p_data);
        _data.Remove(_data[_data.Count-1]);

        //�����L���O�\��
        CriateRanking();

    }

    //�Q�[���I�u�W�F�N�g����
    private void CriateRanking()
    {
        _canvas.SetActive(false);
        for (var i = 0; i < _data.Count; i++)
        {
            _data[i].rank = i + 1;
            //���ʃ^�C�\��
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

        //�t�@�C�������o�����ďI��
        SaveFile();

    }

    //�t�@�C�������o��
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

    //�t�@�C���ǂݍ���
    //Debug.Log�̌��ʂ���݂ĉ��̂��Q��Ăяo����Ă���
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

    //�\�[�g�p�̊֐��A�~��
    static int Conpare(RankData a, RankData b)
    {
        return b.score - a.score;
    }


}
