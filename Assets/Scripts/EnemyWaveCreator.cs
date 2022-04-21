using System.Collections;
using UnityEngine;

public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField] int enemyCountInWave;

    public int WaveLevel = 0;
    private int partOfWave=0;

    private void Start()
    {
        InvokeRepeating("StartNewWave", 3f, 60f);
    }

    public void StartNewWave()
    {
        WaveLevel++;
        EnemySpawner.instance.RefreshPool();
        enemyCountInWave += partOfWave;
        partOfWave = Mathf.Abs(enemyCountInWave/5);

        Debug.Log($"Wave: {WaveLevel}" +
            $"\nPartOfWave: {partOfWave}" +
            $"\nEnemyCount: {enemyCountInWave}");

        for (int i=0; i<partOfWave; i++)
        {
            EnemySpawner.instance.SpawnEnemy();
        }
        StartCoroutine(SpawnCuro());
    }

    private IEnumerator SpawnCuro ()
    {
        for (int i = partOfWave; i < enemyCountInWave*WaveLevel; i++)
        {
            yield return new WaitForSeconds(2f);
            EnemySpawner.instance.SpawnEnemy();
        }
    }
}
