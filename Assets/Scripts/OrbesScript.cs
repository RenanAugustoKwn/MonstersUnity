using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbesScript : MonoBehaviour
{
    GameManager gameManager;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.AddOrbes();
            Destroy(gameObject);
        }
    }
}
