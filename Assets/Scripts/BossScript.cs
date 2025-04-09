using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [Header("Refer�ncias")]
    public GameObject projeteisPrefab;    // Prefab do proj�til
    public GameObject espinhosWallPrefab;
    public GameObject espinhosWallDuploPrefab;
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

    public int sequenceSkill = 0;

    void OnEnable()
    {
        EspinhosWall.BossTimeFire += VoltarAtirar;
        EspinhosWallDuplos.BossTimeFire += VoltarAtirar;
    }

    void OnDisable()
    {
        EspinhosWall.BossTimeFire -= VoltarAtirar;
        EspinhosWallDuplos.BossTimeFire -= VoltarAtirar;
    }
    void VoltarAtirar()
    {
        spawnFire = true;
        sequenceSkill++;
        StartCoroutine(SpawnFireTime());
    }

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
    void CriarEspinhosDuplo()
    {
        Vector3 pos = new(0, 0, 0);
        GameObject wallClone = Instantiate(espinhosWallDuploPrefab, pos, Quaternion.identity);
        // Envia os dados de dire��o e velocidade pro script do proj�til
        EspinhosWallDuplos wallScript = wallClone.GetComponent<EspinhosWallDuplos>();
        if (wallScript != null)
        {
            wallScript.SetSpeed(paredeSpeed);
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
            if(sequenceSkill == 0)
            {
                CriarEspinhosWallE();
            }
            else if (sequenceSkill == 1)
            {
                CriarEspinhosWallD();
            }
            else if (sequenceSkill == 2)
            {
                CriarEspinhosDuplo();
            }
            StopAllCoroutines();
        }
    }
}
