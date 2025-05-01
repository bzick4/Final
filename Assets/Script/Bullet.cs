using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private int _randomDamage;
   private void  OnTriggerEnter(Collider other)
   {
    _randomDamage = Random.Range(150, 301);

    if(other.GetComponent<Health>() != null)
    {
        Health health = other.GetComponent<Health>();
        health.TakeDamage(_randomDamage);
    }

    PoolObject pool = FindObjectOfType<PoolObject>();
    if (pool != null)
    {
        pool.ReturnObject(gameObject);
    }
    
   }

}
   

