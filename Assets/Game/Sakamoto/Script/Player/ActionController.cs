using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Rayの長さ")]
    [SerializeField] float _rayDistance;
    [Header("プレイヤーのインベントリ")]
    [SerializeField] Inventory _inventory;
    [SerializeField]Animator _anim;

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
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _playerInput.LastMoveDir, _rayDistance);
        Debug.DrawRay(transform.position, _playerInput.LastMoveDir, Color.black, _rayDistance);
        if (hit.Length != 0) 
        {
            for(int i = 0; i < hit.Length; i++) 
            {
                if (hit[i].collider.TryGetComponent(out IAddItem AddItem))
                {
                    //現在持っているアイテムを渡す
                    var item = AddItem.ReceiveItems(_inventory.ReceiveItems());
                    //帰ってきたデータをインベントリに渡す
                    if (item != null)
                    {
                        _inventory.SetItemData(item);
                    }

                }

                if (hit[i].collider.TryGetComponent(out IPickUp PickedUpItems))
                {
                    if (_inventory.ItemInventory == null)
                    {
                        _inventory.SetItemData(PickedUpItems.PickUpItem());
                    }

                }

                if (hit[i].collider.TryGetComponent(out ICraftItem CraftItem)) 
                {
                    StartCoroutine(CraftAction(CraftItem.Craft(), CraftItem));
                }
                
            }
         
        }
    }

    IEnumerator CraftAction(float craftTime, ICraftItem craftItem) 
    {
        if (craftTime != 0) 
        {
            _anim.SetBool("Craft", true);
            _playerInput.InputBlock();
            yield return new WaitForSeconds(craftTime);
            _anim.SetBool("Craft", false);
            _playerInput.InputBlock();
            craftItem.CraftEnd();
        }

        yield return null;
    }
}
