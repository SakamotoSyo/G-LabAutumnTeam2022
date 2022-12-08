using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [Header("カウントに使うImage")]
    [SerializeField] Image _timeImage;
    [Header("TimerのSprite")]
    [SerializeField] Sprite[] _sprites = new Sprite[2];

    [Tooltip("始まるまでの秒数")]
    readonly float _startTime = 3;
    [Tooltip("スコアを保存しておく変数")]
    private static int score;

    /// <summary>Gameがスタートするときに呼ぶAction</summary>
    public static event Action GameStart;
    /// <summary>GameをPauseさせるときに呼ぶAction</summary>
    public static event Action GamePause;
    /// <summary>Gameを終了するときに呼ぶAction</summary>
    public static event Action GameEnd;

    async void Start()
    {
        //スタートまで待機
        await StartDeley();
        OnBiginTurn();
    }

    public static void AddScore(int scoreNum) 
    {
        score += scoreNum;
    }

    /// <summary>ゲームを開始するときに呼ぶ関数</summary>
    public static void OnBiginTurn()
    {
        GameStart?.Invoke();
    }

    /// <summary>ゲームを終了するときに呼ぶ関数</summary>
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