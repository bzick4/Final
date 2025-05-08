using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _Trap, _Enemy;
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            _Trap.GetComponent<Rigidbody>().useGravity = true;
            _Trap.GetComponent<Rigidbody>().isKinematic = false;
            _Enemy.GetComponent<Rigidbody>().useGravity = true;
            _Enemy.GetComponent<Health>().TakeDamage(1000);
            _soundManager.SoundMeme();
        }
        
    }
}
