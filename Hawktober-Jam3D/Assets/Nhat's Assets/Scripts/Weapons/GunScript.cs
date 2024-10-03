using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunScript : WeaponScript
{

    public float bulletSpeed = 20f;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private PlayerMove playerScript;

    private bool scopedIn;
    private Animator anim;
    private ParticleSystem particles;
    private GameObject gunLight;


    private void Start()
    {
        anim = mainCamera.GetComponent<Animator>();
        particles = barrel.GetChild(0).GetComponent<ParticleSystem>();
        gunLight = barrel.GetChild(1).gameObject;
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //Left Mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = target.position - barrel.position;
            direction.Normalize();

            Vector3 offset = GenerateOffSet();
            direction += offset;

            GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

            //Shoot animations
            if (scopedIn)
            {
                anim.Play("ScopedInFire");
            }
            else
            {
                anim.Play("ScopeOutFire");
            }
            //Fire Lights and Particles
            particles.Play();
            gunLight.SetActive(true);
            StartCoroutine(TurnOffLight(.1f));

            
        }
        //Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Scope(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Scope(false);
        }
    }

    private void Scope(bool value)
    {
        if (value)
        {
            scopedIn = true;
            anim.Play("ScopedIn");
            //Scope in movespeed
            playerScript.movementSpeed = 3.5f;
        }
        else
        {
            scopedIn = false;
            anim.Play("ScopeOut");
            playerScript.movementSpeed = 5f;
        }
    }

    private Vector3 GenerateOffSet()
    {
        if (scopedIn)
        {
            return Vector3.zero;
        }
        else
        {
            return new Vector3(Random.Range(-0.06f, .04f), Random.Range(-0.04f, 0.06f), 0);
        }
    }

    private IEnumerator TurnOffLight(float offTime)
    {
        yield return new WaitForSeconds(offTime);
        gunLight.SetActive(false);
    }

    public override void Activate()
    {
        anim.Play("Draw");
    }
    public override void DeActivate()
    {
        anim.Play("Dock");
    }
}
