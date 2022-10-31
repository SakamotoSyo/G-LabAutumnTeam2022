using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMgr : MonoBehaviour
{
    bool _doorChecker=false;
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
        if(_doorChecker == false)
        {
            _doorTimer -= Time.deltaTime;
            if(_doorTimer < 0)_doorChecker = true;
        }
    }
    private void StopGen()
    {
        if(_doorChecker == true )_genChecker = false;
    }
}
