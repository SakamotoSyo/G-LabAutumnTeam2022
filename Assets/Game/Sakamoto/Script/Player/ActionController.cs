using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

[Serializable]
public class ActionController
{
    [Header("Rayの長さ")]
    [SerializeField] float _rayDistance;
    [Header("プレイヤーのインベントリ")]
    [SerializeField] Inventory _inventory;
    [SerializeField] Animator _anim;
    [SerializeField] PlayerHp _playerHp;
    [SerializeField]PlayerInput _playerInput;

    bool _isDead = false;
    Vector2 _wrappingDir = new Vector2(0, 1);


    public void Start() 
    {
        _playerHp.OnHealth += OnHelthChanged;
    }


    /// <summary>
    /// インタラクトをする処理
    /// </summary>
    public async void Interact(Transform transform) 
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _playerInput.LastMoveDir, _rayDistance);
        Debug.DrawRay(transform.position, _playerInput.LastMoveDir, Color.black, _rayDistance);
        if (hit.Length != 0) 
        {
            for(int i = 0; i < hit.Length; i++) 
            {
                if (!_isDead)
                {
                    if (hit[i].collider.TryGetComponent(out IAddItem AddItem))
                    {
                        if (_inventory.ItemInventory != null)
                        {
                            AudioManager.Instance.PlaySound(SoundPlayType.Put);
                        }
                        //現在持っているアイテムを渡す
                        var item = AddItem.ReceiveItems(_inventory.ReceiveItems());
                        //帰ってきたデータをインベントリに渡す
                        if (item != null)
                        {
                            AudioManager.Instance.PlaySound(SoundPlayType.PickUp);
                            _inventory.SetItemData(item);
                        }
                    }

                    if (hit[i].collider.TryGetComponent(out IPickUp PickedUpItems))
                    {
                        if (_inventory.ItemInventory == null)
                        {
                            AudioManager.Instance.PlaySound(SoundPlayType.PickUp);
                            _inventory.SetItemData(PickedUpItems.PickUpItem());
                        }
                    }

                    if (hit[i].collider.TryGetComponent(out ICraftItem CraftItem))
                    {
                        await CraftAction(CraftItem.Craft(), CraftItem);
                    }

                }

                if (hit[i].collider.TryGetComponent(out DoorHit door)) 
                {
                    door.DoorHitChange();
                }
                
            }
         
        }
    }

    private async UniTask CraftAction(float craftTime, ICraftItem craftItem) 
    {
        if (craftTime != 0) 
        {
            var dir = _playerInput.PlayerDir;
            if (dir == _wrappingDir)
            {
                _anim.SetBool("Wrapping", true);
            }
            else 
            {
                _anim.SetBool("Craft", true);
            }
            _playerInput.InputBlock();
            await UniTask.Delay(TimeSpan.FromSeconds(craftTime));
            if (dir == _wrappingDir)
            {
                _anim.SetBool("Wrapping", false);
            }
            else
            {
                _anim.SetBool("Craft", false);
            }
            _playerInput.InputBlock();
            craftItem.CraftEnd();
        }
    }

    private void OnHelthChanged(float amount)
    {
        if (amount <= 0)
        {
            _isDead = true;
            Debug.Log("si");
        }
        else 
        {
            _isDead = false;
        }
    }
}
