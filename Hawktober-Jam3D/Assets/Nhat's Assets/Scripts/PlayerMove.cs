using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField]
    private float cameraSensitivity = 500f;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private LayerMask groundLayer;


    public float rotationY;
    private Vector3 direction;
    private Rigidbody rb;
    private bool startCamera;
    private bool onFloor;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        StartCoroutine(StartCamera());
    }

    void Update()
    {
        Move();
        MoveCamera();
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

        rb.linearVelocity = new Vector3(direction.x * movementSpeed, rb.linearVelocity.y, direction.z * movementSpeed);
    }

    private void MoveCamera()
    {
        if (startCamera)
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
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
        else
        {
            onFloor = false;
        }
    }


    IEnumerator StartCamera()
    {
        yield return new WaitForSeconds(.5f);
        startCamera = true;
    }
}
