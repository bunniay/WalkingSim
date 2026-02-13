using UnityEngine;

public class TriggerActivator3D : MonoBehaviour
{
    [SerializeField] private Collider barrierCollider;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            barrierCollider.isTrigger = false;
            activated = true;
        }
    }
}