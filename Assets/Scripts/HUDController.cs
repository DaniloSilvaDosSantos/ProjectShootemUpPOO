using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // Referência ao jogador
    private Player player; 
    // Vida atual do jogador
    private int currentPlayerLife; 
    // Lista de ícones da vida na HUD
    [SerializeField] private List<GameObject> lifeIcons; 
    // Pontuação do jogador
    [SerializeField] private int score; 
    // Referência ao texto da pontuação
    private TextMeshProUGUI scoreHud; 
    // Referência ao controlador de menus
    private MenuController menuController;
    // Referência ao controlador do game
    private GameController gameController;
    // Referência ao objetos dos textos de inicio de level e vitoria
    private GameObject startLevelHUD;
    private GameObject winLevelHUD;


    private void Start()
    {        
        if(player == null)
        {
            // Busca a referencia para o objeto do jogador com a tag "Player" na cena
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if(menuController == null)
        {
            // Busca a referencia para o objeto do CanvasMenu na cena
            menuController = GameObject.Find("CanvasMenu").GetComponent<MenuController>();
        }

        if(gameController == null)
        {
            // Busca a referencia para o objeto singleton GameController na cena
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        if (scoreHud == null)
        {
            // Busca a referencia do objeto Score que exibe a pontuação
            GameObject scoreObject = GameObject.Find("Score");
            if (scoreObject != null)
                scoreHud = scoreObject.GetComponent<TextMeshProUGUI>();
        }

        if (startLevelHUD == null)
        {
            startLevelHUD = GameObject.Find("StartLevel");
            UpdateStartLevelHUDText(startLevelHUD);
        }

        if(winLevelHUD ==  null)
        {
            winLevelHUD = GameObject.Find("WinLevel");
            winLevelHUD.SetActive(false);
        }

        StartCoroutine(DeactivateStartLevelHUD());
    }

    private void Update()
    {
        // Atualiza a exibição da vida do jogador
        HandlePlayerLifeDisplay();
        // Atualiza a exibição da pontuação
        HandleScoreDisplay();
    }

    // Atualiza os ícones de vida com base na vida atual do jogador
    private void HandlePlayerLifeDisplay()
    {
        currentPlayerLife = player.Life;

        for (int i = 0; i < lifeIcons.Count; i++)
        {
            // Ativa ou desativa os ícones de vida de acordo com o quanto de vida o jogador tem no momento
            if (i < currentPlayerLife)
                lifeIcons[i].SetActive(true);
            else
                lifeIcons[i].SetActive(false);
        }
    }

    // Atualiza o texto da pontuação
    private void HandleScoreDisplay()
    {
        if (scoreHud != null)
        {
            // Muda o valor exibido no Score da HUD para o valor da string score apos a sua formatação
            scoreHud.text = "Score: " + score.ToString("D8");
        }
    }

    public void OpenPauseMenu()
    {
        // Chama o menu de pausa
        menuController.ShowPauseMenu();
    }

    // Atualizando o texto do LevelStart ou WinLevel
    private void UpdateStartLevelHUDText(GameObject textHUD)
    {
        int levelNumber = gameController.CurrentLevel + 1;
        textHUD.GetComponent<TextMeshProUGUI>().text = "Level " + levelNumber.ToString();
    }

    public void ActivateWinLevelHUD()
    {
        winLevelHUD.SetActive(true);
        int levelNumber = gameController.CurrentLevel + 1;
        winLevelHUD.GetComponent<TextMeshProUGUI>().text = "Level " + levelNumber.ToString() + " Clear!";
    }

    private IEnumerator DeactivateStartLevelHUD()
    {
        yield return new WaitForSeconds(3f);
        startLevelHUD.SetActive(false);
    }
}
