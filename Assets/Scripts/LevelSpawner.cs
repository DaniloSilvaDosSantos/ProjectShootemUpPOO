using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private Queue<EnemieSpawnInfo> enemyQueue;
    private Coroutine spawnRoutine;

    void Start()
    {
        enemyQueue = new Queue<EnemieSpawnInfo>(levelData.enemiesToSpawn);
        StartNextSpawn();
    }

    private void StartNextSpawn()
    {
        if (enemyQueue.Count > 0)
        {
            EnemieSpawnInfo nextEnemy = enemyQueue.Dequeue();
            spawnRoutine = StartCoroutine(SpawnEnemy(nextEnemy));
        }
    }

    private IEnumerator SpawnEnemy(EnemieSpawnInfo enemyInfo)
    {
        yield return new WaitForSeconds(enemyInfo.spawnTime);
        Instantiate(enemyInfo.enemyPrefab, enemyInfo.spawnPosition, Quaternion.identity);
        StartNextSpawn();
    }
}
