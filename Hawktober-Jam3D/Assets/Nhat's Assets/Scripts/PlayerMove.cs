using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 6; //Can be updated by shovel script and reloading
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField]
    public float cameraSensitivity = 500f; //Can be updated by shovel script
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask graveLayer;


    public float rotationY;
    private Vector3 direction;
    private Rigidbody rb;
    private bool startCamera;
    private bool onFloor;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartCamera()); //Wait .5s before starting camera

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        RotatePlayer();
        CheckFloor();
        Jump();
        
    }
    private void Move()
    {
        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        direction.Normalize();

        rb.linearVelocity = new Vector3(direction.x * movementSpeed, rb.linearVelocity.y, direction.z * movementSpeed);
    }
    
    private void RotatePlayer()
    {
        if (startCamera)
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }
    private void CheckFloor()
    {
        Vector3 offset = new Vector3(0, .1f, 0);
        float rayDistance = .2f;
        if (Physics.Raycast(transform.position + offset, Vector3.down, rayDistance, groundLayer))
        {
            onFloor = true;
        }
        else if (Physics.Raycast(transform.position + offset, Vector3.down, rayDistance, graveLayer))
        {
            onFloor = true;
        }
        else
        {
            onFloor = false;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }
    IEnumerator StartCamera()
    {
        yield return new WaitForSeconds(.5f);
        startCamera = true;
    }
}
