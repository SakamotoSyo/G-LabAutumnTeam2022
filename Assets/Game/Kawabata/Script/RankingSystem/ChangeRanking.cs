using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeRanking : Ranking
{

    [SerializeField, Header("�����L���O�{�[�h�̈ʒu�A���J�[")]
    private GameObject _rankPos;
    private RectTransform _rect;
    [SerializeField, Header("�X�N���[���o�[")]
    private GameObject _scroll;
    private Scrollbar _scrollbar;

    private Vector2 _top;
    private Vector2 _bottom;
    private float _sum;

    public  RankData _p_data;

    private void Start()
    {
        _rect = _rankPos.GetComponent<RectTransform>();
        _scrollbar = _scroll.GetComponent<Scrollbar>();
        Selected(EASY);
    }

    private void Update()
    {
        if(_sum == 0)
        {
            ResetBord();
            Selected(EASY);
        }
        float scPos = _sum * _scrollbar.value;
        _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x,scPos);
 
    }

    //��Փx�ύX�{�^���N���b�Nor�^�C�g������̑J�ڂŌĂяo��
    public void Selected(int level)
    {
        ResetBord();
        CreateRanking(level);
        ViewRanking();
        _top = _boad.transform.Find("Top").GetComponent<RectTransform>().anchoredPosition;
        _bottom = _boad.transform.Find("Bottom").GetComponent<RectTransform>().anchoredPosition;
        _sum = _top.y - _bottom.y;
        //_scrollbar.value = 0;
        Debug.Log(_sum);
    }

    //����������L���O�̃Q�[���I�u�W�F�N�g��Destroy����
    private void ResetBord()
    {
        Debug.Log("���Z�b�g");

        if(_boad.transform.childCount > 0)
        {
            foreach (Transform child in _boad.transform)
            {
                Destroy(child.gameObject);
            }

        }
    }

}
