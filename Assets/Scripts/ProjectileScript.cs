using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private BossScript bossScript;


    private void Start()
    {
        bossScript = FindAnyObjectByType<BossScript>();
    }
    public void SetDirection(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
    }

    void Update()
    {
        if (bossScript.vidaBoss <= 0)
        {
            Destroy(gameObject);
        }
        // Move o proj�til na dire��o especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // (Opcional) Destruir se sair da tela
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Exemplo de colis�o
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Proj�til acertou o jogador!");
            Destroy(gameObject);
        }
    }
}
