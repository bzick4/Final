using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _FinishMenu;
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();
    private Pause _pause => FindObjectOfType<Pause>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _FinishMenu.SetActive(true);
            _soundManager._SoundFinish.Play();
            _soundManager._SoundGame.Stop();
           Invoke(nameof(Pause), 0.5f);
           
        }
    }
    
    private void Pause()
    {
        _pause.ScriptOff();
    }
}
