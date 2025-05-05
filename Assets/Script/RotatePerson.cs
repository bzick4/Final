using UnityEngine;

public class RotatePerson : MonoBehaviour
{
    [SerializeField] private GameObject _Level;
    [SerializeField] private float _Angle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _Level.transform.rotation = Quaternion.Euler(0, _Angle, 0);
            
        }
    }
    
}

