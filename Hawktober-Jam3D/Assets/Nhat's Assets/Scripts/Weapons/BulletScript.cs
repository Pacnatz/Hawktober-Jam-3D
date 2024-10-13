using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{ 
    public float BulletKillTime = 1f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    { 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor")) //If collision is in Floor layer wwwwwww
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {

            Skeleton skeleScript = other.gameObject.GetComponent<Skeleton>();

            skeleScript.Health -= 30f;

            Debug.Log("hit " + skeleScript.gameObject.name);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(BulletKillTime);
        Destroy(gameObject);
    }
}
