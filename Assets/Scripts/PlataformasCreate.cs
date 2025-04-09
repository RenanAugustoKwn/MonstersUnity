using UnityEngine;
using System.Collections;

public class PlataformasCreate : MonoBehaviour
{
    [Header("Configurações")]
    public Transform[] spawnPoints;            // Posições onde as plataformas serão geradas
    public GameObject platformPrefab;          // Prefab da plataforma
    public GameObject orbesPower;
    public float spawnInterval = 1f;           // Tempo entre cada plataforma
    public bool randomMode = false;            // Liga o modo aleatório

    private int currentIndex = 0;
    private bool waitingForRandom = true;      // Controla se estamos prontos para escolher aleatório
    public GameObject cenarioParent;

    void Start()
    {

    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnPlatformsLoop());
    }
    public void PauseSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnPlatformsLoop()
    {
        while (true)
        {
            if (spawnPoints.Length == 0) yield break;

            Transform spawnPoint = spawnPoints[currentIndex];

            GameObject platform = Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);
            int moveRandom = Random.Range(0, 3);
            int orbRandom = Random.Range(0, 3);

            if (orbRandom == 0 || orbRandom == 1) 
            {
                Vector3 orbePos = new Vector3(platform.transform.position.x, platform.transform.position.y + 0.5f, platform.transform.position.z);
                GameObject orbeClone = Instantiate(orbesPower, orbePos, Quaternion.identity);
                orbeClone.transform.SetParent(platform.transform);
            }

            if (currentIndex == 1)
            {
                if (moveRandom == 0)
                {
                    platform.gameObject.GetComponent<MovingPlatform>().isHorizontal = true;
                }
                else if (moveRandom == 2)
                {
                    platform.gameObject.GetComponent<MovingPlatform>().isVertical = true;
                }
            }


            platform.transform.SetParent(cenarioParent.transform);

            yield return new WaitForSeconds(spawnInterval);

            if (randomMode)
            {
                if (currentIndex == 1 && waitingForRandom)
                {
                    // Escolhe aleatoriamente entre 0 ou 2
                    currentIndex = Random.value < 0.5f ? 0 : 2;
                    waitingForRandom = false;
                }
                else
                {
                    // Sempre volta para 1
                    currentIndex = 1;
                    waitingForRandom = true;
                }
            }
            else
            {
                // Modo ping-pong normal
                currentIndex += direction;

                if (currentIndex == spawnPoints.Length - 1 || currentIndex == 0)
                {
                    direction *= -1;
                }
            }
        }
    }

    // Só usada no modo ping-pong tradicional
    private int direction = 1;
}
