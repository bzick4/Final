using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meme : MonoBehaviour
{
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();
   private void OnTriggerEnter(Collider other) 
   {
      if (other.CompareTag("Player"))
      {
         _soundManager.SoundMeme2();
      }
   }
}
