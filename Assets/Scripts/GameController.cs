using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private int currentLevel;
    [SerializeField] private  List<LevelData> levels;

    public int CurrentLevel
    {
        get {return currentLevel;}
        set 
        {
            if (currentLevel >= 0 && currentLevel < levels.Count)
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
        currentLevel = 0;   
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
