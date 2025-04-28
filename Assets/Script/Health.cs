
using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private Image _ImageHP;

     public float _currentHealth{ get; set;}
     
     private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK;
     
    public static Action OnDamage;

    private void Start()
    {
        _weaponEquipTwoHandedIK = GetComponent<WeaponEquipTwoHandedIK>();
        _currentHealth = _MaxHealth;
    }

     private void Update()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (_weaponEquipTwoHandedIK !=null && _weaponEquipTwoHandedIK.weaponInHand)
        {
            _currentHealth -= damage * 0.5f;
            UpdateHpBar();
            OnDamage?.Invoke();
        }
    
        else
        {
            _currentHealth -= damage;
            UpdateHpBar();
            OnDamage?.Invoke();
        }
    }

    public void UpdateHpBar()
    {
        _ImageHP.fillAmount = _currentHealth / _MaxHealth; 
    }

}
