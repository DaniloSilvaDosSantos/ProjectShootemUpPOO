using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DeathBerrier : MonoBehaviour // Classe que destrói inimigos e tiros ao colidir com eles
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {   
            // Destroi qualquer inimigo que tocar na barreira
            Destroy(collision.gameObject); 
        }
        else if (collision.gameObject.CompareTag("Shot"))
        {
            // Desativa qualquer tiro que tocar na barreira
            collision.gameObject.SetActive(false);
        }
    }
}
