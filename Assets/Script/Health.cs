
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private Image _ImageHP;
    [SerializeField] private Image _ImageHPBackground;
    

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
        if (_ImageHPBackground.fillAmount > _ImageHP.fillAmount)
        {
            _ImageHPBackground.fillAmount = Mathf.Lerp(_ImageHPBackground.fillAmount, _ImageHP.fillAmount, Time.deltaTime * 2f);
        }
        else
        {
            _ImageHPBackground.fillAmount = _ImageHP.fillAmount;
        }

    }

    public void TakeDamage(float damage)
    {
        if (_weaponEquipTwoHandedIK !=null && _weaponEquipTwoHandedIK.weaponInHand)
        {
            _currentHealth -= damage * 0.5f;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
            UpdateHpBar();
       
            Debug.Log("OnDamage event invoked with weapon in hand");
            OnDamage?.Invoke();
        }
    
        else
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
            UpdateHpBar();
            
            Debug.Log("OnDamage event invoked with weapon in hand");
            OnDamage?.Invoke();
        }
    }

    public void UpdateHpBar()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothHpBarUpdate());
    }

    private IEnumerator SmoothHpBarUpdate()
{
    float startFill = _ImageHP.fillAmount;
    float targetFill = _currentHealth / _MaxHealth;
    float duration = 0.5f;
    float elapsed = 0f;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        _ImageHP.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / duration);
        yield return null;
    }

    _ImageHP.fillAmount = targetFill;
}


}
