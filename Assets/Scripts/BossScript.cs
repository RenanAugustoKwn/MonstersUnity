using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [Header("Refer�ncias")]
    public GameObject projeteisPrefab;    // Prefab do proj�til
    public GameObject espinhosWallPrefab;
    public Transform firePoint;            // Ponto de onde os proj�teis s�o disparados
    public Transform player;               // Refer�ncia ao jogador

    [Header("Configura��es")]
    public float projectileSpeed = 5f;     // Velocidade do proj�til
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
        // Instancia o proj�til
        Vector3 pos = new(3.74f, 0, 0);
        GameObject wallClone = Instantiate(espinhosWallPrefab, pos, Quaternion.identity);

        // Calcula a dire��o do proj�til
        Vector2 direction = Vector2.left;

        // Envia os dados de dire��o e velocidade pro script do proj�til
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

        // Calcula a dire��o do proj�til
        Vector2 direction = Vector2.right;

        // Envia os dados de dire��o e velocidade pro script do proj�til
        EspinhosWall wallScript = wallClone.GetComponent<EspinhosWall>();
        if (wallScript != null)
        {
            wallScript.SetDirection(direction, paredeSpeed, pos.x, false);
        }
    }

    void FireProjectile()
    {
        if (player == null) return;

        // Instancia o proj�til
        GameObject projClone = Instantiate(projeteisPrefab, firePoint.position, Quaternion.identity);

        // Calcula a dire��o do proj�til
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Envia os dados de dire��o e velocidade pro script do proj�til
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
