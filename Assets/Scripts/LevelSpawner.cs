using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    // Dados do nível atual
    [SerializeField] private LevelData levelData;
    // Fila de inimigos a serem gerados
    private Queue<EnemieSpawnInfo> enemyQueue;
    // Referência para a rotina de spawn
    private Coroutine spawnRoutine;

    void Start()
    {
        enemyQueue = new Queue<EnemieSpawnInfo>(levelData.enemiesToSpawn); // Inicializa a fila de inimigos
        StartNextSpawn();  // Começa a gerar os inimigos
    }

    // Inicia o spawn do próximo inimigo na fila
    private void StartNextSpawn()
    {
        if (enemyQueue.Count > 0)
        {
            EnemieSpawnInfo nextEnemy = enemyQueue.Dequeue(); // Retira o próximo inimigo da fila
            spawnRoutine = StartCoroutine(SpawnEnemy(nextEnemy)); // Inicia a rotina de spawn
        }
    }

    // Rotina para spawn de inimigos com tempo de espera
    private IEnumerator SpawnEnemy(EnemieSpawnInfo enemyInfo)
    {
        yield return new WaitForSeconds(enemyInfo.spawnTime); // Espera o tempo necessário antes de spawnar
        Instantiate(enemyInfo.enemyPrefab, enemyInfo.spawnPosition, Quaternion.identity); // Instancia o inimigo na posição correta
        StartNextSpawn(); // Chama o próximo inimigo na fila
    }
}
