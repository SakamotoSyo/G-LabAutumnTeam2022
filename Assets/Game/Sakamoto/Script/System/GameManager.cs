using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("始まるまでの秒数")]
    [SerializeField] float _startTime;


    /// <summary>Gameがスタートするときに呼ぶ</summary>
    public static event Action GameStart;
    /// <summary>GameをPauseさせるときに呼ぶ</summary>
    public static event Action GamePause;
    /// <summary>Gameを終了するときに呼ぶ</summary>
    public static event Action GameEnd;

    async void Start()
    {
        //スタートまで待機
        await StartDeley();
        OnBiginTurn();
    }

    /// <summary>ゲームを開始するときに呼ぶ関数</summary>
    public static void OnBiginTurn()
    {
        GameStart();
    }

    /// <summary>ゲームを終了するときに呼ぶ関数</summary>
    public static void OnEndTurn()
    {
        GameEnd();
    }

    async UniTask StartDeley() 
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_startTime));
    }
}