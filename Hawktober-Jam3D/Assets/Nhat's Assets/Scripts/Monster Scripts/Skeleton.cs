using System.Collections;
using UnityEngine;

public class Skeleton : Monster
{
    public float Health = 100;
    public float WalkRange = 16;
    public float AttackRange = 9;
    public float BackUpRange = 5;

    public GameObject[] BodyParts;

    [SerializeField]
    private Animator topAnim;
    [SerializeField]
    private Animator bottomAnim;

    private Transform player;
    private Rigidbody rb;
    private bool isSpawning; //Spawn time ~2.5s

    private bool isActive = false;
    private bool isDead = false;

    private bool InWalkRange = false;
    private bool InAttackRange = false;
    private bool InBackUpRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        player = FindAnyObjectByType<PlayerMove>().transform;

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
        InWalkRange = (Vector3.Distance(player.position, transform.position) < WalkRange);
        InAttackRange = (Vector3.Distance(player.position, transform.position) < AttackRange);
        InBackUpRange = (Vector3.Distance(player.position, transform.position) < BackUpRange);



        if (isActive)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            //Need to relearn state machines... no time

            if (InAttackRange)
            {
                //Attack Code

                topAnim.Play("Attack");

                transform.LookAt(new Vector3(player.position.x, .5f, player.position.z));
                if (InBackUpRange)
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
            else if (InWalkRange)
            {
                //Walk towards player
                topAnim.Play("Walk");
                bottomAnim.Play("Walk");
                transform.LookAt(new Vector3(player.position.x, .5f, player.position.z));
                rb.linearVelocity = transform.forward * 2;
            }
            else
            {
                //Add target code
                topAnim.Play("Idle");
                bottomAnim.Play("Idle");
            }


        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Health = 0;
            Debug.Log(Random.Range(-1, 2));
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

        Destroy(GetComponent<CapsuleCollider>());
        Destroy(topAnim);
        Destroy(bottomAnim);
        Destroy(rb);
        foreach (GameObject part in BodyParts)
        {
            part.AddComponent<Rigidbody>();
            part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-20, 21), Random.Range(0, 301), Random.Range(-20, 21)));
            part.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-20, 21), Random.Range(-20, 21), Random.Range(-20, 21)));
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        float despawnTime = 2.5f;
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
