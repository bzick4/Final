using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeathBar : MonoBehaviour
{
    [SerializeField] private Transform _Enemy;
    [SerializeField] private Vector3 _Offset = new Vector3(0, 1, 0); 

    void Update()
    {
        if (_Enemy!= null)
        {
            transform.position = _Enemy.position + _Offset;
        }
    }
}
