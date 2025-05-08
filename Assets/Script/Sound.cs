using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private Slider _SliderMusic, _SliderSound;


    public bool isPlaying { get; private set; }
    private SoundManager _sound => GetComponent<SoundManager>();

    private void Awake()
    {
        Load();
    }

    private void Update()
    {
        _sound._SoundMenu.volume = _SliderMusic.value;
        _sound._SoundGame.volume = _SliderMusic.value;
        _sound._SoundHit.volume = _SliderSound.value;
        _sound._SoundHeal.volume = _SliderSound.value;
        _sound._SoundDeath.volume = _SliderSound.value;
        _sound._SoundClick.volume = _SliderSound.value;
        _sound._SoundGameOver.volume = _SliderSound.value;
        _sound._SoundFinish.volume = _SliderSound.value;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", _SliderMusic.value);
        PlayerPrefs.SetFloat("SoundVolume", _SliderSound.value);
    }
    
    public void Load()
    {
        _SliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", _SliderMusic.value);
        _SliderSound.value = PlayerPrefs.GetFloat("SoundVolume", _SliderSound.value);
    }
    
}
