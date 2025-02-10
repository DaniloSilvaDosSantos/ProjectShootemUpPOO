using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private Player player;
    private int currentPlayerLife;
    [SerializeField] private List<GameObject> lifeIcons;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    private void Update()
    {
        HandlePlayerLifeDisplay();
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
}
