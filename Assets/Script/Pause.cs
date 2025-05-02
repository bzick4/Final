using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _PauseMenu;
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();

    private bool isPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPause) 
            {
                isPause = true;
                _PauseMenu.SetActive(true);
                PauseOn();
            }
            else
            {
                isPause = false;
                _PauseMenu.SetActive(false);
                PauseOff();
            }
        }
    }
    
    public void ScriptOff()
    {
        foreach (var health in FindObjectsOfType<Health>())
        {
            health.enabled = false;
        }

        foreach(var useBottle in FindObjectsOfType<UseBottle>())
        {
            useBottle.enabled = false;
        }

        foreach(var capsuleCollider in FindObjectsOfType<CapsuleCollider>())
        {
            capsuleCollider.enabled = false;
        }


        foreach (var weaponEquipTwoHandedIK in FindObjectsOfType<WeaponEquipTwoHandedIK>())
        {
            weaponEquipTwoHandedIK.enabled = false;
        }

        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            bullet.enabled = false;
        }

    

        foreach (var poolObject in FindObjectsOfType<PoolObject>())
    {
        poolObject.enabled = false;
    }

    foreach (var shoot in FindObjectsOfType<Shoot>())
    {
        shoot.enabled = false;
    }

    foreach (var enemy in FindObjectsOfType<Enemy>())
    {
        enemy.enabled = false;
    }

    foreach (var characterController in FindObjectsOfType<CharacterController>())
    {
        characterController.enabled = false;
    }

    foreach (var health in FindObjectsOfType<Health>())
    {
        health.enabled = false;
    }

    foreach (var controller in FindObjectsOfType<Controller>())
    {
        controller.enabled = false;
    }

     
    }

    private void ScriptOn()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            bullet.enabled = false;
        }

        foreach (var useBottle in FindObjectsOfType<UseBottle>())
        {
            useBottle.enabled = true;
        }


        foreach (var capsuleCollider in FindObjectsOfType<CapsuleCollider>())
        {
            capsuleCollider.enabled = true;
        }
        
        foreach (var weaponEquipTwoHandedIK in FindObjectsOfType<WeaponEquipTwoHandedIK>())
        {
            weaponEquipTwoHandedIK.enabled = true;
        }
      

        foreach (var poolObject in FindObjectsOfType<PoolObject>())
    {
        poolObject.enabled = true;
    }

    foreach (var shoot in FindObjectsOfType<Shoot>())
    {
        shoot.enabled = true;
    }

    foreach (var enemy in FindObjectsOfType<Enemy>())
    {
        enemy.enabled = true;
    }

    foreach (var characterController in FindObjectsOfType<CharacterController>())
    {
        characterController.enabled = true;
    }

    foreach (var health in FindObjectsOfType<Health>())
    {
        health.enabled = true;
    }

    foreach (var controller in FindObjectsOfType<Controller>())
    {
        controller.enabled = true;
    }

   
    
    }


    private void PauseOn()
    {
         _soundManager._SoundGame.Pause();
      _soundManager._SoundMenu.Play();
        ScriptOff();
    }
    private void PauseOff()
    {
        _soundManager._SoundGame.Play();
    _soundManager._SoundMenu.Stop();
        ScriptOn();
    }
}
