using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerHp : MonoBehaviour, IDamage
{
   [Header("ÉvÉåÉCÉÑÅ[ÇÃHp")]
   [SerializeField] float _playerHp = 0;
   

   public Action OnDead;  
    
    public void ApplyDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyHeal(float amount)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
