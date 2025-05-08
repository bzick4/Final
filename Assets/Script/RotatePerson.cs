using System.Collections;
using UnityEngine;

public class RotatePerson : MonoBehaviour
{
    [SerializeField] private GameObject _Level;
    [SerializeField] private float _Angle;

    private Controller _controller => FindObjectOfType<Controller>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RotateLevel());
            
        }
    }

    private IEnumerator RotateLevel()
    {
        _controller.enabled = false; 
        Quaternion initialRotation = _Level.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, _Angle, 0);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            
            _Level.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / 1f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _Level.transform.rotation = targetRotation;
        _controller.enabled = true;
    }
    
}

