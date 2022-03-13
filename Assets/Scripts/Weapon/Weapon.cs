using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private GameObject weaponModel;

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

    [SerializeField] private UnityEvent OnShot;

    #region Делегаты и события
    public delegate void WeaponChanged();
    public delegate void WeaponReloaded(float timeToReload);
    public delegate void WeaponAmmoChanged(int currentAmmoInClip, int currentAmmo);

    public static event WeaponAmmoChanged OnWeaponFire;
    public static event WeaponReloaded OnWeaponReloadStart;
    public static event WeaponAmmoChanged OnWeaponReloadEnd;
    public static event WeaponAmmoChanged OnWeaponActivated;
    public static event WeaponChanged OnWeaponDeactivated;
    public static event WeaponAmmoChanged OnWeaponAmmoChanged;
    #endregion

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
            if (currentAmmoInClip > 0)
            {
                isReadyToShoot = false;
                currentAmmoInClip--;

                //OnWeaponFire(currentAmmoInClip, currentAmmo);
                if (OnWeaponAmmoChanged!=null)
                    OnWeaponAmmoChanged(currentAmmoInClip, currentAmmo);

                Shot();
                StartCoroutine(ShotCuro(timeBetweenShots));
            }
        }
    }

    public virtual void Shot()
    {
        audio.PlayOneShot(audioShot);
        OnShot.Invoke();
    }

    public void Reload ()
    {
        if (currentAmmoInClip == maxAmmoInClip || isOnReload)
            return;

        if (OnWeaponReloadStart!=null)
            OnWeaponReloadStart(timeToReload);

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

        //OnWeaponReloadEnd(currentAmmoInClip, currentAmmo);
        if (OnWeaponAmmoChanged!=null)
            OnWeaponAmmoChanged(currentAmmoInClip, currentAmmo);
    }

    protected void ShowBlood (Vector3 position)
    {
        bloodParticle.transform.position = position;
        bloodParticle.Play();
    }

    public void TakeWeapon ()
    {
        if (currentAmmo > 0)
        {
            isReadyToShoot = true;
            isOnReload = false;
        }

        weaponModel.SetActive(true);

        if (OnWeaponActivated != null)
            OnWeaponActivated(currentAmmoInClip, currentAmmo);
    }

    public void PutAwayWeapon ()
    {
        StopAllCoroutines();
        audio.Stop();
        weaponModel.SetActive(false);

        if (OnWeaponDeactivated!=null)
            OnWeaponDeactivated();
    }

    public void AddAmmo ()
    {
        currentAmmo += maxAmmoInClip;
        if (currentAmmo > maxAmmoCount)
            currentAmmo = maxAmmoCount;

        if (OnWeaponAmmoChanged!=null)
            OnWeaponAmmoChanged(currentAmmoInClip, currentAmmo);
    }
}
