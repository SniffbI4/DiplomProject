using UnityEngine;

public class SniperRifle : Weapon
{
    [Header("SniperRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] int damageAbatement;
    [SerializeField] float bulletRadius;

    private int damage;

    protected override void Shot(Vector3 target)
    {
        CameraEffects.instance.ShakeCamera(3, 0.1f);

        Ray ray = new Ray(transform.position, target-transform.position);
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(ray, bulletRadius, 100);

        damage = damagePerShot;

        for (int i=0; i<hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Enemy"))
            {
                base.ShowBlood(hits[i].point);
                hits[i].collider.GetComponent<Health>().ApplyDamage(damage);
                damage -= damageAbatement;
            }
            else
            {
                break;
            }
        }
    }

    private void Update()
    {
        CheckAmmo();
    }
}
