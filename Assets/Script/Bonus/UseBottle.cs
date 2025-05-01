using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBottle : MonoBehaviour
{
    private HealtBottleManeger _totalBottle, _healBottle;
    private Health _heroHealth;
    
    private void Awake()
    {
        _heroHealth = GetComponent<Health>();
        _healBottle = GetComponentInChildren<HealtBottleManeger>();
        _totalBottle = GetComponentInChildren<HealtBottleManeger>();
    }

    private void Update()
    {
        DrinkHealBottle();
        Cheat();
    }

    private void CureHero()
    {
        _heroHealth?.TakeDamage(-40);
    }
    
    private void DrinkHealBottle()
    {
        if (Input.GetKeyDown(KeyCode.I) && _totalBottle.TotalHealBottle > 0)
        {
            _healBottle.Remove(1);
            CureHero();
        }
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _totalBottle.AddBonus(5);
        }
    }
}
