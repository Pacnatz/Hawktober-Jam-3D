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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = target.position - barrel.position;
            direction.Normalize();

            //Randomize Direction Should update if plan on making a scope gun
            Vector3 offset = new Vector3(Random.Range(-0.06f, .04f), Random.Range(-0.04f, 0.06f), 0);
            direction += offset;

            GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

            
        }
    }


}
