using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Ray�̒���")]
    [SerializeField] float _rayDistance;
    [Header("�v���C���[�̃C���x���g��")]
    [SerializeField] Inventory _inventory;
    
    PlayerInput _playerInput;

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
            Debug.Log("Collider�������Ă���");
            if (collider.TryGetComponent(out IAddItem AddItem))
            {
                //���ݎ����Ă���A�C�e����n��
                var item = AddItem.ReceiveItems(_inventory.ReceiveItems());
                //�A���Ă����f�[�^���C���x���g���ɓn��
                if (item != null) 
                {
                    _inventory.SetItemData(item);
                }
              
            }
            else if (collider.TryGetComponent(out IPickUp PickedUpItems)) 
            {
                Debug.Log("�A�C�e���擾����");
                _inventory.SetItemData(PickedUpItems.PickUpItem());
            }

            Debug.Log("��������");
           //AddItemInterface����ꂽ��A�C�e����n�����������s����
            
           //ItemBase����ꂽ��C���x���g���ɃA�C�e���̏���n��
        }
    }
}
