using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    //�{�^���������ꂽ���Փx��ύX����悤�ȃX�N���v�g�ɂ�����
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
        //�V�[���J��
        Debug.Log($"�Q�[���X�^�[�g�@��Փx�F{_level}");
    }
}
