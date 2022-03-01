using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseSpawner : MonoBehaviour
{
    public static ResourseSpawner instance = null;

    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject hpPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    public void SpawnResourse (Vector3 position)
    {
        float x = Random.value;
        if (x > 0.4f)
            return;
        if (x > 0.15f)
            SpawnAmmo(position);
        else
            SpawnHP(position);
    }

    private void SpawnAmmo (Vector3 position)
    {
        GameObject ammo = Instantiate(ammoPrefab, position, Quaternion.identity);
    }

    private void SpawnHP (Vector3 position)
    {
        GameObject hp = Instantiate(hpPrefab, position, Quaternion.identity);
    }
}
