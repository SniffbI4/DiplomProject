using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Timer))]
public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField] int enemyCountInWave;

    public int WaveLevel = 0;
    private Timer timer;
    private int partOfWave=0;

    private void Start()
    {
        //timer = GetComponent<Timer>();
        Invoke("StartNewWave", 2f);
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
