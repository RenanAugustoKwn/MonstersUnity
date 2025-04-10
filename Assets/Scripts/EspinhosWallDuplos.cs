using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EspinhosWallDuplos : MonoBehaviour
{
    public GameObject[] wallsEspinhos;
    private float speed = 2f;
    private float slowTime = 2f;
    private float pauseTime = 2f;
    private bool avancar = false;
    private bool voltar = false;
    private float posInitial;

    public static event Action BossTimeFire;
    private BossScript bossScript;


    private void Start()
    {
        bossScript = FindAnyObjectByType<BossScript>();
        StartCoroutine(AvancarTime());
    }
    public void SetSpeed(float spd)
    {
        speed = spd;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossScript.vidaBoss <= 0)
        {
            Destroy(gameObject);
        }
        if (avancar)
        {
            // Move a parede na direção especificada
            wallsEspinhos[0].transform.Translate(Vector2.left * speed * Time.deltaTime);
            wallsEspinhos[1].transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (wallsEspinhos[0].transform.position.x <= 2.2f)
            {
                avancar = false;
                StartCoroutine(ParadoTime());

            }
            else if (wallsEspinhos[1].transform.position.x >= -2.2f)
            {
                avancar = false;
                StartCoroutine(ParadoTime());

            }
        }
        else if (voltar)
        {
            // Move a parede na direção especificada
            wallsEspinhos[1].transform.Translate(Vector2.left * speed * Time.deltaTime);
            wallsEspinhos[0].transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (wallsEspinhos[0].transform.position.x >= 3.74f)
            {
                voltar = false;
                BossTimeFire?.Invoke();
                Destroy(gameObject);
            }
            else if (wallsEspinhos[1].transform.position.x <= -3.74f)
            {
                voltar = false;
                BossTimeFire?.Invoke();
                Destroy(gameObject);
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
