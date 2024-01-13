using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfig currentWave;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int enemyCount = this.currentWave.GetEnemyCount();
        for(int i = 0; i < enemyCount; i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(i), 
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform
                        );
        }
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }
}
