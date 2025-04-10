using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EspinhosFinal : MonoBehaviour
{
    GameManager gameManager;
    public RectTransform uiTarget; // o RectTransform da UI (ex: Image)
    public Camera uiCamera;        // a câmera do Canvas (geralmente Camera.main)
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, uiTarget.position);

        // Converte para posição no mundo
        Vector3 worldPos = uiCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0; // mantém no plano 2D

        // Atualiza posição do sprite
        gameObject.transform.position = worldPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.RemoveVidaEspinhos();
        }
    }
}
