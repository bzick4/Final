using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float  _RunSpeed = 9f;

    private float _currentSpeed;
    private Animator _animator;
    private Vector3 _velocity;
    private CharacterController _characterController;
    private WeaponEquipTwoHandedIK _weaponEquipTwoHandedIK;

    private bool _isRun;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponEquipTwoHandedIK = GetComponent<WeaponEquipTwoHandedIK>();
        _characterController = GetComponent<CharacterController>();
        _currentSpeed = _Speed;
        _isRun = false;
        
        
    }

       void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
    float _horiz = Input.GetAxis("Horizontal");
    _isRun = Input.GetKey(KeyCode.RightShift);
    _currentSpeed = _isRun ? _RunSpeed : _Speed;

    _velocity = new Vector3(_horiz * _currentSpeed, 0, 0);
    
    _characterController.Move(_velocity * Time.deltaTime);

    if (_horiz > 0)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    else if (_horiz < 0)
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    float lockomotionValue = _isRun ? 2f : Mathf.Abs(_horiz);
    if (_weaponEquipTwoHandedIK.weaponInHand)
    {
        lockomotionValue = Mathf.Min(lockomotionValue, 0.5f);
    }

    _animator.SetFloat("Lockomotion", lockomotionValue, 0.2f, Time.deltaTime);

    
    }
}
