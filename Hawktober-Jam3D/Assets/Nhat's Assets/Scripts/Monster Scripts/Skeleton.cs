using System.Collections;
using UnityEngine;

public class Skeleton : Monster
{
    public float Health = 100;
    public float WalkRange = 20;
    public float AttackRange = 9;
    public float BackUpRange = 5;

    public GameObject[] BodyParts;

    [SerializeField]
    private Animator topAnim;
    [SerializeField]
    private Animator bottomAnim;

    private MonsterSpawner spawnScript;

    private Transform player;
    private Rigidbody rb;
    private bool isSpawning; //Spawn time ~2.5s

    private bool isActive = false;
    private bool isDead = false;

    private bool inWalkRange = false;
    private bool inAttackRange = false;
    private bool inBackUpRange = false;

    //Target Code Variables
    private bool isTargetWalking = false;
    private float targetTimer = 1f;
    private Vector3 targetLoc;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        player = FindAnyObjectByType<PlayerMove>().transform;
        spawnScript = FindAnyObjectByType<MonsterSpawner>();

        Spawn();
    }

    void Update()
    {


        if (isSpawning)
        {
            transform.position += Vector3.up * Time.deltaTime;
            if (transform.position.y > .5 && !isDead)
            {
                isSpawning = false;
                isActive = true;
            }
        }

        //Get range booleans
        inWalkRange = (Vector3.Distance(player.position, transform.position) < WalkRange);
        inAttackRange = (Vector3.Distance(player.position, transform.position) < AttackRange);
        inBackUpRange = (Vector3.Distance(player.position, transform.position) < BackUpRange);



        if (isActive)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            //Need to relearn state machines... no time

            if (inAttackRange)
            {
                //Attack Code

                topAnim.Play("Attack");

                transform.LookAt(new Vector3(player.position.x, .5f, player.position.z));
                if (inBackUpRange)
                {
                    rb.linearVelocity = -transform.forward;
                    bottomAnim.Play("WalkBack");
                }
                else
                {
                    bottomAnim.Play("Idle");

                    rb.linearVelocity = Vector3.zero;
                }
            }
            else if (inWalkRange)
            {
                if (!topAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //Continues Attacking
                {
                    topAnim.Play("Walk");
                }
                //Walk towards player
                bottomAnim.Play("Walk");
                transform.LookAt(new Vector3(player.position.x, .5f, player.position.z));
                rb.linearVelocity = transform.forward * 2;
                isTargetWalking = false;
            }
            else //TargetCode
            {
                
                if (isTargetWalking)
                {
                    //Walk towards target
                    topAnim.Play("Walk");
                    bottomAnim.Play("Walk");
                    transform.LookAt(targetLoc);
                    rb.linearVelocity = transform.forward * 2;

                    if (Vector3.Distance(transform.position, targetLoc) < .2f)
                    {
                        isTargetWalking = false;
                        targetTimer = 1f;
                        topAnim.Play("Idle");
                        bottomAnim.Play("Idle");
                    }
                }
                else
                {
                    targetTimer -= Time.deltaTime;
                    rb.linearVelocity = Vector3.zero;
                    if (targetTimer <= 0) //Every second chooses random action between stay or walk towards target vector
                    {
                        targetTimer = 1f;
                        if (Random.Range(0, 4) == 0)
                        {
                            isTargetWalking = true;
                            targetLoc = GetTargetLocation();
                            
                        }
                        else
                        {
                            topAnim.Play("Idle");
                            bottomAnim.Play("Idle");
                        }
                    }
                }
            }
        }

        //Death
        if (Health <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                Die();
            }
        }
    }

    private void Spawn()
    {
        isSpawning = true;
        isActive = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | 
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        topAnim.Play("Spawn");
        bottomAnim.Play("Spawn");
    }

    private void Die()
    {
        isActive = false;

        //Remove from current enemy list in MonsterSpawner
        spawnScript.CurrentEnemies.Remove(gameObject);

        Destroy(GetComponent<CapsuleCollider>());
        Destroy(topAnim);
        Destroy(bottomAnim);
        Destroy(rb);
        foreach (GameObject part in BodyParts)
        {
            part.AddComponent<Rigidbody>();
            part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-20, 21), Random.Range(0, 301), Random.Range(-20, 21)));
            part.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-20, 21), Random.Range(-20, 21), Random.Range(-20, 21)));
            Destroy(gameObject, 2.5f);
        }
    }

    private Vector3 GetTargetLocation()
    {
        float targetX = transform.position.x + Random.Range(-5.0f, 5.0f); //Target range
        float targetZ = transform.position.z + Random.Range(-5.0f, 5.0f);
        targetX = Mathf.Clamp(targetX, -19.5f, 19.5f); //Map size
        targetZ = Mathf.Clamp(targetZ, -19.5f, 19.5f);

        return new Vector3(targetX, .5f, targetZ);
    }

}
