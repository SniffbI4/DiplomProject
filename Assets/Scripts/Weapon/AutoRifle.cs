using UnityEngine;

public class AutoRifle : Weapon
{
    [Header ("AmmoRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] float bulletRadius;

    public override void Shot()
    {
        base.Shot();
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, bulletRadius, out hit, 30))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                base.ShowBlood(hit.point);
                hit.collider.GetComponent<Health>().ApplyDamage(damagePerShot);
            }

            if (hit.collider.CompareTag("Transformer"))
            {
                //Debug.Log("œŒœ¿ƒ¿Õ»≈ ¬ “–¿Õ—‘Œ–Ã¿“Œ–");
                hit.collider.GetComponent<Transformer>().Break();
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
