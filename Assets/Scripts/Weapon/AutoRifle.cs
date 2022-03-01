using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Weapon
{
    [Header ("AmmoRifleSettings")]
    [SerializeField] int damagePerShot;

    public override void Shot()
    {
        base.Shot();
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Health>().ApplyDamage(damagePerShot);
            }
        }
    }

    private void Update()
    {
        CheckAmmo();

        //Vector3 direction = transform.forward * 30;
        //Debug.DrawRay(transform.position, direction, Color.red);
    }
}
