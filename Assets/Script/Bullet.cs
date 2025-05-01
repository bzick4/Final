using UnityEngine;

public class Bullet : MonoBehaviour
{
  
   private void  OnTriggerEnter(Collider other)
   {
    if(other.GetComponent<Health>() != null)
    {
        Health health = other.GetComponent<Health>();
        health.TakeDamage(16);
    }

    PoolObject pool = FindObjectOfType<PoolObject>();
    if (pool != null)
    {
        pool.ReturnObject(gameObject);
    }
    
   }

}
   

