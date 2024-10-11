using UnityEngine;

public class Skeleton : Monster
{
    //health = 100
    public float WalkRange = 16;
    public float AttackRange = 9;
    public float BackUpRange = 5;


    [SerializeField]
    private Animator topAnim;
    [SerializeField]
    private Animator bottomAnim;

    private Transform player;
    private Rigidbody rb;
    private bool isSpawning; //Spawn time ~2.5s

    private bool isActive = false;
    private bool isAttacking = false;
    private bool attackingHasRun = false;

    private bool InWalkRange = false;
    private bool InAttackRange = false;
    private bool InBackUpRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        player = FindAnyObjectByType<PlayerMove>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Spawn();
        }

        if (isSpawning)
        {
            transform.position += Vector3.up * Time.deltaTime;
            if (transform.position.y > .5)
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

    }

    private void Spawn()
    {
        isSpawning = true;
        isActive = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        transform.localPosition = Vector3.zero;
        topAnim.Play("Spawn");
        bottomAnim.Play("Spawn");
    }


    public void ThrowBone() //Called from top anim player
    {

    }

}
