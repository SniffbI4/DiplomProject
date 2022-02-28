using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header ("AMMO")]
    [SerializeField] int maxAmmoCount;
    [SerializeField] int maxAmmoInClip;

    [SerializeField] int currentAmmo;
    [SerializeField] int currentAmmoInClip;

    [Header ("TIME")]
    [SerializeField] float timeToReload;
    [SerializeField] float timeBetweenShots;

    [Header ("SOUNDS")]
    [SerializeField] private AudioClip audioShot;
    [SerializeField] private AudioClip audioReload;

    private AudioSource audio;
    private bool isReadyToShoot = false;
    private bool isOnReload;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    protected void CheckAmmo ()
    {
        if (currentAmmoInClip <= 0 && currentAmmo > 0 && isReadyToShoot)
        {
            Reload();
        }

        if (currentAmmo <= 0 && currentAmmoInClip <= 0)
        {
            StopAllCoroutines();
            isReadyToShoot = false;
        }
    }

    public void Fire ()
    {
        if (isReadyToShoot)
        {
            isReadyToShoot = false;
            currentAmmoInClip--;
            UpdateAmmoUI();

            Shot();
            StartCoroutine(ShotCuro(timeBetweenShots));
        }
    }

    public virtual void Shot()
    {
        audio.PlayOneShot(audioShot);
    }

    public void Reload ()
    {
        if (currentAmmoInClip == maxAmmoInClip || isOnReload)
            return;
        isOnReload = true;
        StopAllCoroutines();
        isReadyToShoot = false;
        audio.PlayOneShot(audioReload);
        StartCoroutine(ReloadCuro(timeToReload));
    }

    private IEnumerator ShotCuro (float time)
    {
        yield return new WaitForSeconds(time);
        isReadyToShoot = true;
    }

    private IEnumerator ReloadCuro (float time)
    {
        yield return new WaitForSeconds(time);
        if (currentAmmo >= (maxAmmoInClip - currentAmmoInClip))
        {
            currentAmmo -= (maxAmmoInClip - currentAmmoInClip);
            currentAmmoInClip = maxAmmoInClip;
        }
        else
        {
            currentAmmoInClip += currentAmmo;
            currentAmmo = 0;
        }
        isReadyToShoot = true;
        isOnReload = false;
        UpdateAmmoUI();
    }

    public void TakeWeapon ()
    {
        if (currentAmmo > 0)
        {
            isReadyToShoot = true;
            isOnReload = false;
        }

        UpdateAmmoUI();
    }

    public void PutAwayWeapon ()
    {
        StopAllCoroutines();
        audio.Stop();   
    }

    public void UpdateAmmoUI ()
    {
        UIManager.instance.ShowAmmo(currentAmmoInClip, currentAmmo);
    }
}
