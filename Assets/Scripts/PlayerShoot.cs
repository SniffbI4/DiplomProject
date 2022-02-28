using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon mainWeapon;
    [SerializeField] private Weapon specialWeapon;

    private Weapon currentWeapon;

    private void Start()
    {
        currentWeapon = mainWeapon;
        currentWeapon.TakeWeapon();
    }

    public void ChangeWeapon ()
    {
        currentWeapon.PutAwayWeapon();
        if (currentWeapon == mainWeapon)
        {
            Debug.Log("Special weapon choosed");
            currentWeapon = specialWeapon;
        }
        else if (currentWeapon == specialWeapon)
        {
            Debug.Log("Main weapon choosed");
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
        currentWeapon.Reload();
    }
}
