using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunScript : WeaponScript
{
    //Damage 30

    public float bulletSpeed = 40f;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform closeTarget;
    [HideInInspector]
    public bool IsCloseToMonster;

    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private PlayerMove playerScript;

    [HideInInspector]
    public bool scopedIn = false;
    private Animator anim;
    [HideInInspector]
    public bool animToggle = true; 
    private ParticleSystem particles;
    private GameObject gunLight;

    //Ammo variables
    [HideInInspector]
    public int currentAmmo = 8;
    [HideInInspector]
    public int holdingAmmo = 30;
    private int maxAmmo;

    private UIScript uiScript;

    private void Start()
    {
        anim = mainCamera.GetComponent<Animator>();
        particles = barrel.GetChild(0).GetComponent<ParticleSystem>();
        gunLight = barrel.GetChild(1).gameObject;
        uiScript = FindAnyObjectByType<UIScript>();
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //Left Mouse button
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && animToggle)
        {
            currentAmmo--;


            Vector3 direction;
            if (scopedIn)
            {
                direction = target.position - barrel.position;
            }
            else
            {
                if (IsCloseToMonster) direction = closeTarget.position - barrel.position;
                else direction = target.position - barrel.position;
            }
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
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("End") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopeOut") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopeOutFire") && animToggle)
            {
                scopedIn = true;
                anim.Play("ScopedIn");
                //Scope in movespeed
                playerScript.movementSpeed = 3.5f;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Scope") || 
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopedIn") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopedInFire") && animToggle) //If either scope or scopedIn is playing
            {
                scopedIn = false;
                anim.Play("ScopeOut");
                playerScript.movementSpeed = 6f;
            }
        }
        //Reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 8 && holdingAmmo > 0)
        {
            anim.Play("Reload");
        }

    }

    //Called from reload animation events
    public void SlowPlayer()
    {
        playerScript.movementSpeed = 3.5f;
    }
    public void UnSlowPlayer()
    {
        playerScript.movementSpeed = 6f;
    }
    public void UpdateAmmo()
    {
        if (holdingAmmo >= 8)
        {
            holdingAmmo -= 8 - currentAmmo;
            currentAmmo = 8;
        }
        else
        {
            currentAmmo = holdingAmmo;
            holdingAmmo = 0;
        }
    }
    public void ScopeIn() //Called from switch weapons
    {
        if (Input.GetMouseButton(1))
        {
            anim.Play("ScopedIn");
            scopedIn = true;
            playerScript.movementSpeed = 3.5f;
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
            return new Vector3(Random.Range(-0.02f, .02f), Random.Range(-0.02f, 0.02f), 0);
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
