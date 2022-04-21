using UnityEngine;

public class AutoRifle : Weapon
{
    [Header ("AmmoRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] float bulletRadius;

    protected override void Shot(Vector3 target)
    {
        CameraEffects.instance.ShakeCamera(0.5f, 0.1f);

        Ray weaponRay = new Ray(transform.position, target-transform.position);

        RaycastHit weaponHit;

        if (Physics.SphereCast(weaponRay, bulletRadius, out weaponHit, float.MaxValue))
        {
            if (weaponHit.collider.CompareTag("Enemy"))
            {
                ShowBlood(weaponHit.point);
                weaponHit.collider.GetComponent<Health>().ApplyDamage(damagePerShot);
            }

            if (weaponHit.collider.CompareTag("Transformer"))
            {
                weaponHit.collider.GetComponent<Transformer>().Break();
            }
        }
    }

    private void Update()
    {
        CheckAmmo();
    }
}
