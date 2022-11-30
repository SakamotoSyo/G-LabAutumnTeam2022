using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    //ボタンが押されたら難易度を変更するようなスクリプトにしたい
    public int _level;
    [SerializeField]
    public GameObject[] _selected;

    public void ChangeLevel(int level)
    {
        _level = level;
        for(var i = 0; i < _selected.Length; i++)
        {
            if(i == _level)
            {
                _selected[i].SetActive(true);
            }
            else
            {
                _selected[i].SetActive(false); 
            }
        }
    }

    public void GameStart()
    {
        //シーン遷移
        Debug.Log($"ゲームスタート　難易度：{_level}");
    }
}
