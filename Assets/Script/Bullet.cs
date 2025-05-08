using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Bullet : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume => FindObjectOfType<PostProcessVolume>();
     private ChromaticAberration _chromaticAberration; //=> _postProcessVolume.GetComponent<ChromaticAberration>();

   private float _randomDamageForEnemy, _randomDamageForPlayer;

   private  void Awake()
   {
        
        if (_postProcessVolume.profile.TryGetSettings(out _chromaticAberration))
        {
            Debug.Log("Chromatic Aberration эффект найден.");
        }
        else
        {
            Debug.LogWarning("Chromatic Aberration эффект не найден в профиле.");
        }
   }
   private void  OnTriggerEnter(Collider other)
   {
    _randomDamageForEnemy = Random.Range(200, 300);
    _randomDamageForPlayer = Random.Range(100, 200);


    if(other.GetComponent<Health>() != null)
    {
        Health health = other.GetComponent<Health>();
        
        if(other.CompareTag("Player"))
        {
            health.TakeDamage(_randomDamageForPlayer);
            Debug.Log("Player Hit" + _randomDamageForPlayer);
           _chromaticAberration.active = true;
           Invoke(nameof(ChromaticAberration), 0.2f);
        }
        if(other.CompareTag("Enemy"))
        {
            health.TakeDamage(_randomDamageForEnemy);
            Debug.Log("Enemy Hit" + _randomDamageForEnemy);
        }
    }

    PoolObject pool = FindObjectOfType<PoolObject>();
    if (pool != null)
    {
        pool.ReturnObject(gameObject);
    }
    
   }

   private void ChromaticAberration()
   {
       
        _chromaticAberration.active = false;
    
   }

}
   

