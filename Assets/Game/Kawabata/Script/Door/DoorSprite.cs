using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprite : MonoBehaviour
{
    [SerializeField, Header("変更対象のスプライト")]
    private GameObject _target;
    [SerializeField,Header("ドア閉じ状態のスプライト")]
    Sprite _spriteClose;
    [SerializeField, Header("ドア開き状態のスプライト")]
    Sprite _spriteOpen;

    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = _target.GetComponent<SpriteRenderer>();

        if (_spriteClose == null)
        {
            Debug.LogError("ドア閉じ状態のスプライトがセットされていません");
        }
        if(_spriteOpen == null)
        {
            Debug.LogError("ドア開き状態のスプライトがセットされていません");
        }
    }



    public void DoorChangeSprite()
    {
        var closed = transform.parent.GetComponent<DoorHit>().IsDoorClose();

        if (closed) { _spriteRenderer.sprite = _spriteClose; }
        else { _spriteRenderer.sprite = _spriteOpen; }
    } 
}
