using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [Header("�J�E���g�Ɏg��Image")]
    [SerializeField] Image _timeImage;
    [Header("Timer��Sprite")]
    [SerializeField] Sprite[] _sprites = new Sprite[2];

    [Tooltip("�n�܂�܂ł̕b��")]
    readonly float _startTime = 3;
    [Tooltip("�X�R�A��ۑ����Ă����ϐ�")]
    private static int score;

    /// <summary>Game���X�^�[�g����Ƃ��ɌĂ�Action</summary>
    public static event Action GameStart;
    /// <summary>Game��Pause������Ƃ��ɌĂ�Action</summary>
    public static event Action GamePause;
    /// <summary>Game���I������Ƃ��ɌĂ�Action</summary>
    public static event Action GameEnd;

    async void Start()
    {
        //�X�^�[�g�܂őҋ@
        await StartDeley();
        OnBiginTurn();
    }

    public static void AddScore(int scoreNum) 
    {
        score += scoreNum;
    }

    /// <summary>�Q�[�����J�n����Ƃ��ɌĂԊ֐�</summary>
    public static void OnBiginTurn()
    {
        GameStart?.Invoke();
    }

    /// <summary>�Q�[�����I������Ƃ��ɌĂԊ֐�</summary>
    public static void OnEndTurn()
    {
        GameEnd?.Invoke();
    }

    async UniTask StartDeley() 
    {
        for (int i = 0; i < _sprites.Length; i++) 
        {
            _timeImage.sprite = _sprites[i];
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        _timeImage.enabled = false;
        //await UniTask.Delay(TimeSpan.FromSeconds(_startTime));
        Debug.Log("Start");
    }
}