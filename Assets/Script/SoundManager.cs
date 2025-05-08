using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource _SoundMenu, _SoundGame, _SoundMeme,
                    _SoundClick, _SoundHit, _SoundFinish,
                    _SoundHeal, _SoundDeath, _SoundGameOver;

    

    
    public void SoundMeme()
    {
        _SoundMeme.Play();
    }

    
    public void SoundHeal()
    {
        _SoundHeal.Play();
    }

    public void SoundFinish()
    {
        _SoundFinish.Play();
    }


    public void SoundHit()
    {
        _SoundHit.Play();
    }       

    public void SoundDeath()
    {
       _SoundDeath.Play();
    }
    public void SoundClick()
    {
        _SoundClick.Play();
    }
    public void SoundGame()
    {
        _SoundGame.Play();
    }
    public void SoundMenu()
    {
        _SoundMenu.Play();
    }
    public void SoundGameOver()
    {
        _SoundGameOver.Play();
    }

    
}
