using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _enemyHealth;
    private Animator _enemyAnimator;
    private RagdollHandler _ragdollHandler;
    private HealtBottleManeger _healtBottleManeger;
    private Points _points;
    private KillEnemy _killEnemy;

    private bool _isDead = false;

    private void Awake()
    {
        _enemyHealth = GetComponent<Health>();
    }

    private void Start()
    {
        
        _enemyAnimator = GetComponent<Animator>();
        _ragdollHandler = GetComponentInChildren<RagdollHandler>();

        _healtBottleManeger = FindAnyObjectByType<HealtBottleManeger>();
        _points = FindAnyObjectByType<Points>();

        _killEnemy = GetComponent<KillEnemy>();

        _ragdollHandler.Instalize();
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
        if (_enemyHealth._currentHealth <=0 && !_isDead)
        {
            _isDead = true;
            int _randomLoot = Random.Range(0,3);
             int _randomPoints = Random.Range(100, 301);

            _ragdollHandler.EnableRagdoll();
            _enemyAnimator.enabled = false;

            _killEnemy.AddBonus(1);
            _points.AddBonus(_randomPoints);

           if (_randomLoot == 2)
           {
               Debug.Log("Loot");
               _healtBottleManeger.AddBonus(1);
           }
           else
           {
               Debug.Log("No loot");
           }

           Destroy(gameObject, 2f);
           Debug.Log("Enemy is dead");

        }
    }
}
