using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMgr : MonoBehaviour
{
    #region 定義
    [Header("ドア開放時間"),SerializeField]float _doorRereaseTimer = 5;
    [Header("再始動時間"), SerializeField] float _startTimer;
    [SerializeField] DoorHit [] _doorHits;
    #endregion
    void Start()
    {
        _doorHits[0].DoorChange += DoorCheck;
        _doorHits[1].DoorChange += DoorCheck;
        
    }
    void Update()
    {
        //Debug.Log($"Door1 = {_doorHits[0].IsDoorClose()}");
        //Debug.Log($"Door2 = {_doorHits[1].IsDoorClose()}");
          DoorCheck();
    }
    #region ２ヶ所のドアの真偽のチェックとその後の処理
    private void DoorCheck()
    {
        
        //どちらかのドアがfalseならカウントダウンを開始
        if (_doorHits[0].IsDoorClose() == false || _doorHits[1].IsDoorClose() == false)
        {
           // Debug.Log("aaaaaaaaa");
            _doorRereaseTimer -= Time.deltaTime;
            _startTimer = 5;
        }

        //ドアがtrueならタイマーをリセット
        if (_doorHits[0].IsDoorClose() == true && _doorHits[1].IsDoorClose() == true)
        {
           // Debug.Log("reset");
            _doorRereaseTimer = 5;
        }
        StopGen();
    }
    #endregion
    
    #region 一定時間経つとItemGeneratorとItemMoveConのStop関数を呼ぶ
  private void StopGen()
    {
        if(_doorRereaseTimer < 0)
        {
            ItemGenerator._stopFrag = false;
            ItemMoveCon._ItemMoveConFlag = false;
           
        }
        DoorReCheck();
    }
    #endregion

    #region ドアを閉めた時の処理
    private void DoorReCheck()
    {
        //両方のドアがtrueならタイマーをリセット
        if (_doorHits[0].IsDoorClose() == true && _doorHits[1].IsDoorClose() == true)
        {
            _doorRereaseTimer = 5;//リセット
            _startTimer -= Time.deltaTime;//再始動のカウントダウン
            if (_startTimer < 0)
            {
                ItemGenerator._stopFrag = true;
                ItemMoveCon._ItemMoveConFlag = true;
            }
        }
    }
    #endregion
}
