using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorHit : MonoBehaviour
{
    [Header("Door‚ÌCoolider")]
    [SerializeField] BoxCollider2D _boxCollider2D;
    //ƒhƒA‚ª•Â‚¶‚Ä‚¢‚é‚©‚Ç‚¤‚©
    public bool _doorClosed = true;
    public Action DoorChange;

    public void DoorHitChange()
    {
        _doorClosed = !_doorClosed;
        //DoorChange();

        if (_doorClosed)
        {
            _boxCollider2D.enabled = true;
            AudioManager.Instance.PlaySound(SoundPlayType.SE_door_open);
        }
        else
        {
            _boxCollider2D.enabled = false;
            AudioManager.Instance.PlaySound(SoundPlayType.SE_door_close);
        }

        foreach (Transform child in transform)
        {
            child.GetComponent<DoorSprite>().DoorChangeSprite();
        }

    }

    public bool IsDoorClose() { return _doorClosed; }
}
