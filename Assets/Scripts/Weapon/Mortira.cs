using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortira : Weapon
{
    [SerializeField] private GameObject mortiraBullet;

    private Vector3 explosionPosition;

    public override void Shot()
    {
        base.Shot();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            explosionPosition = hit.point;
            explosionPosition.y = 0.5f;
            GameObject bullet = Instantiate(mortiraBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<MortiraBullet>().GoToTarget(explosionPosition);
        }
    }

    private void Update()
    {
        base.CheckAmmo();
    }
}
