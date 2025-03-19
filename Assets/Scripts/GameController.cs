using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private int currentLevel;
    [SerializeField] private List<LevelData> levels;
    private MenuController menuController;

    public int CurrentLevel
    {
        get {return currentLevel;}
        set 
        {
            if (value >= 0 && value < levels.Count)
            {
                currentLevel = value;
            }
            else Debug.Log("Valor invalido para nivel");
        }
    }

    private void Awake()
    {
        //Padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o GameController entre cenas
        }
        else
        {
            Destroy(gameObject); // Destroy o GameController caso já exista um
        }
    }

    private void Start()
    {
        currentLevel = 0; // Definindo que o nivel atual será o que está no indice 0
    }

    // Logica para ir para o proximo nivel
    public void GoToNextLevel()
    {
        //Verificando se não existe proximo level
        if(currentLevel >= levels.Count-1) 
        {
            currentLevel = 0;
            
            SceneManager.LoadScene("MainMenu"); // Carrega a cena do menu principal
            Time.timeScale = 1f;

            return;
        }

        CurrentLevel = currentLevel + 1; // Aumentando o valor em 1

        menuController = GameObject.Find("CanvasMenu").GetComponent<MenuController>(); 
        menuController.GoToLevel(); // Carregar o proximo nivel
    }

    // Retorna o LevelData do nível atual
    public LevelData GetCurrentLevel()
    {
        if (currentLevel >= 0 && currentLevel < levels.Count)
        {
            return levels[currentLevel];
        }
        //Retorna null caso o currentLevel não for um valor valido
        return null;
    }
}
