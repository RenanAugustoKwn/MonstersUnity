using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RectTransform imageSpawn;
    public GameObject player;
    public GameObject orbesPrefab;
    public GameObject pedrasPrefab;
    public GameObject[] vidasGO;
    public GameObject powerSlider;
    public GameObject gameOverPainel;
    public GameObject plataformaBase;
    public GameObject boss01;

    public GameObject powerBtn;
    public GameObject jumpBtn;

    public CenarioCreate cenarioCreate;

    public PlataformasCreate plataformasCreate;

    public Vector2 checkPoint;


    public float timeSpawnOrbes = 1f;
    public float timeSpawnPedras = 2f;
    public int orbesScore = 0;
    public int totalVida = 3;
    public int estagioFase = 0;

    bool moveboss01 = false;
    public static event Action DestroyPlats;

    public void ComecarACairOrbes()
    {
        StartCoroutine(SpawnLoopOrbes());
        StartCoroutine(SpawnLoopPedras());
    }
    // Update is called once per frame
    void Update()
    {
        if(moveboss01)
        {
            boss01.transform.Translate(Vector2.down * 1f * Time.deltaTime);
            if(boss01.transform.position.y <= 3.5f)
            {
                moveboss01 = false;
            }
        }
    }
    public void MecanicaBoss01()
    {

        plataformaBase.SetActive(true);
        plataformasCreate.PauseSpawn();
        cenarioCreate.scrollSpeed = 0f;
        DestroyPlats?.Invoke();
        boss01.SetActive(true);
        moveboss01 = true;
    }
    public void Boss01Derrotado()
    {
        orbesScore = 0;
        powerBtn.SetActive(false);
        jumpBtn.SetActive(true);
        FimEstagio01();
        estagioFase++;
    }
    public void FimEstagio01()
    {

    }
    public void RemoveVida()
    {
        totalVida--;
        if (totalVida == 3)
        {
            vidasGO[0].SetActive(true);
            vidasGO[1].SetActive(true);
            vidasGO[2].SetActive(true);
        }
        else if (totalVida == 2)
        {
            vidasGO[0].SetActive(false);
            vidasGO[1].SetActive(true);
            vidasGO[2].SetActive(true);
        }
        else if (totalVida == 1)
        {
            vidasGO[0].SetActive(false);
            vidasGO[1].SetActive(false);
            vidasGO[2].SetActive(true);
        }

        if (totalVida <= 0)
        {
            {
                vidasGO[0].SetActive(false);
                vidasGO[1].SetActive(false);
                vidasGO[2].SetActive(false);
            }
        }

        if(totalVida > 0)
        {
            player.transform.position = checkPoint;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<PlayerController>().JumpReviver();
        }
        else
        {
            player.SetActive(false);
            gameOverPainel.SetActive(true);
        }
    }
    public void AddOrbes()
    {
        orbesScore++;
        powerSlider.GetComponent<Slider>().value = orbesScore;
        if (orbesScore>=4 && estagioFase ==0)
        {
            jumpBtn.SetActive(false);
            powerBtn.SetActive(true);
            //Chama o Boss
            MecanicaBoss01();
        }
    }
    public void SpawnOrbes()
    {
        float width = imageSpawn.rect.width;

        Vector3 localPos = new Vector3(
            UnityEngine.Random.Range(-width / 2f, width / 2f),
            0f,
            0f
        );

        Vector3 worldPos = imageSpawn.TransformPoint(localPos);
        Instantiate(orbesPrefab, worldPos, Quaternion.identity);
    }
    public void SpawnPedras()
    {
        float width = imageSpawn.rect.width;

        Vector3 localPos = new Vector3(
            UnityEngine.Random.Range(-width / 2f, width / 2f),
            0f,
            0f
        );

        Vector3 worldPos = imageSpawn.TransformPoint(localPos);
        Instantiate(pedrasPrefab, worldPos, Quaternion.identity);
    }
    IEnumerator SpawnLoopOrbes()
    {
        while (true)
        {
            SpawnOrbes();
            yield return new WaitForSeconds(timeSpawnOrbes);
        }
    }
    IEnumerator SpawnLoopPedras()
    {
        while (true)
        {
            SpawnPedras();
            yield return new WaitForSeconds(timeSpawnPedras);
        }
    }
}
