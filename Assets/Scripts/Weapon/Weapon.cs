using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private GameObject weaponModel;

    [SerializeField] public string weaponName;

    [Header ("AMMO")]
    [SerializeField] int maxAmmoCount;
    [SerializeField] int maxAmmoInClip;

    public int currentAmmo { get; private set; }
    public int currentAmmoInClip { get; private set; }

    [Header ("TIME")]
    [SerializeField] float timeToReload;
    [SerializeField] float timeBetweenShots;

    [Header ("SOUNDS")]
    [SerializeField] private AudioClip audioShot;
    [SerializeField] private AudioClip audioReload;

    [SerializeField] private UnityEvent OnShot;

    #region Делегаты и события
    public delegate void WeaponInfoChange(Weapon w);
    public static WeaponInfoChange WeaponInfoChanged;
    public static WeaponInfoChange WeaponReloadStart;
    public static WeaponInfoChange WeaponReloadStop;
    #endregion

    private AudioSource audio;
    private bool isReadyToShoot = false;
    private bool isOnReload;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        currentAmmoInClip = maxAmmoInClip;
        currentAmmo = maxAmmoInClip * 3;
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
        if (isReadyToShoot && currentAmmoInClip > 0)
        {
            isReadyToShoot = false;
            currentAmmoInClip--;

            WeaponInfoChanged?.Invoke(this);

            Shot();
            StartCoroutine(ShotCuro(timeBetweenShots));
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

        isOnReload = true;
        StopAllCoroutines();
        WeaponReloadStart(this);
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

        WeaponInfoChanged?.Invoke(this);
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

        WeaponInfoChanged?.Invoke(this);
    }

    public void PutAwayWeapon ()
    {
        StopAllCoroutines();
        WeaponReloadStop(this);
        audio.Stop();
        weaponModel.SetActive(false);
    }

    public void AddAmmo ()
    {
        currentAmmo += maxAmmoInClip;
        if (currentAmmo > maxAmmoCount)
            currentAmmo = maxAmmoCount;

        WeaponInfoChanged?.Invoke(this);
    }
}
