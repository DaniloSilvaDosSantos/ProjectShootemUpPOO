using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DeathBerrier : MonoBehaviour // Classe que destrói inimigos e tiros ao colidir com eles
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Shot"))
        {   
            // Destroi qualquer inimigo, com a tag "Enemy" ou "Shot" ou tiro que tocar na barreira
            Destroy(collision.gameObject); 
        }
    }
}
