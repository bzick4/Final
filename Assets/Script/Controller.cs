using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _Speed = 3f;
    [SerializeField] private float  _RunSpeed = 5f;
    private float _currentSpeed;
    private bool _isRun;

    [Header("Gravity")]
    [SerializeField] private float _FallThreshold = 5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 15f;
    private float _fallStartHeight;
    private Vector3 _velocityMove;
    private Vector3 _horizontalMove;
    private bool _isGrounded;
    private bool _isAlive=true;

    // Script
    private Animator _animator => GetComponent<Animator>();
    private CharacterController _characterController => GetComponent<CharacterController>();
    private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK => GetComponent<WeaponEquipTwoHandedIK>();
    private RagdollHandler _ragdollHandler => GetComponentInChildren<RagdollHandler>();
    private Health _health => GetComponent<Health>();
    private CapsuleCollider _capsuleColliderHero => GetComponent<CapsuleCollider>();
    private Collider _colliderHero => GetComponent<Collider>();

    private void Awake()
    {
        _currentSpeed = _Speed;
        _isRun = false;
    }

    private void Start()
    {
        _ragdollHandler.Instalize();
    }

    private void Update()
    {
        Falling();
        PlayerMovement();
        Jump();
    }

    private void PlayerMovement()
    {
   
if (_isAlive)
{
    float _horiz = Input.GetAxis("Horizontal");
    
     if (_weaponEquipTwoHandedIK.weaponInHand)
    {
        _isRun = false;
        _currentSpeed = _Speed;
    }
    else
    {
        _isRun = Input.GetKey(KeyCode.RightShift);
    }


    _currentSpeed = _isRun ? _RunSpeed : _Speed;

     _horizontalMove = new Vector3(_horiz * _currentSpeed, 0, 0);
    
    _characterController.Move(_horizontalMove * Time.deltaTime);

    if (_horiz > 0)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    else if (_horiz < 0)
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    _animator.SetFloat("Lockomotion", _isRun ? 2f : Mathf.Abs(_horiz), 0.2f, Time.deltaTime);
    }
    }

    private void Jump()
{
    
    _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);

    if (_isGrounded && _velocityMove.y < 0)
    {
        _velocityMove.y = -2f; // Сбрасываем вертикальную скорость
    }

    if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded  && _horizontalMove.x == 0)
    {
       _velocityMove.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

     if (_weaponEquipTwoHandedIK.weaponInHand == true)
        {
            _animator.SetTrigger("JumpWeapon");
        }
        else
        {
            _animator.SetTrigger("Jump");
        }
    }

     if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded && _horizontalMove.x > 0)
     {
       _velocityMove.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    
      if (_weaponEquipTwoHandedIK.weaponInHand== true)
        {
            _animator.SetTrigger("RunJumpWeapon");
        }
        else
        {
            _animator.SetTrigger("RunJump");
        }
     }  

     if(Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded && _horizontalMove.x < 0)
     {
       _velocityMove.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    
      if (_weaponEquipTwoHandedIK.weaponInHand == true)
        {
            _animator.SetTrigger("RunJumpWeapon");
        }
        else
        {
            _animator.SetTrigger("RunJump");
        }
   }


    // Применяем гравитацию
    _velocityMove.y += _gravity * Time.deltaTime;

    // Применяем движение
    _characterController.Move(_velocityMove * Time.deltaTime);
}

    private void Falling()
{
    if (!_isGrounded && _velocityMove.y < 0)
    {
       if (_fallStartHeight == 0)
        {
            _fallStartHeight = transform.position.y; 
        }

        if (_weaponEquipTwoHandedIK.weaponInHand == true)
        {
            _animator.SetBool("FallingWeapon", true);
        }
        else
        {
            _animator.SetBool("Falling", true);
        }
    }
    // Если персонаж приземлился
    else if (_isGrounded)
    {
        if(_weaponEquipTwoHandedIK.weaponInHand == true)
        {
            _animator.SetBool("FallingWeapon", false);
        }
        else
        {
            _animator.SetBool("Falling", false);
        }
       

        if (_fallStartHeight > 0)
        {
            float fallDistance = _fallStartHeight - transform.position.y;

            if (fallDistance > _FallThreshold)
            {
                if (_weaponEquipTwoHandedIK.weaponInHand == true)
                {
                    _animator.SetTrigger("RollWeapon");
                }
                else
                {
                    _animator.SetTrigger("Roll");
                }
            }
            Debug.Log($"Fall distance: {fallDistance}");
            _fallStartHeight = 0;
        }
    }
}

    private void OnEnable()
    { 
        if (_health != null) 
        { 
            Health.OnDamage += Dead;
        } 
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            Health.OnDamage -= Dead;
        }
    }
    
    private void Dead()
    {
        if (_health._currentHealth <=0)
        {
            _isAlive = false;
            Debug.Log("Dead");

            _ragdollHandler.EnableRagdoll();
            _animator.enabled = false;
            _capsuleColliderHero.enabled = false;
            _colliderHero.enabled = false;
            _characterController.enabled = false;
            
        }
    }


}
