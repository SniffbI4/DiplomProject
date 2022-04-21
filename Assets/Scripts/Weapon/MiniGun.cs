using UnityEngine;

public class MiniGun : Weapon
{
    [Header ("Minigum settings")]
    [SerializeField] int damagePerShot;
    [SerializeField] float dispersionDistance;
    [SerializeField] float dispersion;

    private Vector3 direction;

    protected override void Shot(Vector3 target)
    {
        CameraEffects.instance.ShakeCamera(1, 0.2f);

        direction = target;
        direction.x += Random.Range(-dispersion / 2, dispersion / 2);
        direction.y += Random.Range(-dispersion / 2, dispersion / 2);
        
        Ray ray = new Ray(transform.position, target-transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 40))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                base.ShowBlood(hit.point);
                hit.collider.GetComponent<Health>().ApplyDamage(damagePerShot);
            }
        }
    }

    private void Update()
    {
        CheckAmmo();
    }
}


