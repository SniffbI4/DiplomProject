using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image weaponSprite;
    [SerializeField] private Image deactivatedWeaponMask;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text ammoText;

    private Weapon weapon;
    private float currentTime;

    public void OnEnable ()
    {
        Weapon.WeaponInfoChanged += UpdateWeaponUI;
        Weapon.WeaponReloadStart += StartReload;
        Weapon.WeaponReloadStop += StopReload;
    }

    private void OnDisable()
    {
        Weapon.WeaponInfoChanged -= UpdateWeaponUI;
        Weapon.WeaponReloadStart -= StartReload;
        Weapon.WeaponReloadStop -= StopReload;
    }

    private void Start()
    {
        Color temp = deactivatedWeaponMask.color;
        temp.a = 0;
        deactivatedWeaponMask.color = temp;
    }

    private void UpdateWeaponUI (Weapon w)
    {
        CheckWeapon(w);
        
        ammoText.text = $"{weapon.currentAmmoInClip} / {weapon.currentAmmo}";

        float progressBarValue = (float)weapon.currentAmmoInClip / (float)weapon.maxAmmoInClip;
        progressBar.fillAmount = progressBarValue;
    }

    private void StartReload (Weapon w)
    {
        CheckWeapon(w);
        currentTime = Time.time;
        Color temp = deactivatedWeaponMask.color;
        temp.a = 1;
        deactivatedWeaponMask.color = temp;
    }

    private void StopReload (Weapon w)
    {
        CheckWeapon(w);
        Color temp = deactivatedWeaponMask.color;
        temp.a = 0;
        deactivatedWeaponMask.color = temp;
        UpdateWeaponUI(weapon);
    }

    private void CheckWeapon (Weapon w)
    {
        if (weapon != w)
        {
            weapon = w;
            weaponSprite.sprite = weapon.weaponUiSprite;
            deactivatedWeaponMask.sprite = weapon.weaponUiSprite;
        }
    }

    private void Update()
    {
        if (weapon == null)
            return;

        if (weapon.IsOnReload)
        {
            float progressBarValue = (Time.time - currentTime) / weapon.timeToReload;
            if (progressBarValue <= 1)
                progressBar.fillAmount = progressBarValue;
        }
    }
}
