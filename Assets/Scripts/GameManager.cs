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
    public GameObject boss01Prefab;
    public GameObject[] trocaDeEstagio;

    public GameObject powerBtn;
    public GameObject jumpBtn;

    public CenarioCreate cenarioCreate;

    public PlataformasCreate plataformasCreate;
    public GameObject plataformasPrefab;

    public Vector2 checkPoint;


    public float timeSpawnOrbes = 1f;
    public float timeSpawnPedras = 2f;
    public int orbesScore = 0;
    public int totalVida = 3;
    public int estagioFase = 0;

    public static event Action DestroyPlats;

    bool delayRemoveVida = true;

    public void ComecarACairOrbes()
    {
        StartCoroutine(SpawnLoopOrbes());
        StartCoroutine(SpawnLoopPedras());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void MecanicaBoss01()
    {

        plataformaBase.SetActive(true);
        plataformasCreate.PauseSpawn();
        cenarioCreate.scrollSpeed = 0f;
        DestroyPlats?.Invoke();

        Vector3 pos = new(0, 6.45f, 0);
        GameObject bossClone = Instantiate(boss01Prefab, pos, Quaternion.identity);
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
        Vector3 pos1 = new(trocaDeEstagio[1].transform.position.x, trocaDeEstagio[1].transform.position.y, 0);
        GameObject platClone1 = Instantiate(plataformasPrefab, pos1, Quaternion.identity);
        platClone1.transform.SetParent(cenarioCreate.gameObject.transform);

        Vector3 pos2 = new(trocaDeEstagio[2].transform.position.x, trocaDeEstagio[2].transform.position.y, 0);
        GameObject platClone2 = Instantiate(plataformasPrefab, pos2, Quaternion.identity);
        platClone2.transform.SetParent(cenarioCreate.gameObject.transform);

        Vector3 pos3 = new(trocaDeEstagio[3].transform.position.x, trocaDeEstagio[3].transform.position.y, 0);
        GameObject platClone3 = Instantiate(plataformasPrefab, pos3, Quaternion.identity);
        platClone3.transform.SetParent(cenarioCreate.gameObject.transform);

        MovingPlatform platScript = platClone3.GetComponent<MovingPlatform>();
        platScript.moveCenario = true;

        Vector3 pos4 = new(trocaDeEstagio[4].transform.position.x, trocaDeEstagio[4].transform.position.y, 0);
        GameObject platClone4 = Instantiate(plataformasPrefab, pos4, Quaternion.identity);
        platClone4.transform.SetParent(cenarioCreate.gameObject.transform);

        player.GetComponent<PlayerController>().isGrounded =true;

    }
    public void RemoveVidaWall()
    {
        RemoveVidaDelay();
    }
    public void RemoveVidaEspinhos()
    {
        RemoveVida();

        if (totalVida > 0)
        {
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<PlayerController>().JumpReviver();
        }
    }
    public void RemoveVidaSkill()
    {
        RemoveVida();
    }
    void RemoveVida()
    {
        totalVida--;

        for (int i = 0; i < vidasGO.Length; i++)
        {
            // Ativa se o índice for menor que o total de vidas restantes
            vidasGO[i].SetActive(i < totalVida);
        }
        if (totalVida <= 0)
        {
            player.SetActive(false);
            gameOverPainel.SetActive(true);
        }
    }
    void RemoveVidaDelay()
    {
        StartCoroutine(DelayVida());

        if(delayRemoveVida)
        {
            delayRemoveVida = false;
            RemoveVida();
        }

    }
    public void AddOrbes()
    {
        orbesScore++;
        powerSlider.GetComponent<Slider>().value = orbesScore;
        if (orbesScore>=5)
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
    IEnumerator DelayVida()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            delayRemoveVida = true;
            StopCoroutine(DelayVida());
        }
    }
}
