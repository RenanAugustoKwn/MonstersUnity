using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChao : MonoBehaviour
{

    private PlayerController player;
    private bool isPressed = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           player.isGrounded = true;
           player.GetComponent<BoxCollider2D>().isTrigger = false;
           collision.GetComponent<MovingPlatform>().SetCam();
           collision.GetComponent<MovingPlatform>().SetGround();
        }
        else
        {
            player.isGrounded = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
        }
    }
}
