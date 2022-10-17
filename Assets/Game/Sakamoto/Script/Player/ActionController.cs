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
        
    }

    /// <summary>
    /// �C���^���N�g�����鏈��
    /// </summary>
    public void Interact() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerInput.MoveInput, _rayDistance);
        Debug.DrawRay(transform.position, _playerInput.MoveInput, Color.black, 5);
        if (hit.collider) 
        {
           //AddItemInterface����ꂽ��A�C�e����n�����������s����
            
           //ItemBase����ꂽ��C���x���g���ɃA�C�e���̏���n��
        }
    }
}
