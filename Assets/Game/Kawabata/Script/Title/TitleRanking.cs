using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleRanking : Ranking
{
    //��Փx�ύX�{�^���N���b�Nor�^�C�g������̑J�ڂŌĂяo��
    public void Selected(int level)
    {
        ResetBord();
        CreateRanking(level);
        ViewRanking();
    }

    //����������L���O�̃Q�[���I�u�W�F�N�g��Destroy����
    private void ResetBord()
    {
        foreach(var ob in _obj){ Destroy(ob); }
    }
}
