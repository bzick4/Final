using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
   private void OnTriggerEnter(Collider other)
   {
    if(other.GetComponent<Health>() != null)
    {
        Health health = other.GetComponent<Health>();
        health.TakeDamage(20);
    }
   }
}
   

