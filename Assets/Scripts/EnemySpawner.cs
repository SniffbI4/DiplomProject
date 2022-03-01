using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefabs
{
    public GameObject prefab;
    public int count;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    [SerializeField] private Transform playerTransform;

    [SerializeField , Header("Enemies")]
    List<EnemyPrefabs> enemies;
    [SerializeField] private List<GameObject> enemiesOnScene;

    [Header ("Spawn Zone")]
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;

    private Vector3 enemyPosition;
    private Vector3 enemySize = new Vector3(1f, 2f, 1f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            InicializeEnemies();
            return;
        }

        Destroy(this.gameObject);

    }

    private void InicializeEnemies ()
    {
        enemiesOnScene = new List<GameObject>();

        GameObject EnemiesParent = new GameObject("EnemiesParent");
        EnemiesParent.transform.SetParent(gameObject.transform);

        for (int i=0; i<enemies.Count; i++)
        {
            for (int j=0; j<enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(enemies[i].prefab, EnemiesParent.transform);
                enemiesOnScene.Add(enemyObject);
                enemyObject.SetActive(false);
            }
        }
    }

    public void SpawnEnemy()
    {
        //Debug.Log($"SpawnEnemy. EnemyCount = {enemiesOnScene.Count}");
        int randEnemyIndex = Random.Range(0, enemiesOnScene.Count);
        while (enemiesOnScene[randEnemyIndex].activeSelf)
            randEnemyIndex = Random.Range(0, enemiesOnScene.Count);
        GetNewEnemyPosition();
        while (!CanEnemySpawnHere())
        {
            GetNewEnemyPosition();
        }
        enemiesOnScene[randEnemyIndex].transform.position = enemyPosition;
        enemiesOnScene[randEnemyIndex].SetActive(true);
        //enemiesOnScene.Remove(enemiesOnScene[randEnemyIndex]);
    }

    private void GetNewEnemyPosition ()
    {
        float x = Random.Range(-maxRadius, maxRadius);
        float rad = Random.Range(minRadius, maxRadius);
            
        float z = Mathf.Sqrt(Mathf.Pow(rad, 2) - Mathf.Pow(x, 2));

        enemyPosition = new Vector3(playerTransform.position.x + x, playerTransform.position.y, playerTransform.position.z + z);
    }

    private bool CanEnemySpawnHere ()
    {
        Collider[] colliders = Physics.OverlapBox(enemyPosition, enemySize);

        Ray ray = new Ray(enemyPosition, enemyPosition - new Vector3(0, 1, 0));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100) || colliders.Length > 1)
            return false;
        else
            return true;
    }

    public Transform GetPlayerPosition ()
    {
        return playerTransform;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.yellow;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawSphere(playerTransform.position, minRadius);
        Gizmos.color = color;
        Gizmos.DrawSphere(playerTransform.position, maxRadius);
    }
}
