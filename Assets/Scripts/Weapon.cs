using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    [SerializeField] int maxAmmoCount;
    [SerializeField] int maxAmmoInClip;

    [SerializeField] int currentAmmo;
    [SerializeField] int currentAmmoInClip;

    [SerializeField] float timeToReload;
    [SerializeField] float timeBetweenShots;

    private AudioSource audio;
    [SerializeField] private AudioClip audioShot;
    [SerializeField] private AudioClip audioReload;

    private bool isReadyToShoot = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        if (currentAmmo > 0 && currentAmmoInClip > 0)
            isReadyToShoot = true;

        Debug.Log($"{currentAmmoInClip}/{currentAmmo}");
    }

    private void Update()
    {
        if (currentAmmoInClip<=0 && currentAmmo>0)
        {
            Reload();
        }
        if (currentAmmo<=0 && currentAmmoInClip<=0)
        {
            StopAllCoroutines();
            isReadyToShoot = false;
        }
    }

    public void Shot ()
    {
        if (isReadyToShoot)
        {
            isReadyToShoot = false;
            audio.PlayOneShot(audioShot);
            currentAmmoInClip--;
            MoveBullet();
            Debug.Log($"{currentAmmoInClip}/{currentAmmo}");
            StartCoroutine(TimerCuro(timeBetweenShots));
        }
    }

    private void MoveBullet ()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward*bulletSpeed, ForceMode.Impulse);
    }

    public void Reload ()
    {
        StopAllCoroutines();
        isReadyToShoot = false;
        audio.PlayOneShot(audioReload);
        StartCoroutine(TimerCuro(timeToReload));

        if (currentAmmo>=(maxAmmoInClip-currentAmmoInClip))
        {
            currentAmmo -= (maxAmmoInClip-currentAmmoInClip);
            currentAmmoInClip = maxAmmoInClip;
        }
        else
        {
            currentAmmoInClip = currentAmmo;
            currentAmmo = 0;
        }

        Debug.Log($"{currentAmmoInClip}/{currentAmmo}");

    }

    private IEnumerator TimerCuro (float time)
    {
        yield return new WaitForSeconds(time);
        isReadyToShoot = true;
    }
}
