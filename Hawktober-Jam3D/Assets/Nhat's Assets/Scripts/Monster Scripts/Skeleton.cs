using UnityEngine;

public class Skeleton : Monster
{

    public Animator topAnim;
    public Animator bottomAnim;

    private Rigidbody rb;
    private bool isActive;
    private bool isSpawning; //Spawn time ~2.5s

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

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
        if (isActive)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            topAnim.Play("Idle");
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
}
