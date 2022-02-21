using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    [SerializeField] private Transform playerTransform;

    [Header("EnemyPrefabs")]
    List<Enemy> enemyPrefabs;

    [Header ("Spawn Zone")]
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    public Transform GetPlayerPosition ()
    {
        return playerTransform;
    } 
}
