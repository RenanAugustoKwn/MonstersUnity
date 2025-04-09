using System.Collections;
using System.Collections.Generic;
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
    public Vector2 checkPoint;


    public float timeSpawnOrbes = 1f;
    public float timeSpawnPedras = 2f;
    public int orbesScore = 0;
    public int totalVida = 3;


    // Start is called before the first frame update
    void Start()
    {

    }

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
        if (orbesScore>=30)
        {
            //Chama o boss
        }
    }
    public void SpawnOrbes()
    {
        float width = imageSpawn.rect.width;

        Vector3 localPos = new Vector3(
            Random.Range(-width / 2f, width / 2f),
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
            Random.Range(-width / 2f, width / 2f),
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
