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
    }

    public void ChangeWeapon ()
    {
        if (currentWeapon == mainWeapon)
        {
            currentWeapon = specialWeapon;
        }
        else if (currentWeapon == specialWeapon)
        {
            currentWeapon = mainWeapon;
        }
    }

    public void Shot ()
    { 
        currentWeapon.Shot();
    }

    public void ReloadWeapon ()
    {
        currentWeapon.Reload();
    }
}
