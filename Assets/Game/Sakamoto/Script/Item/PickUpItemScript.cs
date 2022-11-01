using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemScript : MonoBehaviour, IPickUp
{
    [Header("アイテムデータ")]
    [SerializeField] ItemData _data;

    [Tooltip("Sprite")]
    SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _data._itemSprite;
    }

    public ItemData PickUpItem()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.2f);
        return _data;
    }
}
