using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public GameObject orbePowerPrefab;

    private Rigidbody2D rb;
    public bool isGrounded = true;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool travarPlayer = true;
    private bool empurrandoPlayer = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMovingLeft && !empurrandoPlayer)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (isMovingRight && !empurrandoPlayer)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if(travarPlayer)
        {
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
    public void Empurra(bool paraDireita)
    {
        travarPlayer = false;
        empurrandoPlayer = true;
        StartCoroutine(TravarPlayer());
        float direcao = paraDireita ? 1f : -1f;
        rb.velocity = new Vector2(direcao * 5f, 1f);
        isGrounded = false;
    }
    public void JumpDano()
    {
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        isGrounded = false;
    }
    public void Attack()
    {
        if (gameManager.powerBtn.GetComponent<Button>().interactable)
        {
            GameObject powerClone = Instantiate(orbePowerPrefab, gameObject.transform.position, Quaternion.identity);
            OrbePowerScript powerScript = powerClone.GetComponent<OrbePowerScript>();
            Rigidbody2D rbClone = powerClone.GetComponent<Rigidbody2D>();
            rbClone.gravityScale = 0f;
            powerClone.GetComponent<BoxCollider2D>().isTrigger = true;
            if (powerScript != null)
            {
                powerScript.SetOrbe(8f, true, false);
            }
            gameManager.powerBtn.GetComponent<Button>().interactable = false;
        }
    }
    public void JumpReviver()
    {
        rb.velocity = new Vector2(rb.velocity.x, 9f);
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
            gameManager.RemoveVidaEspinhos();
        }
    }
    IEnumerator TravarPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            travarPlayer = true;
            empurrandoPlayer = false;
            StopAllCoroutines();
        }
    }
}
