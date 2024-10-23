using UnityEngine;
using System.Collections;

public class Pumpkin : MonoBehaviour
{
    public float Damage = 20f;

    public float Health = 20f;
    public float WalkRange = 50;
    public float BlowUpRange = 2;
    private float moveSpeed = 4f;

    private Rigidbody rb;
    private Player player;

    public GameObject explosionPrefab;
    public Light explodeLight;
    private ParticleSystem explosionParticles;
    private float explosionLightIntensity = 0;
    private float lightIncreaseRate = 5f;

    public ParticleSystem FuseParticles;
    public GameObject DamageArea;


    private bool isSpawning = true;
    private bool isActive = false;
    private bool isDead = false;
    private bool showLight = false;

    private bool inWalkRange;
    private bool inBlowUpRange;

    private MonsterSpawner spawnScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<Player>();
        spawnScript = FindAnyObjectByType<MonsterSpawner>();
        explodeLight.intensity = explosionLightIntensity;
        isDead = false;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        inWalkRange = (Vector3.Distance(player.transform.position, transform.position) < WalkRange);
        inBlowUpRange = (Vector3.Distance(player.transform.position, transform.position) < BlowUpRange);

        if (isSpawning)
        {
            transform.position += Vector3.up * Time.deltaTime;
            if (transform.position.y > 1f && !isDead)
            {
                isSpawning = false;
                isActive = true;
            }
        }

        if (Health <= 0)
        {
            if (!isDead)
            {
                isActive = false;
                isDead = true;
                StartCoroutine(Explode());
            }
        }

        if (isActive)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            //Need to relearn state machines... no time

            if (inBlowUpRange)
            {
                if (!isDead)
                {
                    isActive = false;
                    isDead = true;
                    StartCoroutine(Explode());
                }
            }
            else if (inWalkRange)
            {
                transform.LookAt(new Vector3(player.transform.position.x, 1f, player.transform.position.z));
                rb.linearVelocity = transform.forward * moveSpeed;
            }
            else //Stop moving if out of range
            {
                rb.linearVelocity = Vector3.zero;

            }
        }

        if (showLight)
        {

            explosionLightIntensity += lightIncreaseRate * Time.deltaTime;
            explodeLight.intensity = explosionLightIntensity;
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Works");
            StartCoroutine(Explode());
        }

        


    }

    private IEnumerator Explode()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        //Show light
        FuseParticles.Play();
        showLight = true;
        yield return new WaitForSeconds(.65f);
        DamageArea.SetActive(true);
        yield return new WaitForSeconds(.85f);
        showLight = false;
        //Instantiate explosion particles;
        GameObject explosion = Instantiate(explosionPrefab, transform.position + new Vector3(0, .4f, 0), transform.rotation);
        explosion.transform.Find("Point Light").GetComponent<Light>().intensity = explosionLightIntensity + 60;
        Die();

    }
    private void Die()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 4)
        {
            player.Health -= Damage;
        }
        isActive = false;

        spawnScript.CurrentEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
