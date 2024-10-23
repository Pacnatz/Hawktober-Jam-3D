using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{ 
    public float BulletKillTime = 1.2f;
    private float damage = 30f;

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
            other.TryGetComponent<Skeleton>(out var skeleScript);
            other.TryGetComponent<Pumpkin>(out var pumpkinScript);

            if (skeleScript)
            {
                skeleScript.Health -= damage;
                Destroy(gameObject);
            }
            if (pumpkinScript)
            {
                pumpkinScript.Health -= damage;
                Destroy(gameObject);
            }


        }
    }

}
