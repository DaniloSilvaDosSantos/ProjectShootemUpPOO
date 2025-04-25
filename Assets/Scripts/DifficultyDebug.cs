using UnityEngine;
using TMPro;

public class DifficultyDebug : MonoBehaviour
{
    private GameController gameController;
    private TextMeshProUGUI text;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();

        text = GetComponent<TextMeshProUGUI>();
        text.text = "Difficulty: "+gameController.DifficultLevel.ToString();
    }
}
