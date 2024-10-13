using System.Collections;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{

    private Rigidbody rb;
    private Transform player;
    private Vector3 direction;
    private float moveSpeed = 10;

    void Start()
    {
        StartCoroutine(Despawn());
        rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<PlayerMove>().transform;

        direction = (player.position + Vector3.up) - transform.position;
        rb.linearVelocity = direction.normalized * moveSpeed;
    }

    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * 720 * Time.deltaTime);
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
