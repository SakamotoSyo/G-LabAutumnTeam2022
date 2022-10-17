using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Rayの長さ")]
    [SerializeField] float _rayDistance;
    
    PlayerInput _playerInput;
    Inventory _inventory;

    void Start()
    {
        //インベントリスクリプトをGetComponentする

        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// インタラクトをする処理
    /// </summary>
    public void Interact() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerInput.MoveInput, _rayDistance);
        Debug.DrawRay(transform.position, _playerInput.MoveInput, Color.black, 5);
        if (hit.collider) 
        {
           //AddItemInterfaceが取れたらアイテムを渡す処理を実行する
            
           //ItemBaseが取れたらインベントリにアイテムの情報を渡す
        }
    }
}
