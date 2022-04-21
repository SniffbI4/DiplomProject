using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyPrefabs
{
    public GameObject prefab;
    public int count;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    private Transform playerTransform;

    [SerializeField , Header("Enemies")]
    List<EnemyPrefabs> enemies;
    [SerializeField] private List<GameObject> enemiesOnScene;

    [Header ("Spawn Zone")]
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;

    private Vector3 enemyPosition;
    private Vector3 enemySize = new Vector3(2f, 2f, 2f);

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

    public void SetPlayerPosition (Transform playerTransform)
    {
        this.playerTransform = playerTransform;
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

    public void RefreshPool()
    {
        foreach (GameObject go in enemiesOnScene)
        {
            if (go.activeSelf && !go.GetComponent<Health>().IsAlive)
            {
                go.GetComponent<Mutant>().Refresh();
                go.SetActive(false);
            }
        }
    }

    public void SpawnEnemy()
    {
        int randEnemyIndex = Random.Range(0, enemiesOnScene.Count);
        while (enemiesOnScene[randEnemyIndex].activeSelf)
            randEnemyIndex = Random.Range(0, enemiesOnScene.Count);
       
        do
        {
            enemyPosition = GetNewEnemyPosition();
        }
        while (!CanEnemySpawnHere());

        enemiesOnScene[randEnemyIndex].transform.position = enemyPosition;
        enemiesOnScene[randEnemyIndex].SetActive(true);
    }

    private Vector3 GetNewEnemyPosition ()
    {
        #region Old
        //float x = Random.Range(-maxRadius, maxRadius);
        //float rad = Random.Range(minRadius, maxRadius);

        //float z = Mathf.Sqrt(Mathf.Pow(rad, 2) - Mathf.Pow(x, 2));

        //enemyPosition = new Vector3(playerTransform.position.x + x, playerTransform.position.y, playerTransform.position.z + z);
        #endregion

        #region New
        float x = Random.Range(-maxRadius, maxRadius);
        float z = Random.Range(-maxRadius, maxRadius);
        if (x > -minRadius && x < minRadius && z > -minRadius && z < minRadius)
            return Vector3.zero;

       return new Vector3(playerTransform.position.x + x, playerTransform.position.y, playerTransform.position.z + z);
        #endregion

    }

    private void GetNewEnemyPosition (GameObject enemy)
    {
        #region NavMesh
        bool get_correct_point = false;
        while (!get_correct_point)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * maxRadius + playerTransform.position, out hit, maxRadius, NavMesh.AllAreas);

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(hit.position, path);

            if (path.status == NavMeshPathStatus.PathComplete) get_correct_point = true;
        }
        #endregion
    }

    private bool CanEnemySpawnHere ()
    {
        if (enemyPosition == Vector3.zero)
            return false;

        Ray ray = new Ray(enemyPosition, enemyPosition - new Vector3(0, 1, 0));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 3))
            return false;

        Collider[] colliders = Physics.OverlapBox(enemyPosition, enemySize);

        if (colliders.Length > 1)
            return false;

        return true;
    }

    public Transform GetPlayerPosition ()
    {
        return playerTransform;
    }

    //private void OnDrawGizmos()
    //{
    //    Color color = Color.yellow;
    //    color.a = 0.1f;
    //    Gizmos.color = color;
    //    Gizmos.DrawSphere(playerTransform.position, minRadius);
    //    Gizmos.color = color;
    //    Gizmos.DrawSphere(playerTransform.position, maxRadius);
    //}
}
