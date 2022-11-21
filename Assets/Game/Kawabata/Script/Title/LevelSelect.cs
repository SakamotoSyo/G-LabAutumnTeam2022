using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject _selected;
    //�{�^���������ꂽ���Փx��ύX����悤�ȃX�N���v�g�ɂ�����
    public int _level;

    public void ChangeLevel(int level)
    {
        _selected.GetComponent<RectTransform>().anchoredPosition = new Vector2(-175 + level * 175,0) ;
        _level = level;
    }

    public void GameStart()
    {
        //�V�[���J��
        Debug.Log($"�Q�[���X�^�[�g�@��Փx�F{_level}");
    }
}
