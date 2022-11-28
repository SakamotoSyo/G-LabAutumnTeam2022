using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    [SerializeField,Header("Door")]
    GameObject _door;
    DoorHit _doorHit;

    private void Start()
    {
        _doorHit = _door.GetComponent<DoorHit>();
    }
    //テスト用のプレイヤーコントローラー
    void Update()
    {
        var x = Input.GetAxis("Horizontal1") / 20;
        var y = Input.GetAxis("Vertical1") / 20;

        gameObject.transform.Translate(x, y, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _doorHit.DoorHitChange();
        }
    }
}
