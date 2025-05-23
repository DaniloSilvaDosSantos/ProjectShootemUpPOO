using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Referência ao menu principal
    [SerializeField] private GameObject mainMenu; 
    // Referência ao menu de pausa
    [SerializeField] private GameObject pauseMenu; 
    // Referência ao menu de tentar novamente
    [SerializeField] private GameObject tryagainMenu;
    [SerializeField] private GameObject playerAIMenu;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ShowMainMenu(); // Exibe o menu principal se estiver na cena do menu
        }
    }

    // Metodo para mostrar o menu principal
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true); // Ativa o menu principal
    }

    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void ShowPlayerAIMenu()
    {
        playerAIMenu.SetActive(true);
    }

    public void ActivatePlayerAI()
    {
        GameController.Instance.IsPlayerAI = true;
    }

    public void DeactivatePlayerAI()
    {
        GameController.Instance.IsPlayerAI = false;
    }

    // Metodo para mostrar o menu de pause
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true); // Ativa o menu de pausa
        Time.timeScale = 0f; // Pausa o jogo
    }

    // Metodo para ocultar o menu de pause
    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false); // Desativa o menu de pausa
        Time.timeScale = 1f; // Retoma o jogo
    }

    // Metodo para mostrar o menu da tela de tentar de novo
    public void showTryAgainMenu()
    {
        tryagainMenu.SetActive(true); // Ativa o menu de tentar novamente
        Time.timeScale = 0f; // Pausa o jogo
    }

    // Metodo para carregar a cena do nível
    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelRoom"); // Carrega a cena do nível
        Time.timeScale = 1f; // Retoma o tempo do jogo
    }

    // Metodo para carregar a cena do menu principal
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Carrega a cena do menu principal
        Time.timeScale = 1f; // Retoma o tempo do jogo
    }

    // Metodo para encerrar o jogo
    public void ExitGame()
    {
        Application.Quit(); // Fecha o jogo

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para o jogo no editor da Unity
        #endif
    }
}
