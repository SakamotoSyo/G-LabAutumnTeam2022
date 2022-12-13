using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //�{�^���������ꂽ���Փx��ύX����悤�ȃX�N���v�g�ɂ�����

    [SerializeField, Header("�J�ڐ�Q�[���V�[���̖��O")]
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
        //�V�[���J��
        Debug.Log($"�Q�[���X�^�[�g�@��Փx�F{_level}");
        SceneManager.LoadScene(GameScene);
    }

}
