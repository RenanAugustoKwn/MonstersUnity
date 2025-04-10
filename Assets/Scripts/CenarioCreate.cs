using UnityEngine;

public class CenarioCreate : MonoBehaviour
{
    [Header("Configuração")]
    public float levelWidth = 5f;          // Distância entre paredes
    public float scrollSpeed = 2f;         // Velocidade de descida do cenário
    public float wallHeight = 12f;         // Altura da parede (corresponde ao scale.y ou altura visual)
    public bool cenarioAtivo = false;

    [Header("Prefabs")]
    public GameObject wallPrefab;

    [Header("Referências")]
    public Transform levelParent;

    private Transform lastRightWall;

    void Start()
    {

    }

    public void StartGenerate()
    {
        GenerateWallBlock(); // primeira parede
        GenerateWallBlock(); // segunda parede
        cenarioAtivo = true;
    }

    void Update()
    {
        // Move o cenário para baixo
        levelParent.position += Vector3.down * scrollSpeed * Time.deltaTime;

        // Se a última parede estiver abaixo de 5.8, gera uma nova acima dela
        if (lastRightWall != null && lastRightWall.position.y < 5.8f)
        {
            GenerateWallBlock();
        }
    }

    void GenerateWallBlock()
    {
        float newY = lastRightWall != null ? lastRightWall.position.y + wallHeight : 12f;

        Vector3 leftPos = new Vector3(-levelWidth / 2f, newY, 0f);
        Vector3 rightPos = new Vector3(levelWidth / 2f, newY, 0f);

        // Instancia a parede da esquerda e define a tag
        GameObject leftWall = Instantiate(wallPrefab, leftPos, Quaternion.identity, levelParent);
        leftWall.tag = "Wall2";

        // Instancia a parede da direita e define a tag
        GameObject rightWall = Instantiate(wallPrefab, rightPos, Quaternion.identity, levelParent);
        rightWall.tag = "Wall";

        // Atualiza a referência da última parede direita criada
        lastRightWall = rightWall.transform;
    }
}
