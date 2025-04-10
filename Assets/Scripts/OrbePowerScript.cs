using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbePowerScript : MonoBehaviour
{
    private float speed = 0;
    private bool attackOrbe = false;
    public bool props = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void SetOrbe(float spd, bool attack, bool prop)
    {
        speed = spd;
        attackOrbe= attack;
        props = prop;

    }
    // Update is called once per frame
    void Update()
    {
        if(attackOrbe)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y >= 10f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(props)
            {
                gameManager.powerBtn.GetComponent<Button>().interactable = true;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            collision.gameObject.GetComponent<BossScript>().RemoverVidaBoss();
            Debug.Log("Acertou o Boss");
            Destroy(gameObject);
        }
    }

}
