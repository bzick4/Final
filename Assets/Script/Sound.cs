using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public bool isPlaying { get; private set; }
    private AudioSource _sound;
    
    private void Start()
    {
        isPlaying = false;
        _sound = GetComponent<AudioSource>();
    }
    
    public void PlayOrPause()
    {
        if (_sound.isPlaying)
        {
            _sound.Pause();
        }
        else
        {
            _sound.Play();
        }
    }
}
