using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultRanking : Ranking
{

    [SerializeField, Header("���͉�ʂ�canvas")]
    private GameObject _inputCanvas;


    [SerializeField,Header("�e�X�g�F�󂯎��X�R�A")]
    private int _p_score = 0;
    private int _p_rank;



    // Start is called before the first frame update
    void Start()
    {
        CreateRanking(_level);


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
            _inputCanvas.SetActive(true); 
        }//�����N�C�����Ă��Ȃ������炻�̂܂܃����L���O�\��
        else { ViewRanking(); }
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
        _inputCanvas.SetActive(false);
        ViewRanking();

    }






}
