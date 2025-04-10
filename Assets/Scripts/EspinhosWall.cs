using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EspinhosWall : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 2f;
    private float slowTime = 2f;
    private float pauseTime = 2f;
    private bool avancar = false;
    private bool voltar = false;
    private bool trocarDir = false;
    private float posInitial;
    private bool wallE = false;
    private BossScript bossScript;

    public static event Action BossTimeFire;


    private void Start()
    {
        bossScript = FindAnyObjectByType<BossScript>();
        StartCoroutine(AvancarTime());
    }
    public void SetDirection(Vector2 dir, float spd, float posInit, bool wallEsquerda)
    {
        direction = dir.normalized;
        speed = spd;
        posInitial = posInit;
        wallE = wallEsquerda;

    }

    // Update is called once per frame
    void Update()
    {
        if (bossScript.vidaBoss <=0)
        {
            Destroy(gameObject);
        }
        if (avancar)
        {
            // Move a parede na direção especificada
            transform.Translate(direction * speed * Time.deltaTime);

            if(wallE)
            {
                if (transform.position.x <= 1.13f)
                {
                    avancar = false;
                    trocarDir = true;
                    StartCoroutine(ParadoTime());

                }
            }
            else
            {
                if (transform.position.x >= -1.13f)
                {
                    avancar = false;
                    trocarDir = true;
                    StartCoroutine(ParadoTime());

                }
            }

        }
        else if (voltar)
        {
            // Move a parede na direção especificada
            if (trocarDir)
            {
                if (direction == Vector2.right)
                {
                    direction = Vector2.left;
                }
                else
                {
                    direction = Vector2.right;
                }
                trocarDir = false;
            }

            transform.Translate(direction * speed * Time.deltaTime);

            if (wallE)
            {
                if (transform.position.x >= posInitial)
                {
                    voltar = false;
                    BossTimeFire?.Invoke();
                    Destroy(gameObject);
                }
            }
            else
            {
                if (transform.position.x <= posInitial)
                {
                    voltar = false;
                    BossTimeFire?.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Parede acertou o jogador!");
        }
    }
    IEnumerator AvancarTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(slowTime);
            avancar = true;
            StopAllCoroutines();
        }
    }
    IEnumerator ParadoTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(pauseTime);
            voltar = true;
            StopAllCoroutines();
        }
    }
}
