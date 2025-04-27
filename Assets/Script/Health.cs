using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
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
        if (_weaponEquipTwoHandedIK !=null)
        {
            if (_weaponEquipTwoHandedIK.weaponInHand)
            {
                _currentHealth -= damage * 0.5f;
                OnDamage?.Invoke();
            }
        
        else
        {
            _currentHealth -= damage;
            OnDamage?.Invoke();
        }
        }

        


       
    }


}
