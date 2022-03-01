using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAdder : MonoBehaviour
{
    [SerializeField] int recoveryHitPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Health>().RecoveryHealth(recoveryHitPoints);
        Destroy(gameObject);
    }
}
