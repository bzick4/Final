
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Random = System.Random;

public class Health : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume => FindObjectOfType<PostProcessVolume>();
    private Vignette _vignette;

    private float _MaxHealth = 1000;
    [SerializeField] private Image _ImageHP;
    [SerializeField] private Image _ImageHPBackground;

     public float _currentHealth{ get; set;}
     
     private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK => GetComponent<WeaponEquipTwoHandedIK>();

    public static Action OnDamage;

    private void Start()
    {
        _currentHealth = _MaxHealth;

         if (_postProcessVolume.profile.TryGetSettings(out _vignette))
        {
            Debug.Log("Vignette эффект найден.");
        }
        else
        {
            Debug.LogWarning("Vignette эффект не найден в профиле.");
        }
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

        CriticalHP();
        
    }

    public void TakeDamage(float _damage)
    {
        _currentHealth -= _weaponEquipTwoHandedIK !=null && _weaponEquipTwoHandedIK.weaponInHand ? _damage * 0.5f : _damage;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _MaxHealth);
         UpdateHpBar();
         
         OnDamage?.Invoke();
         
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

    public void CriticalHP()
    {
        if (CompareTag("Player"))
    {
        if (_currentHealth <= 299)
        {
        
                StartCoroutine(PulseVignette());
                Debug.Log("Vignette effect started.");
            
        }
        if(_currentHealth >= 300)
        {
                StopCoroutine(PulseVignette());
                _vignette.active = false;
                Debug.Log("Vignette effect stopped.");
        }
    }
    }

    private IEnumerator PulseVignette()
{
    while (true)
    {
        _vignette.active = true;
            _vignette.intensity.value = Mathf.Lerp(0.15f, 0.25f, Mathf.PingPong(Time.time, 0.5f));
        

        yield return null;
    }
}

}
