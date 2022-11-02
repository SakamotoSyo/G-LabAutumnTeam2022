using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    GameObject _door;
    DoorHit _doorHit;
    DoorSprite _doorSprite;

    private void Start()
    {
        _door = GameObject.Find("Door");
        _doorHit = _door.GetComponent<DoorHit>();
        _doorSprite = _door.GetComponent<DoorSprite>();
    }
    //テスト用のプレイヤーコントローラー
    void Update()
    {
        _door = GameObject.Find("Door");
        var x = Input.GetAxis("Horizontal") / 20;
        var y = Input.GetAxis("Vertical") / 20;

        gameObject.transform.Translate(x, y, 0);

        if (Input.GetKeyDown(KeyCode.Return))
        {

            _doorHit.DoorHitChange();
            _doorSprite.DoorChangeSprite();
        }
    }
}
