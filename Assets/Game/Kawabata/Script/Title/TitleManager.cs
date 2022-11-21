using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("メインタイトルCanvas")]
    private GameObject _title;
    [SerializeField, Header("難易度選択Canvas")]
    private GameObject _levelSelect;
    [SerializeField, Header("操作説明Canvas")]
    private GameObject _help;
    [SerializeField, Header("ランキングCanvas")]
    private GameObject _ranking;

    private GameObject _curCanvas;

    private void Start()
    {
        _curCanvas = _title;
    }

    public void ChangeCanvas(GameObject canvas)
    {
        _curCanvas.SetActive(false);
        canvas.SetActive(true);
        _curCanvas = canvas;
    }
}
