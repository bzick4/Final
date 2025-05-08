using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  
    
    [SerializeField] private Transform _FirePoint; // Точка выстрела
    [SerializeField] private float _BulletSpeed = 10;

    [SerializeField] private float _RayLength = 10f; // Длина рейкаста
     public LayerMask HeroLayer; 

    private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK => GetComponent<WeaponEquipTwoHandedIK>();
    private PoolObject _BulletPool=> FindObjectOfType<PoolObject>();
   
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();



    private void Start()
    {
        StartCoroutine(CallShootEnemyRepeatedly());
    }

    private void Update()
    {
        if(_weaponEquipTwoHandedIK != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _weaponEquipTwoHandedIK.weaponInHand)
        {
            ActiveShoot();
        }
        }
        
    }

    private void ActiveShoot()
    {

        GameObject bullet = _BulletPool.GetObject();

        _soundManager.SoundHit();

        bullet.transform.position = _FirePoint.position;
        bullet.transform.rotation = _FirePoint.rotation;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = _FirePoint.up * _BulletSpeed;
        }

        StartCoroutine(ReturnBulletToPool(bullet, 2f));
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(0.9f);
        _BulletPool.ReturnObject(bullet);
    }

    private void ShootEnemy()
    {
        Ray ray = new Ray(_FirePoint.position, _FirePoint.up);
        if (Physics.Raycast(ray, out RaycastHit hit, _RayLength, HeroLayer))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            Health heroHealth = hit.collider.GetComponentInParent<Health>();
            if (heroHealth != null)
            {
                ActiveShoot();
            }
        }
        else
        {
            Debug.Log("Raycast missed.");
        }

        Debug.DrawRay(_FirePoint.position, _FirePoint.up * _RayLength, Color.green, 4f);
    
    }

    private IEnumerator CallShootEnemyRepeatedly()
{
    while (true)
    {
        ShootEnemy();
        yield return new WaitForSeconds(0.5f);
    }
}
    
    
}

