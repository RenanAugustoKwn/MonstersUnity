using UnityEngine;
using static UnityEditor.SceneView;
using System;

public class MovingPlatform : MonoBehaviour
{
    public bool isVertical = false;
    public bool isHorizontal = false;
    public bool isCameraMove = false;

    public bool moveCenario = false;
    public float speed = 2f;

    public float speedUp = 3f;
    public float speedDown = 1f;
    // Para movimento vertical
    public float minY;
    public float maxY;

    private Vector2 direction;
    private Camera cam;

    private PlayerController player;

    private GameManager gameManager;
    private CenarioCreate cenarioCreate;
    private PlataformasCreate PlataformasCreate;

    void OnEnable()
    {
        GameManager.DestroyPlats += DestroyObectPlat;
    }

    void OnDisable()
    {
        GameManager.DestroyPlats -= DestroyObectPlat;
    }


    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        gameManager = FindAnyObjectByType<GameManager>();
        cenarioCreate = FindAnyObjectByType<CenarioCreate>();
        PlataformasCreate = FindAnyObjectByType<PlataformasCreate>();
        player = FindAnyObjectByType<PlayerController>();

        cam = FindFirstObjectByType<Camera>();
        if (isVertical && isHorizontal)
        {
            Debug.LogWarning("Apenas um dos modos deve estar ativado: 'isVertical' OU 'isHorizontal'.");
        }
        if(!isVertical && !isHorizontal)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        if (isVertical)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            maxY = transform.localPosition.y + maxY;
            minY = transform.localPosition.y - minY;
            direction = Vector2.up;
        }
        else if (isHorizontal)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            direction = Vector2.right;
        }

    }

    void Update()
    {
        if (isVertical)
        {
            float currentSpeed = direction == Vector2.up ? speedUp : speedDown;
            transform.Translate(direction * currentSpeed * Time.deltaTime);

            if (transform.localPosition.y >= maxY)
                direction = Vector2.down;
            else if (transform.localPosition.y <= minY)
                direction = Vector2.up;
        }
        else if (isHorizontal)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void SetCam()
    {
        if (isCameraMove)
        {
            cam.gameObject.transform.position = new Vector3(0, gameObject.transform.position.y + 2.5f, cam.gameObject.transform.position.z);
            gameManager.checkPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1f);
        }
    }

    public void SetGround()
    {
        player.gameObject.transform.SetParent(null);
        player.gameObject.transform.SetParent(transform);

        if (moveCenario)
        {
            cenarioCreate.scrollSpeed = 1.5f;
            if (!cenarioCreate.cenarioAtivo)
            {
                cenarioCreate.StartGenerate();
            }
            PlataformasCreate.StartSpawn();
            gameManager.plataformaBase.SetActive(false);
            moveCenario = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHorizontal && collision.gameObject.CompareTag("Wall"))
        {
            // Inverte direção horizontal ao colidir
            direction *= -1;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!collision.gameObject.GetComponent<PlayerController>().isGrounded)
            {
                collision.gameObject.GetComponent<PlayerController>().JumpPause();
            }
        }
        if (collision.gameObject.CompareTag("Espinhos") && isVertical)
        {
            // Procura por um filho com o componente PlayerController
            VerificaPlayer();
            DestroyObectPlat();
        }
    }
    private void DestroyObectPlat()
    {
        VerificaPlayer();
        Destroy(gameObject);
    }
    void VerificaPlayer()
    {
        PlayerController player = GetComponentInChildren<PlayerController>();
        if (player != null)
        {
            // Remove o parent do objeto que tem o PlayerController
            player.transform.SetParent(null);
        }
    }
}
