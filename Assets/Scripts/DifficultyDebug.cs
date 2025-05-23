using UnityEngine;
using TMPro;

public class DifficultyDebug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Difficulty: " + GameController.Instance.DifficultLevel.ToString();
    }
}
