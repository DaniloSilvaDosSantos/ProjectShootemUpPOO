using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private float elapsedTime = 0f;
    private List<EnemieSpawnInfo> remainingEnemies;

    void Start()
    {
        remainingEnemies = new List<EnemieSpawnInfo>(levelData.enemiesToSpawn);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        for (int i = remainingEnemies.Count - 1; i >= 0; i--)
        {
            if (elapsedTime >= remainingEnemies[i].spawnTime)
            {
                Instantiate(remainingEnemies[i].enemyPrefab, remainingEnemies[i].spawnPosition, Quaternion.identity);
                remainingEnemies.RemoveAt(i);
            }
        }
    }
}
