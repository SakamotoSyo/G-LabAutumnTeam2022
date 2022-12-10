using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class GameManager
{
    public static IObservable<int> Score => score;
    [Tooltip("スコアを保存しておく変数")]
    private static ReactiveProperty<int> score = new();

    /// <summary>Gameがスタートするときに呼ぶAction</summary>
    public static event Action GameStart;
    /// <summary>GameをPauseさせるときに呼ぶAction</summary>
    public static event Action GamePause;
    /// <summary>Gameを終了するときに呼ぶAction</summary>
    public static event Action GameEnd;

    public static void AddScore(int scoreNum)
    {
        score.Value += scoreNum;
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


}