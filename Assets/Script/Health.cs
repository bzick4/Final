
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Health : MonoBehaviour
{
    private float _MaxHealth = 1000;
    [SerializeField] private Image _ImageHP;
    [SerializeField] private Image _ImageHPBackground;

     public float _currentHealth{ get; set;}
     
     private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK => GetComponent<WeaponEquipTwoHandedIK>();
     
    public static Action OnDamage;

    private void Start()
    {
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

    public void TakeDamage(float _damage)
    {
        _currentHealth -= _weaponEquipTwoHandedIK !=null && _weaponEquipTwoHandedIK.weaponInHand ? _damage * 0.5f : _damage;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
         UpdateHpBar();
         OnDamage?.Invoke();
         Debug.Log(_damage);
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

    public void Heal(float _heal)
    {
        _currentHealth += _heal;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
        UpdateHpBar();
    }


}
