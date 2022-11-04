using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMgr : MonoBehaviour
{
    bool _genChecker =false;
    [SerializeField]float _doorTimer = 5;
    void Start()
    {
        
    }

    void Update()
    {
        DoorCheck();
        StopGen();
    }
    private void DoorCheck()
    {
        //ドアがfalseならカウントダウンを開始
       if(DoorHit._doorClosed == false)
        {
            _doorTimer -= Time.deltaTime;
        }
       //ドアがtrueならタイマーをリセット
       if (DoorHit._doorClosed == true)
        {
            _doorTimer = 5;
        }
    }

    private void StopGen()
    {
        if(_doorTimer < 0 )_genChecker = false;
    }
}
