using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //ボタンが押されたら難易度を変更するようなスクリプトにしたい
    static public int _level;

    [SerializeField, Header("ミナライシーン名前")]
    private string _easyName;
    [SerializeField, Header("ショクニンシーン名前")]
    private string _nomalName;
    [SerializeField, Header("オヤカタシーン名前")]
    private string _hardName;

    private string _gameSceneName;

    [SerializeField]
    public GameObject[] _selected;

    private void Start()
    {
        ChangeLevel(_level);
    }

    public void ChangeLevel(int level)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.SE_select);

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
        //Debug.Log($"ゲームスタート　難易度：{_level}");
        switch (_level)
        {
            case 0:
                _gameSceneName = _easyName;
                break;
            case 1:
                _gameSceneName = _nomalName;
                break;
            case 2:
                _gameSceneName = _hardName;
                break;
            default:
                break;
                
        }

        AudioManager.Instance.Reset();
        SceneManager.LoadScene(_gameSceneName);
    }

}
