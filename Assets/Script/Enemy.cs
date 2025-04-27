using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _enemyHealth;

    private void Start()
    {
        _enemyHealth = GetComponent<Health>();
    }  

    private void Update()
    {
        Dead();
    }


    private void OnEnable()
    { 
        if (_enemyHealth != null) 
        { 
            Health.OnDamage += Dead;
        } 
    }

    private void OnDisable()
    {
        if (_enemyHealth != null)
        {
            Health.OnDamage -= Dead;
        }
    }
    
    private void Dead()
    {
        if (_enemyHealth._currentHealth <=0)
        {
            Destroy(gameObject);
           Debug.Log("Enemy is dead");
        }
    }
}
