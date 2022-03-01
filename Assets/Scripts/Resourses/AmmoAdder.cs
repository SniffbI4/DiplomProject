using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAdder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerShoot>().AddAmmoToCurrentWeapon();
        Destroy(gameObject);
    }
}
