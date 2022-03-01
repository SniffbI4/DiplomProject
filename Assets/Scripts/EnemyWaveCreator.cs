using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Timer))]
public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField] int enemyCountInFirstWave;

    static int WaveLevel = 1;
    private Timer timer;

    private void Start()
    {
        timer = GetComponent<Timer>();
        StartNewWave();
    }

    private void StartNewWave()
    {
        StartCoroutine(SpawnCuro());
    }

    private IEnumerator SpawnCuro ()
    {
        for (int i = 0; i < enemyCountInFirstWave; i++)
        {
            yield return new WaitForSeconds(2f);
            EnemySpawner.instance.SpawnEnemy();
        }
    }
}
