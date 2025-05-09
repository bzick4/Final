using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBottle : MonoBehaviour
{
    private HealtBottleManeger _totalBottle => GetComponentInChildren<HealtBottleManeger>();
    private HealtBottleManeger _healBottle => GetComponentInChildren<HealtBottleManeger>();
    private Health _heroHealth => GetComponent<Health>();
    private SoundManager _soundManager => FindAnyObjectByType<SoundManager>();
    private void Update()
    {
        DrinkHealBottle();
        Cheat();
    }

    private void CureHero()
    {
        _heroHealth?.Heal(400);
        _soundManager.SoundHeal();
    }
    
    private void DrinkHealBottle()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _totalBottle.TotalHealBottle > 0)
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
