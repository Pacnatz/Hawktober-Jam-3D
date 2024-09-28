using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunScript : MonoBehaviour
{

    public float bulletSpeed = 10f;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private GameObject bulletPrefab;

    private float bulletKillTime = 1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = target.position - barrel.position;
            direction.Normalize();

            //Randomize Direction;
            Vector3 offset = new Vector3(Random.Range(-0.06f, .04f), Random.Range(-0.04f, 0.06f), 0);
            direction += offset;

            GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

            StartCoroutine(DestroyBullet(bullet));
        }
    }

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletKillTime);
        Destroy(bullet);
    }
}
