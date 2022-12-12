using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //ボタンが押されたら難易度を変更するようなスクリプトにしたい

    [SerializeField, Header("遷移先ゲームシーンの名前")]
    private string GameScene;
    public int _level;
    [SerializeField]
    public GameObject[] _selected;

    private void Start()
    {
        ChangeLevel(_level);
    }

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
        SceneManager.LoadScene(GameScene);
    }

}
