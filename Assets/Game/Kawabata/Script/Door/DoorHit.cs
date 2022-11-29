using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorHit : MonoBehaviour
{
    //�h�A�����Ă��邩�ǂ���
    static public bool _doorClosed = true;
    public Action DoorChange;

    public void DoorHitChange()
    {
        _doorClosed = !_doorClosed;
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        //DoorChange();

        if (_doorClosed)
        {
            collider.enabled = true;
            
        }
        else
        {
            collider.enabled = false;
        }

        foreach (Transform child in transform)
        {
            child.GetComponent<DoorSprite>().DoorChangeSprite();
        }

    }

    public bool IsDoorClose() { return _doorClosed; }
}
