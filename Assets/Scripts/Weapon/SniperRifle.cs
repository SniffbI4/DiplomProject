using UnityEngine;

public class SniperRifle : Weapon
{
    [Header("SniperRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] int damageAbatement;
    [SerializeField] float bulletRadius;

    private int damage;

    public override void Shot()
    {
        base.Shot();

        Debug.Log("ONE SHOT");
        CameraEffects.instance.ShakeCamera(3, 0.1f);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits;

        //hits = Physics.RaycastAll(ray, 50);
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
