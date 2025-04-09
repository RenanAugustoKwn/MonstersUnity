using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    public void SetDirection(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
    }

    void Update()
    {
        // Move o projétil na direção especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // (Opcional) Destruir se sair da tela
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Exemplo de colisão
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Projétil acertou o jogador!");
            Destroy(gameObject);
        }
    }
}
