using UnityEngine;

public class SniperRifle : Weapon
{
    [Header("SniperRifleSettings")]
    [SerializeField] int damagePerShot;
    [SerializeField] int damageAbatement;

    private int damage;

    public override void Shot()
    {
        base.Shot();
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits;

        hits = Physics.RaycastAll(ray, 50);

        damage = damagePerShot;

        for (int i=0; i<hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Enemy"))
            {
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
        base.CheckAmmo();

        Vector3 direction = transform.forward * 50;
        Debug.DrawRay(transform.position, direction, Color.red);
    }

}
