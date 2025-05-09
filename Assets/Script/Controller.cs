using System.Collections;
using UnityEngine;


public class Controller : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _Speed = 3f;
    [SerializeField] private float  _RunSpeed = 5f;
    public Vector3 _horizontalMove {get; set;}
    private float _horiz;
    private float _currentSpeed;
    private bool _isRun;
    private bool _isAlive = true;


    [Header("Gravity")]
    [SerializeField] private float _FallThreshold = 5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 15f;
    private float _fallStartHeight;
    private Vector3 _velocityMove;
    private bool _isGrounded;
    

    // Script
    private Animator _animator => GetComponent<Animator>();
    public CharacterController _characterController => GetComponent<CharacterController>();
    private CapsuleCollider _capsuleCollider => GetComponent<CapsuleCollider>();
    private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK => GetComponent<WeaponEquipTwoHandedIK>();
    private RagdollHandler _ragdollHandler => GetComponentInChildren<RagdollHandler>();
    private Health _health => GetComponent<Health>();
    private SoundManager _soundManager => FindObjectOfType<SoundManager>();
    private Pause _pause => FindObjectOfType<Pause>();


    [SerializeField] private GameObject _PanelLose;


    
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
        Slide();
    }

    private void PlayerMovement()
    {
        if (_isAlive)
        {
            _horiz = Input.GetAxis("Horizontal");

            
            if (_weaponEquipTwoHandedIK.weaponInHand)
            {
                _isRun = false;
                _currentSpeed = _Speed;
            }

            else
            {
                _isRun = Input.GetKey(KeyCode.LeftShift);
            }
            
            _currentSpeed = _isRun ? _RunSpeed : _Speed;
             _horizontalMove = transform.forward * _horiz * _currentSpeed;
            

    
    if (_horiz > 0)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _characterController.Move(_horizontalMove * Time.deltaTime);
       
        
    }
    else if (_horiz < 0)
    {
        transform.rotation = Quaternion.Euler(0, -180, 0);
        _characterController.Move(-_horizontalMove * Time.deltaTime);
       
    }

    _animator.SetFloat("Lockomotion", _isRun ? 2f : Mathf.Abs(_horiz), 0.2f, Time.deltaTime);
    }
    }

    private void Jump()
{
    
    _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);

    if (_isGrounded && _velocityMove.y < 0)
    {
        _velocityMove.y = -2f;
    }

    if (Input.GetKeyDown(KeyCode.W) && _isGrounded  && _horizontalMove.x == 0)
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

     if (Input.GetKeyDown(KeyCode.W) && _isGrounded && _horizontalMove.x > 0 || Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded && _horizontalMove.x < 0)
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

//      if(Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded && _horizontalMove.x < 0)
//      {
//        _velocityMove.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    
//       if (_weaponEquipTwoHandedIK.weaponInHand == true)
//         {
//             _animator.SetTrigger("RunJumpWeapon");
//         }
//         else
//         {
//             _animator.SetTrigger("RunJump");
//         }
//    }

    _velocityMove.y += _gravity * Time.deltaTime;

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
            _animator.enabled = false;
            Debug.Log("Dead");

            _ragdollHandler.EnableRagdoll();
            _soundManager._SoundDeath.Play();
            _soundManager._SoundGame.Stop();
            Invoke(nameof(GameOver), 0.8f);

        }
    }

    private void GameOver()
    {
        _soundManager._SoundGameOver.Play();
        _PanelLose.SetActive(true);
        _pause.ScriptOff();
    }

private void Slide()
{
    if(Input.GetKeyDown(KeyCode.S) && _isGrounded && _horiz > 0 || Input.GetKeyDown(KeyCode.S) && _isGrounded && _horiz < 0)
    {
        StartCoroutine(SlideCoroutine());
        _animator.SetTrigger("Slide");
    }
}

private IEnumerator SlideCoroutine()
{
    _characterController.center = new Vector3(0, 0.3f, 0);
    _characterController.height = 0f;
    _capsuleCollider.enabled = false;
    _capsuleCollider.center = new Vector3(0, 0f, 0);
    yield return new WaitForSeconds(1.4f);
    _characterController.center = new Vector3(0, 1.03f, 0);
    _capsuleCollider.enabled = true;
   _capsuleCollider.center = new Vector3(0, 1.03f, 0);
    _characterController.height = 1.92f;
}

}
