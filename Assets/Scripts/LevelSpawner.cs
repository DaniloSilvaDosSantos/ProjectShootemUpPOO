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
    // Referencia para o singleton game controller
    private GameController gameController;
    private HUDController hudController;

    void Start()
    {
        //Pegando a referencia para o GameController e HUDController
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        hudController = GameObject.Find("CanvasHUD").GetComponent<HUDController>();

        // Armazenando o scriptable object contendo as informações do level atual
        levelData = gameController.GetCurrentLevel();

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
        else
        {
            InvokeRepeating("EnemyHasBeenFound", 1f, 1f); 
        }
    }

    // Verifica se ainda existem inimigos
    private void EnemyHasBeenFound()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            Debug.Log("Ainda tem inimigos");
        }
        else // Se não houver inimigos então será chamado a função do Game Controller para carregar o proximo nivel
        {
            Debug.Log("Vitoria!");
            CancelInvoke("EnemyHasBeenFound");

            StartCoroutine(GameControllerGoToNextLevel());
        }
    }

    // Rotina para spawn de inimigos com tempo de espera
    private IEnumerator SpawnEnemy(EnemieSpawnInfo enemyInfo)
    {
        yield return new WaitForSeconds(enemyInfo.spawnTime); // Espera o tempo necessário antes de spawnar
        Instantiate(enemyInfo.enemyPrefab, enemyInfo.spawnPosition, Quaternion.identity); // Instancia o inimigo na posição correta
        StartNextSpawn(); // Chama o próximo inimigo na fila
    }

    // Rotina para chamar a função do Game Controller que faz carregar o proximo nivel
    private IEnumerator GameControllerGoToNextLevel()
    {
        hudController.ActivateWinLevelHUD();
        yield return new WaitForSeconds(3f);
        gameController.GoToNextLevel();
    }
}
