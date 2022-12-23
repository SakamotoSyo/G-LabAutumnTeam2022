using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //�{�^���������ꂽ���Փx��ύX����悤�ȃX�N���v�g�ɂ�����
    static public int _level;

    [SerializeField, Header("�~�i���C�V�[�����O")]
    private string _easyName;
    [SerializeField, Header("�V���N�j���V�[�����O")]
    private string _nomalName;
    [SerializeField, Header("�I���J�^�V�[�����O")]
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
        //�V�[���J��
        //Debug.Log($"�Q�[���X�^�[�g�@��Փx�F{_level}");
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
