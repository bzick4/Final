using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePerson : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.Rotate(0, 90, 0); 
            
            Debug.Log("Player entered the trigger zone.");
        }
    }
}
