using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Item
{
    MedicBack,
    AmmoBack,
}

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private Item itemType;
    private void OnTriggerEnter(Collider other)
    {
        switch (itemType) {
            case Item.AmmoBack:
                other.GetComponent<PlayerShoot>().AddAmmoToCurrentWeapon();
                break;
            case Item.MedicBack:
                Health health = other.GetComponent<Health>();
                int recoveryHitPoints = health.MaxHealth/5;
                health.RecoveryHealth(recoveryHitPoints);
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}
