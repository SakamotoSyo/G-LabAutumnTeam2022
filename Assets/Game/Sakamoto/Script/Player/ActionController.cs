using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Rayの長さ")]
    [SerializeField] float _rayDistance;
    [Header("プレイヤーのインベントリ")]
    [SerializeField] Inventory _inventory;
    
    PlayerInput _playerInput;

    void Start()
    {
        //インベントリスクリプトをGetComponentする

        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (_playerInput.Action) 
        {
            Debug.Log("いんたー");
            Interact();
        }
    }

    /// <summary>
    /// インタラクトをする処理
    /// </summary>
    public void Interact() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerInput.MoveInput, _rayDistance);
        Debug.DrawRay(transform.position, _playerInput.MoveInput, Color.black, 5);
        var collider = hit.collider;
        if (collider != null) 
        {
            if (collider.TryGetComponent(out IAddItem AddItem))
            {
                //現在持っているアイテムを渡す
                var item = AddItem.ReceiveItems(_inventory.ReceiveItems());
                //帰ってきたデータをインベントリに渡す
                if (item != null) 
                {
                    _inventory.SetItemData(item);
                }
              
            }
            else if (collider.TryGetComponent(out IPickUp PickedUpItems)) 
            {
                Debug.Log("アイテム取得した");
                if (_inventory.ItemInventory == null) 
                {
                    _inventory.SetItemData(PickedUpItems.PickUpItem());
                }
                
            }
        }
    }
}
