using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CapsuleCollider _enemyCollider => GetComponent<CapsuleCollider>();
    private Health _enemyHealth => GetComponent<Health>();
    private Animator _enemyAnimator => GetComponent<Animator>();
    private RagdollHandler _ragdollHandler => GetComponentInChildren<RagdollHandler>();
    private HealtBottleManeger _healtBottleManeger => FindAnyObjectByType<HealtBottleManeger>();
    private Points _points => FindAnyObjectByType<Points>();
    private KillEnemy _killEnemy => FindAnyObjectByType<KillEnemy>();
    private SoundManager _soundManager => FindAnyObjectByType<SoundManager>();

    private bool _isDead = false;

    // private void Awake()
    // {
    //     _enemyHealth = GetComponent<Health>();
    // }

    private void Start()
    {
        _ragdollHandler.Instalize();
    }


    private void OnEnable()
    { 
        if (_enemyHealth != null) 
        { 
            Health.OnDamage += Dead;
        } 
    }

    private void OnDisable()
    {
        if (_enemyHealth != null)
        {
            Health.OnDamage -= Dead;
        }
    
    }
    
    private void Dead()
    {
        if (_enemyHealth._currentHealth <=0 && !_isDead)
        {
            _enemyCollider.enabled = false;

            _isDead = true;
            int _randomLoot = Random.Range(0,3);
             int _randomPoints = Random.Range(100, 301);
             
             _enemyAnimator.enabled = false;
            _ragdollHandler.EnableRagdoll();
           
            _soundManager.SoundDeath();

            _killEnemy.AddBonus(1);
            _points.AddBonus(_randomPoints);

           if (_randomLoot == 2)
           {
               Debug.Log("Loot");
               _healtBottleManeger.AddBonus(1);
           }
           else
           {
               Debug.Log("No loot");
           }

           Destroy(gameObject, 2f);

           Debug.Log("Enemy is dead");

        }
    }
}
