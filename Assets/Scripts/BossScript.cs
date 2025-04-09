using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [Header("Referências")]
    public GameObject projeteisPrefab;    // Prefab do projétil
    public GameObject espinhosWallPrefab;
    public Transform firePoint;            // Ponto de onde os projéteis são disparados
    public Transform player;               // Referência ao jogador

    [Header("Configurações")]
    public float projectileSpeed = 5f;     // Velocidade do projétil
    public float paredeSpeed = 5f;
    public float fireRate = 1f;            // Intervalo entre disparos (em segundos)
    public float spawnFireTime = 5f;       // Tempo disparando

    private float fireTimer = 0f;
    private bool spawnFire = true;
    private bool spawnWall = false;

    private void Start()
    {
        StartCoroutine(SpawnFireTime());
    }

    void Update()
    {
        if (spawnFire)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireRate)
            {
                FireProjectile();
                fireTimer = 0f;
            }
        }

    }
    void CriarEspinhosWallE()
    {
        // Instancia o projétil
        Vector3 pos = new(3.74f, 0, 0);
        GameObject wallClone = Instantiate(espinhosWallPrefab, pos, Quaternion.identity);

        // Calcula a direção do projétil
        Vector2 direction = Vector2.left;

        // Envia os dados de direção e velocidade pro script do projétil
        EspinhosWall wallScript = wallClone.GetComponent<EspinhosWall>();
        if (wallScript != null)
        {
            wallScript.SetDirection(direction, paredeSpeed, pos.x,true);
        }
    }
    void CriarEspinhosWallD()
    {
        Vector3 pos = new(-3.74f, 0, 0);
        GameObject wallClone = Instantiate(espinhosWallPrefab, pos, Quaternion.identity);

        // Calcula a direção do projétil
        Vector2 direction = Vector2.right;

        // Envia os dados de direção e velocidade pro script do projétil
        EspinhosWall wallScript = wallClone.GetComponent<EspinhosWall>();
        if (wallScript != null)
        {
            wallScript.SetDirection(direction, paredeSpeed, pos.x, false);
        }
    }

    void FireProjectile()
    {
        if (player == null) return;

        // Instancia o projétil
        GameObject projClone = Instantiate(projeteisPrefab, firePoint.position, Quaternion.identity);

        // Calcula a direção do projétil
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Envia os dados de direção e velocidade pro script do projétil
        ProjectileScript projectileScript = projClone.GetComponent<ProjectileScript>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction, projectileSpeed);
        }
    }

    IEnumerator SpawnFireTime()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(spawnFireTime);
            spawnFire = false;
            CriarEspinhosWallD();
            //CriarEspinhosWallE();
            StopAllCoroutines();
        }
    }
}
