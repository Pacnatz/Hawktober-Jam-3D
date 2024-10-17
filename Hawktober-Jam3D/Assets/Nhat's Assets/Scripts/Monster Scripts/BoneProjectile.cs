using System.Collections;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float Damage = 10;
    public float moveSpeed = 10;

    private Rigidbody rb;
    private Player player;
    private Vector3 direction;

    

    void Start()
    {
        StartCoroutine(Despawn());
        rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<Player>();

        direction = (player.transform.position + Vector3.up) - transform.position;
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
            player.Health -= Damage;
            Destroy(gameObject);
        }
    }
}
