using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("���C���^�C�g��Canvas")]
    private GameObject _title;
    [SerializeField, Header("��Փx�I��Canvas")]
    private GameObject _levelSelect;
    [SerializeField, Header("�������Canvas")]
    private GameObject _help;
    [SerializeField, Header("�����L���OCanvas")]
    private GameObject _ranking;
    [SerializeField, Header("�X�g�[���[���Canvas")]
    private GameObject _story;

    private GameObject _curCanvas;

    private void Start()
    {
        _curCanvas = _title;
    }

    public void ChangeCanvas(GameObject canvas)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.SE_select);
        _curCanvas.SetActive(false);
        canvas.SetActive(true);
        _curCanvas = canvas;
    }
}
