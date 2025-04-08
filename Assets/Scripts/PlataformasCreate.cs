using UnityEngine;
using System.Collections;

public class PlataformasCreate : MonoBehaviour
{
    [Header("Configurações")]
    public Transform[] spawnPoints;            // Posições onde as plataformas serão geradas
    public GameObject platformPrefab;          // Prefab da plataforma
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

    IEnumerator SpawnPlatformsLoop()
    {
        while (true)
        {
            if (spawnPoints.Length == 0) yield break;

            Transform spawnPoint = spawnPoints[currentIndex];

            GameObject platform = Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);
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
