using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject _selected;
    //ボタンが押されたら難易度を変更するようなスクリプトにしたい
    public int _level;

    public void ChangeLevel(int level)
    {
        _selected.GetComponent<RectTransform>().anchoredPosition = new Vector2(-175 + level * 175,0) ;
        _level = level;
    }

    public void GameStart()
    {
        //シーン遷移
        Debug.Log($"ゲームスタート　難易度：{_level}");
    }
}
