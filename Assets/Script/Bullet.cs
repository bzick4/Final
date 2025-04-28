using UnityEngine;

public class Bullet : MonoBehaviour
{
  
   private void  OnTriggerEnter(Collider other)
   {
    if(other.GetComponent<Health>() != null)
    {
        Health health = other.GetComponentInParent<Health>();
        health.TakeDamage(20);
    }

    PoolObject pool = FindObjectOfType<PoolObject>();
    if (pool != null)
    {
        pool.ReturnObject(gameObject);
    }
    
   }

}
   

