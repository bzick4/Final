using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioClip _SoundGame, _SoundMenu, 
                                        _SoundClick, _SoundHit, 
                                        _SoundHeal, _SoundDeath;
    


    public bool isPlaying { get; private set; }
    private AudioSource _sound;
    
    private void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.clip = _SoundGame;
    }
    
    public void SoundHeal()
    {
        PlaySound(_SoundHeal);
    }

    public void SoundHit()
    {
        PlaySound(_SoundHit);
    }       

    public void SoundDeath()
    {
       PlaySound(_SoundDeath);
    }
    // public void SoundClick()
    // {
    //     _SoundClick.Play();
    // }
    public void SoundGame()
    {
        // if (_SoundGame.isPlaying)
        // {
        //     _SoundGame.Pause();
        // }
        // else
        // {
        //     _SoundGame.Play();
        // }
    }
    // public void SoundMenu()
    // {
    //     if (_SoundMenu.isPlaying)
    //     {
    //         _SoundMenu.Pause();
    //     }
    //     else
    //     {
    //         _SoundMenu.Play();
    //     }
    // }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _sound.PlayOneShot(clip); // Воспроизводим звук поверх текущего
        }
    }

}
