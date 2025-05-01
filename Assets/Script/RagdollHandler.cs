using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private List<Collider> _ragdollColliders;

    public void Instalize()
    {
        _ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        foreach (Collider col in _ragdollColliders)
        {
            col.isTrigger = false;
            col.enabled = true;
        }
    }
    public void DisableRagdoll()
    {
       foreach (Collider col in _ragdollColliders)
        {
            col.enabled = false;
            col.isTrigger = true;
        }
    }
}
