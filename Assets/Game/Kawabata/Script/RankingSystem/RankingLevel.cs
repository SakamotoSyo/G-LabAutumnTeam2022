using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingLevel : MonoBehaviour
{
    [SerializeField, Header("��Փx���Ƃ̃��x���̃X�v���C�g")]
    Sprite[] _levels;
    int level = 0; //�󂯎������Փx(��)

    private void Start()
    {
        level = GameObject.Find("ResultManager").GetComponent<ResultManager>().Level;
        if(_levels.Length < 3) { Debug.LogError("��Փx�̐��Ɠ������X�v���C�g���Z�b�g���Ă�������"); }
        this.GetComponent<Image>().sprite = _levels[level];
    }
}
