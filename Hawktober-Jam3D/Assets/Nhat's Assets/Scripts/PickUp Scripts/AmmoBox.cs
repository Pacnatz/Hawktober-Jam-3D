using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 180;
    [SerializeField]
    private float movementSpeed = 1.2f;

    private GunScript gunScript;

    protected Vector3 endPos;
    protected bool floatingUp = true;
    protected ParticleSystem particles;

    private const int MAX_M1911_AMMO = 192;

    void Start()
    {
        endPos = transform.position + new Vector3(0, 1.2f, 0);
        particles = transform.Find("Particle System").GetComponent<ParticleSystem>();
        gunScript = Camera.main.transform.Find("M1911").GetComponent<GunScript>();
        particles.Play();
    }

    protected virtual void Update()
    {
        if (floatingUp)
        {
            transform.position += new Vector3(0, movementSpeed * Time.deltaTime, 0);
            transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
            if (transform.position.y >= endPos.y)
            {
                floatingUp = false;
                
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!floatingUp && gunScript.holdingAmmo < MAX_M1911_AMMO)
            {
                //Add code for ammo update here.
                gunScript.holdingAmmo += 24;
                gunScript.holdingAmmo = Mathf.Clamp(gunScript.holdingAmmo, 0, MAX_M1911_AMMO);
                Destroy(gameObject);
            }
        }
        Instantiate(gunScript, Vector3.zero.normalized, Quaternion.identity);
    }
}
