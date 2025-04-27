using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  
    [SerializeField] private PoolObject _BulletPool; // Ссылка на пул объектов
    [SerializeField] private Transform _FirePoint; // Точка выстрела
    [SerializeField] private float _BulletSpeed = 10;
    private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK;
    private Health _health;
     private Animator _animator;


    [SerializeField] private float _RayLength = 10f; // Длина рейкаста
    [SerializeField] private LayerMask _HeroLayer; 

    private void Start()
    {
        _weaponEquipTwoHandedIK = GetComponent<WeaponEquipTwoHandedIK>();
        _animator = GetComponent<Animator>();  
        _health = GetComponent<Health>(); 
        StartCoroutine(CallShootEnemyRepeatedly());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _weaponEquipTwoHandedIK.weaponInHand) // ЛКМ для выстрела
        {
            ActiveShoot();
        }
    }

    private void ActiveShoot()
    {
        GameObject bullet = _BulletPool.GetObject();

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
        yield return new WaitForSeconds(delay);
        _BulletPool.ReturnObject(bullet);
    }

    private void ShootEnemy()
    {
        Ray ray = new Ray(_FirePoint.position, _FirePoint.up); // Создаём рейкаст
        if (Physics.Raycast(ray, out RaycastHit hit, _RayLength, _HeroLayer))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            // Проверяем, есть ли у объекта компонент Health
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

        Debug.DrawRay(_FirePoint.position, _FirePoint.up * _RayLength, Color.green, 10f);
    
    }

    private IEnumerator CallShootEnemyRepeatedly()
{
    while (true)
    {
        ShootEnemy();
        yield return new WaitForSeconds(0.8f);
    }
}
    
    
}

