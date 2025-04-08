using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    public bool isGrounded = true;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
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
}
