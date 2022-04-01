using UnityEngine;

public class AutoRifle : Weapon
{
    [Header ("AmmoRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] float bulletRadius;

    public override void Shot()
    {
        base.Shot();

        CameraEffects.instance.ShakeCamera(0.5f, 0.1f);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, bulletRadius, out hit, 50))
        {
            Debug.Log($"hit {hit.collider.name}");
            if (hit.collider.CompareTag("Enemy"))
            {
                ShowBlood(hit.point);
                hit.collider.GetComponent<Health>().ApplyDamage(damagePerShot);
            }

            if (hit.collider.CompareTag("Transformer"))
            {
                hit.collider.GetComponent<Transformer>().Break();
            }
        }
    }

    private void Update()
    {
        CheckAmmo();
    }
}
