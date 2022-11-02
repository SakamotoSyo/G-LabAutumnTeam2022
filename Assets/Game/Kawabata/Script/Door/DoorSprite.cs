using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprite : MonoBehaviour
{
    [SerializeField,Header("�h�A����Ԃ̃X�v���C�g")]
    Sprite _spriteClose;
    [SerializeField, Header("�h�A�J����Ԃ̃X�v���C�g")]
    Sprite _spriteOpen;

    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteClose == null)
        {
            Debug.LogError("�h�A����Ԃ̃X�v���C�g���Z�b�g����Ă��܂���");
        }
        if(_spriteOpen == null)
        {
            Debug.LogError("�h�A�J����Ԃ̃X�v���C�g���Z�b�g����Ă��܂���");
        }
    }



    public void DoorChangeSprite()
    {
        var closed = gameObject.GetComponent<DoorHit>().IsDoorClose();

        if (closed) _spriteRenderer.sprite = _spriteClose;
        else _spriteRenderer.sprite = _spriteOpen;
    }
}
