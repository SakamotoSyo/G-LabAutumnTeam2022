using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHit : MonoBehaviour
{
    //ドアが閉じているかどうか
    static public bool _doorClosed = true;


    public void DoorHitChange()
    {
        _doorClosed = !_doorClosed;
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();

        if (_doorClosed)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }

    }

    public bool IsDoorClose() { return _doorClosed; }
}
