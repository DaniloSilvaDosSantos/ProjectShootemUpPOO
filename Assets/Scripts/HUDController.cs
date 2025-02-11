using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private Player player;
    private int currentPlayerLife;
    [SerializeField] private List<GameObject> lifeIcons;
    [SerializeField] private int score;
    private TextMeshProUGUI scoreHud;
    private MenuController menuController;


    private void Start()
    {        
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if(menuController == null)
        {
            menuController =  GameObject.Find("CanvasMenu").GetComponent<MenuController>();
        }

        if (scoreHud == null)
        {
            GameObject scoreObject = GameObject.Find("Score");
            if (scoreObject != null)
                scoreHud = scoreObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        HandlePlayerLifeDisplay();
        HandleScoreDisplay();
    }

    private void HandlePlayerLifeDisplay()
    {
        currentPlayerLife = player.Life;

        for (int i = 0; i < lifeIcons.Count; i++)
        {
            if (i < currentPlayerLife)
                lifeIcons[i].SetActive(true);
            else
                lifeIcons[i].SetActive(false);
        }
    }

    private void HandleScoreDisplay()
    {
        if (scoreHud != null)
        {
            scoreHud.text = "Score: " + score.ToString("D8");
        }
    }

    public void OpenPauseMenu()
    {
        menuController.ShowPauseMenu();
    }
}
