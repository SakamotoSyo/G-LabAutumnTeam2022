using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingLevel : MonoBehaviour
{
    [SerializeField, Header("難易度ごとのラベルのスプライト")]
    Sprite[] _levels;
    int level = 0; //受け取った難易度(仮)

    private void Start()
    {
        level = GameObject.Find("ResultManager").GetComponent<ResultManager>().Level;
        if(_levels.Length < 3) { Debug.LogError("難易度の数と同じ数スプライトをセットしてください"); }
        this.GetComponent<Image>().sprite = _levels[level];
    }
}
