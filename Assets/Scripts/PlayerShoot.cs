using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ActorView))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon mainWeapon=default;
    private Weapon specialWeapon=default;

    public Weapon SpecialWeapon {
        get
        {
            return this.specialWeapon;
        }
        set
        {
            this.specialWeapon = value;
        }
    }

    private Weapon currentWeapon;

    private void Start()
    {
        currentWeapon = mainWeapon;
        currentWeapon.TakeWeapon();
        
        // TO DO
        // TakeWeapon вызывается 
        // перед стартом классов Weapon
        // поэтому в старте в UI отображается 0/0 патрон
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
        currentWeapon.Reload();
    }

    public void AddAmmoToCurrentWeapon ()
    {
        currentWeapon.AddAmmo();
    }
}
