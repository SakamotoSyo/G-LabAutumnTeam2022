using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("始まるまでの秒数")]
    [SerializeField] float _startTime;

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

    public void AddScore(int scoreNum) 
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
        await UniTask.Delay(TimeSpan.FromSeconds(_startTime));
    }
}