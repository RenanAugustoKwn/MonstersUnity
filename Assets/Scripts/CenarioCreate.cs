using UnityEngine;

public class CenarioCreate : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallPrefab;

    [Header("Refer�ncias")]
    public Transform levelParent;

    [Header("Configura��o")]
    public float levelWidth = 5f;          // Dist�ncia entre paredes
    public float scrollSpeed = 2f;         // Velocidade de descida do cen�rio
    public float wallHeight = 12f;         // Altura da parede (corresponde ao scale.y ou altura visual)

    private Transform lastRightWall;

    void Start()
    {

    }

    public void StartGenerate()
    {
        GenerateWallBlock(); // primeira parede
        GenerateWallBlock(); // segunda parede
    }

    void Update()
    {
        // Move o cen�rio para baixo
        levelParent.position += Vector3.down * scrollSpeed * Time.deltaTime;

        // Se a �ltima parede estiver abaixo de 5.8, gera uma nova acima dela
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

        Instantiate(wallPrefab, leftPos, Quaternion.identity, levelParent);
        lastRightWall = Instantiate(wallPrefab, rightPos, Quaternion.identity, levelParent).transform;
    }
}
