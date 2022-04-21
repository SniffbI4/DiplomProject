using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem ammoTracersParticle;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private GameObject weaponModel;

    [SerializeField] public string weaponName;

    [Header ("AMMO")]
    [SerializeField] int maxAmmoCount;
    [SerializeField] public int maxAmmoInClip;

    public int currentAmmo { get; private set; }
    public int currentAmmoInClip { get; private set; }

    [Header ("TIME")]
    [SerializeField] public float timeToReload;
    [SerializeField] float timeBetweenShots;

    [Header ("SOUNDS")]
    [SerializeField] private AudioClip audioShot;
    [SerializeField] private AudioClip audioReload;

    [Header("UI")]
    public Sprite weaponUiSprite;

    [SerializeField] private UnityEvent OnShot;

    private AudioSource audio;
    private bool isReadyToShoot = false;
    public bool IsOnReload { get; private set; }

    public static Action<Weapon> WeaponInfoChanged;
    public static Action<Weapon> WeaponReloadStart;
    public static Action<Weapon> WeaponReloadStop;

    protected abstract void Shot(Vector3 target);

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

    public void Fire (Vector2 aimPosition)
    {
        Vector3 target = new Vector3(aimPosition.x, transform.position.y/2, aimPosition.y);

        if (ammoTracersParticle) 
            ammoTracersParticle.transform.LookAt(target, Vector3.up);

        if (isReadyToShoot && currentAmmoInClip > 0)
        {
            isReadyToShoot = false;
            currentAmmoInClip--;

            WeaponInfoChanged?.Invoke(this);

            audio.PlayOneShot(audioShot);
            OnShot.Invoke();

            Shot(target);
            StartCoroutine(ShotCuro(timeBetweenShots));
        }
    }

    public void Reload ()
    {
        if (currentAmmoInClip == maxAmmoInClip || IsOnReload)
            return;

        IsOnReload = true;
        StopAllCoroutines();
        WeaponReloadStart?.Invoke(this);
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
        IsOnReload = false;
        WeaponReloadStop?.Invoke(this);
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
            IsOnReload = false;
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
