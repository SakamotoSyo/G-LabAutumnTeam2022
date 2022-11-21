using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleRanking : Ranking
{
    //難易度変更ボタンクリックorタイトルからの遷移で呼び出し
    public void Selected(int level)
    {
        ResetBord();
        CreateRanking(level);
        ViewRanking();
    }

    //作ったランキングのゲームオブジェクトをDestroyする
    private void ResetBord()
    {
        foreach(var ob in _obj){ Destroy(ob); }
    }
}
