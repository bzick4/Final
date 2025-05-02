using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioSource _SoundMenu, _SoundGame, 
                    _SoundClick, _SoundHit, 
                    _SoundHeal, _SoundDeath, _SoundGameOver;

    

    
    public void SoundHeal()
    {
        _SoundHeal.Play();
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
