using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Ray�̒���")]
    [SerializeField] float _rayDistance;
    
    PlayerInput _playerInput;
    Inventory _inventory;

    void Start()
    {
        //�C���x���g���X�N���v�g��GetComponent����

        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (_playerInput.Action) 
        {
            Interact();
        }
    }

    /// <summary>
    /// �C���^���N�g�����鏈��
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
               /* if (AddItem.ReceiveItems(�C���x���g���̒��̃A�C�e��)) 
                {
                   //�C���x���g���̒��̃A�C�e����n���Ď����Ă���A�C�e���͍폜
                }
                else
                {
                  
                }
                */

            }
           //AddItemInterface����ꂽ��A�C�e����n�����������s����
            
           //ItemBase����ꂽ��C���x���g���ɃA�C�e���̏���n��
        }
    }
}
