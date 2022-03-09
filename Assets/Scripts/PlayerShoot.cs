using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ActorView))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon mainWeapon;
    [SerializeField] private Weapon specialWeapon;

    private Weapon currentWeapon;
    private ActorView actorView;

    private void Start()
    {
        actorView = GetComponent<ActorView>();

        currentWeapon = mainWeapon;
        currentWeapon.TakeWeapon();
    }

    public void ChangeWeapon ()
    {
        currentWeapon.PutAwayWeapon();
        if (currentWeapon == mainWeapon)
        {
            currentWeapon = specialWeapon;
        }
        else if (currentWeapon == specialWeapon)
        {
            currentWeapon = mainWeapon;
        }
        currentWeapon.TakeWeapon();
    }

    public void Fire ()
    { 
        currentWeapon.Fire();
    }

    public void ReloadWeapon ()
    {
        //actorView.PlayReloadStart();
        currentWeapon.Reload();
    }

    public void AddAmmoToCurrentWeapon ()
    {
        currentWeapon.AddAmmo();
    }
}
