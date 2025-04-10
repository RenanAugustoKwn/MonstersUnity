using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedeDuplasWall : MonoBehaviour
{
    private GameManager gameManager;
    public bool wallE = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Parede acertou o jogador!");

            gameManager.player.GetComponent<PlayerController>().JumpDano();
            gameManager.RemoveVidaWall();
        }
    }
}
