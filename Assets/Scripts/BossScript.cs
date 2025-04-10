using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [Header("Referências")]
    public GameObject projeteisPrefab;    // Prefab do projétil
    public GameObject espinhosWallPrefab;
    public GameObject espinhosWallDuploPrefab;
    public GameObject orbePowerPrefab;
    public Transform firePoint;            // Ponto de onde os projéteis são disparados

    [Header("Configurações")]
    public float projectileSpeed = 5f;     // Velocidade do projétil
    public float paredeSpeed = 5f;
    public float fireRate = 1f;            // Intervalo entre disparos (em segundos)
    public float spawnFireTime = 5f;       // Tempo disparando
    public int vidaBoss = 3;

    private float fireTimer = 0f;
    private bool spawnFire = true;
    private bool spawnWall = false;
    private bool moveboss = true;

    public int sequenceSkill = 0;

    private GameManager gameManager;

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
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        StartCoroutine(SpawnFireTime());
    }
    public void RemoverVidaBoss()
    {
        Debug.Log("Removeu Vida");
        vidaBoss--;
        if (vidaBoss <= 0)
        {
            gameObject.SetActive(false);
            gameManager.Boss01Derrotado();
        }
    }
    void VoltarAtirar()
    {
        spawnFire = true;
        sequenceSkill++;
        CreateOrbePower();
        StartCoroutine(SpawnFireTime());
    }
    void CreateOrbePower()
    {
        Vector3 pos = new(Random.Range(-1.65f, 1.65f), -3f, 0);
        GameObject powerClone = Instantiate(orbePowerPrefab, pos, Quaternion.identity);
        OrbePowerScript powerScript = powerClone.GetComponent<OrbePowerScript>();
        if (powerScript != null)
        {
            powerScript.SetOrbe(0f, false, true);
        }
    }

    void Update()
    {
        if(moveboss)
        {
            transform.Translate(Vector2.down * 1f * Time.deltaTime);
            if (transform.position.y <= 3.5f)
            {
                moveboss = false;
            }
        }
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
    void CriarEspinhosDuplo()
    {
        Vector3 pos = new(0, 0, 0);
        GameObject wallClone = Instantiate(espinhosWallDuploPrefab, pos, Quaternion.identity);
        // Envia os dados de direção e velocidade pro script do projétil
        EspinhosWallDuplos wallScript = wallClone.GetComponent<EspinhosWallDuplos>();
        if (wallScript != null)
        {
            wallScript.SetSpeed(paredeSpeed);
        }
    }

    void FireProjectile()
    {
        if (gameManager.player.GetComponent<PlayerController>() == null) return;

        // Instancia o projétil
        GameObject projClone = Instantiate(projeteisPrefab, firePoint.position, Quaternion.identity);

        // Calcula a direção do projétil
        Vector2 direction = (gameManager.player.transform.position - firePoint.position).normalized;

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
            if(sequenceSkill == 0)
            {
                CriarEspinhosWallE();
                StopAllCoroutines();
            }
            else if (sequenceSkill == 1)
            {
                CriarEspinhosWallD();
                StopAllCoroutines();
            }
            else if (sequenceSkill == 2)
            {
                CriarEspinhosDuplo();
                StopAllCoroutines();
            }
            else if (sequenceSkill >= 3)
            {
                int skillRandom = Random.Range(0, 3);
                if(skillRandom == 0)
                {
                    CriarEspinhosWallE();
                    StopAllCoroutines();
                }
                else if(skillRandom == 1)
                {
                    CriarEspinhosWallD();
                    StopAllCoroutines();
                }
                else if (skillRandom == 2)
                {
                    CriarEspinhosDuplo();
                    StopAllCoroutines();
                }
                else if (skillRandom == 3)
                {
                    spawnFire = true;
                }
            }
        }
    }
}
