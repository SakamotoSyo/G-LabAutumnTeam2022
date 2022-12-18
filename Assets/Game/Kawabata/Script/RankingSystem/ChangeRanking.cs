using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeRanking : Ranking
{

    [SerializeField, Header("ランキングボードの位置アンカー")]
    private GameObject _rankPos;
    private RectTransform _rect;
    [SerializeField, Header("スクロールバー")]
    private GameObject _scroll;
    private Scrollbar _scrollbar;
    [SerializeField]
    private GameObject[] _buttons;

    private Vector2 _top;
    private Vector2 _bottom;
    private float _sum;

    public  RankData _p_data;
    public int _initLevel = EASY;

    private void Start()
    {
        _rect = _rankPos.GetComponent<RectTransform>();
        _scrollbar = _scroll.GetComponent<Scrollbar>();
        Selected(_initLevel);
    }

    private void Update()
    {
        if(_sum == 0)
        {
            ResetBord();
            Selected(_initLevel);
        }
    }

    public void ScrollUpdate()
    {
        float scPos = _sum * _scrollbar.value;
        _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, scPos);
    }

    //難易度変更ボタンクリックorタイトルからの遷移で呼び出し
    public void Selected(int level)
    {
        _initLevel = level;
        ResetBord();
        CreateRanking(level);
        ViewRanking();
        _top = _boad.transform.Find("Top").GetComponent<RectTransform>().anchoredPosition;
        _bottom = _boad.transform.Find("Bottom").GetComponent<RectTransform>().anchoredPosition;
        _sum = _top.y - _bottom.y;
        Debug.Log(_sum);

        for(var i = 0; i < _buttons.Length; i++)
        {
            if(i == level) { _buttons[i].SetActive(true); }
            else { _buttons[i].SetActive(false); }
        }

    }

    //作ったランキングのゲームオブジェクトをDestroyする
    private void ResetBord()
    {
        Debug.Log("リセット");

        if(_boad.transform.childCount > 0)
        {
            foreach (Transform child in _boad.transform)
            {
                Destroy(child.gameObject);
            }

        }
    }

    void OnGUI()
    {
        //マウスホイールでスクロール
        float value = Input.mouseScrollDelta.y;
        var scrollSpeed = 0.05f; 

        if(Mathf.Abs(value) > 0.01f)
        {
            _scrollbar.value -= scrollSpeed * value;
            if(_scrollbar.value < 0.0f) { _scrollbar.value = 0.0f; }
            else if(_scrollbar.value > 1.0f) { _scrollbar.value = 1.0f; }
        }

    }


}
