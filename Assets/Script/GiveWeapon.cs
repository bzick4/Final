using System.Collections;
using UnityEngine;

public class WeaponEquipTwoHandedIK : MonoBehaviour
{
    [SerializeField] private Transform _Weapon;
    [SerializeField] private Transform _BackHolder;
    [SerializeField] private Transform _HandHolder;
    [SerializeField] private Transform _RightHandTarget;
    [SerializeField] private Transform _LeftHandIKTarget;


    private Animator _animator;
    private float ikWeightSpeed = 2f;
    private float timeToDrawWeapon = 1.0f;
    private float timeToHolsterWeapon = 1.0f;

    private bool isBusy = false;
    private bool ikActive = false;

    public bool weaponInHand {get; private set;} = false;
    private float currentIKWeight = 0f;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _Weapon.SetParent(_BackHolder);
        _Weapon.localPosition = Vector3.zero;
        _Weapon.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBusy)
        {
            if (weaponInHand)
            {
                StartCoroutine(HolsterWeaponCoroutine());
            }
            else
            {
                StartCoroutine(DrawWeaponCoroutine());
            }
        }
    }

    private IEnumerator DrawWeaponCoroutine()
    {
        isBusy = true;
        ikActive = true;

        _animator.SetTrigger("Equip");

        yield return new WaitForSeconds(timeToDrawWeapon);
       

        _Weapon.SetParent(_HandHolder);
        _Weapon.localPosition = Vector3.zero;
        _Weapon.localRotation = Quaternion.identity;

        ikActive = false;
        currentIKWeight = 0f;
        weaponInHand = true;
        isBusy = false;
    }

    
    private IEnumerator HolsterWeaponCoroutine()
    {
        isBusy = true;
        ikActive = false;

        _animator.SetTrigger("Unequip");

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);

        yield return new WaitForSeconds(timeToHolsterWeapon);

        _Weapon.SetParent(_BackHolder);
        _Weapon.localPosition = Vector3.zero;
        _Weapon.localRotation = Quaternion.identity;

        weaponInHand = false;
        isBusy = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
        if (ikActive)
        {
            currentIKWeight = Mathf.MoveTowards(currentIKWeight, 1f, Time.deltaTime * ikWeightSpeed);
            
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, currentIKWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, currentIKWeight);

            _animator.SetIKPosition(AvatarIKGoal.RightHand, _RightHandTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _RightHandTarget.rotation);
        }
        else if (weaponInHand && !isBusy)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _LeftHandIKTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _LeftHandIKTarget.rotation);

            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
}