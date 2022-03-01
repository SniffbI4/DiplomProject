using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Weapon
{
    [Header ("Minigum settings")]
    [SerializeField] int damagePerShot;
    [SerializeField] float dispersionDistance;
    [SerializeField] float dispersion;

    private Vector3 direction;

    public override void Shot()
    {
        base.Shot();

        direction = transform.forward * dispersionDistance;
        direction.x += Random.Range(-dispersion / 2, dispersion / 2);
        direction.y += Random.Range(-dispersion / 2, dispersion / 2);
        

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 40))
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

        Debug.DrawRay(transform.position, direction, Color.red);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.yellow;
        color.a = 0.6f;
        Gizmos.color = color;

        Gizmos.DrawSphere(transform.position + direction, dispersion);
    }
}


