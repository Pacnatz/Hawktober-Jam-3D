using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{ 
    public float BulletKillTime = 1.2f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, BulletKillTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor")) //If collision is in Floor layer
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {

            Skeleton skeleScript = other.gameObject.GetComponent<Skeleton>();

            skeleScript.Health -= 30f;
            Destroy(gameObject);
        }
    }

}
