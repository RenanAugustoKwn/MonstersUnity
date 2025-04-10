using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public GameObject orbePowerPrefab;

    private Rigidbody2D rb;
    public bool isGrounded = true;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            // Parar no eixo X se nenhum botão estiver pressionado
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (!isGrounded)
        {
            gameObject.transform.SetParent(null);
        }
    }

    // Chamado pelo botão de mover para a esquerda (OnPointerDown)
    public void StartMovingLeft()
    {
        isMovingLeft = true;
    }

    // Chamado pelo botão de mover para a esquerda (OnPointerUp)
    public void StopMovingLeft()
    {
        isMovingLeft = false;
    }

    // Chamado pelo botão de mover para a direita (OnPointerDown)
    public void StartMovingRight()
    {
        isMovingRight = true;
    }

    // Chamado pelo botão de mover para a direita (OnPointerUp)
    public void StopMovingRight()
    {
        isMovingRight = false;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }
    public void Attack()
    {
        if (gameManager.powerBtn.GetComponent<Button>().interactable)
        {
            GameObject powerClone = Instantiate(orbePowerPrefab, gameObject.transform.position, Quaternion.identity);
            OrbePowerScript powerScript = powerClone.GetComponent<OrbePowerScript>();
            if (powerScript != null)
            {
                powerScript.SetOrbe(8f, true, false);
            }
            gameManager.powerBtn.GetComponent<Button>().interactable = false;
        }
    }
    public void JumpReviver()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }
    public void JumpPause()
    {
        rb.velocity = new Vector2(rb.velocity.x,0f);
        isGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Espinhos"))
        {
            gameManager.RemoveVida();
        }
    }
}
