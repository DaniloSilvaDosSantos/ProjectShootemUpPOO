using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject tryagainMenu;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            ShowMainMenu();
        }
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void showTryAgainMenu()
    {
        tryagainMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelRoom");
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
