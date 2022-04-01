using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortiraBullet : MonoBehaviour
{
    [SerializeField] private int maxDamagePerShot;
    [SerializeField] private float damageRadius;

    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private LayerMask layer;

    private AudioSource audio;
    private bool isMove = false;
    private Vector3 explosionPosition;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void GoToTarget(Vector3 target)
    {
        isMove = true;
        explosionPosition = target;
    }

    private void Update()
    {
        if (isMove)
        {
            if (Vector3.Distance(transform.position, explosionPosition) < 0.5f)
            {
                PlayExplosion();
                Destroy(gameObject, 3f);
            }
            transform.position = Vector3.Lerp(transform.position, explosionPosition, Time.deltaTime * speed);
        }
    }

    private void PlayExplosion ()
    {
        CameraEffects.instance.ShakeCamera(4, 1);

        isMove = false;
        explosion.Play();
        audio.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, layer);
        foreach (var hitcol in colliders)
        {
            float distance = Vector3.Distance(hitcol.transform.position, explosionPosition);
            float damageValue = Mathf.Pow(((damageRadius - distance)/damageRadius), 2) * maxDamagePerShot;
            hitcol.GetComponent<Health>().ApplyDamage(Mathf.RoundToInt(damageValue));
        }
    }
}

