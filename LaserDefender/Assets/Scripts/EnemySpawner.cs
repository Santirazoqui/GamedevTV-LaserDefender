using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfig currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWave());
    }

    IEnumerator SpawnEnemyWave()
    {
        do
        {
            foreach(WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                int enemyCount = this.currentWave.GetEnemyCount();
                for(int i = 0; i < enemyCount; i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), 
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.Euler(0,0,180),
                                transform
                                );
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(this.timeBetweenWaves);
            }
        } while(this.isLooping);
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }
}
