using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDead : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(1000);
            Debug.Log("Player Dead");
        }
    }
}
