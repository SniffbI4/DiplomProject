using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWeaponChanger : MonoBehaviour
{
    [SerializeField]
    private Text mainWeaponName;
    [SerializeField]
    private GameObject mainWeapon;

    [SerializeField]
    private Text advWeaponName;
    [SerializeField]
    private Weapon[] advancedWeapons;

    private int currentAdvancedWeapon;

    public void ShowMainWeapon ()
    {
        advancedWeapons[currentAdvancedWeapon].gameObject.SetActive(false);
        mainWeapon.SetActive(true);
    }

    public void ChangeAdvancedWeapon (int nextOrPrev)
    {
        if (mainWeapon.activeSelf)
            mainWeapon.SetActive(false);

        advancedWeapons[currentAdvancedWeapon].gameObject.SetActive(false);
        currentAdvancedWeapon += nextOrPrev;

        if (currentAdvancedWeapon == advancedWeapons.Length)
            currentAdvancedWeapon = 0;
        else if (currentAdvancedWeapon < 0)
            currentAdvancedWeapon = advancedWeapons.Length - 1;

        advancedWeapons[currentAdvancedWeapon].gameObject.SetActive(true);
        advWeaponName.text = advancedWeapons[currentAdvancedWeapon].weaponName;
    }
}
