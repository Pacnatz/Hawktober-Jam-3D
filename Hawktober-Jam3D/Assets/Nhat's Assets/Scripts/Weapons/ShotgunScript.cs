using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class ShotgunScript : WeaponScript
{
    //Damage 20;

    public float bulletSpeed = 40f;
    private bool canFire = true;

    [SerializeField]
    private LayerMask monsterLayer;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private PlayerMove playerScript;
    private int bulletAmount = 10;

    [HideInInspector]
    public bool scopedIn = false;
    [HideInInspector]
    public float zoomPercent = 60;
    [SerializeField]
    private CinemachineCamera mainCamera;
    private Animator anim;
    [HideInInspector]
    public bool animToggle = true;      //Called from animation player
    [HideInInspector]                   
    public bool IsReloading = false;    //Called from animation player
    private ParticleSystem particles;
    private GameObject gunLight;

    //Ammo variables
    public int currentAmmo;
    public int holdingAmmo;
    private int maxAmmo;

    private UIScript uiScript;

    private AudioSource shootAudio;
    private AudioSource pumpAudio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        particles = barrel.GetChild(0).GetComponent<ParticleSystem>();
        gunLight = barrel.GetChild(1).gameObject;
        uiScript = FindAnyObjectByType<UIScript>();

        shootAudio = transform.Find("ShootSound").GetComponent<AudioSource>();
        pumpAudio = transform.Find("PumpSound").GetComponent<AudioSource>();

        currentAmmo = 5;
        holdingAmmo = 18;

    }

    void Update()
    {
        GetInput();


        mainCamera.Lens.FieldOfView = zoomPercent; //Currently broken, need to unparent Third Person Aim Camera from Main Camera object
    }

    private void GetInput()
    {
        //Left Mouse button
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && animToggle && canFire && !uiScript.isPaused)
        {
            currentAmmo--;
            canFire = false;

            shootAudio.pitch = Random.Range(.9f, 1.1f);
            shootAudio.Play();

            for (int i = 0; i < bulletAmount; i++)
            {
                Vector3 direction;
                if (scopedIn)
                {
                    direction = target.position - barrel.position;
                }
                else
                {
                    if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit monster, 500f, monsterLayer))
                    {
                        direction = new Vector3(monster.point.x, monster.point.y, monster.point.z) - barrel.position;
                    }
                    else
                    {
                        direction = target.position - barrel.position;
                    }
                }
                direction.Normalize();

                Vector3 offset = GenerateOffSet();
                direction += offset;

                GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
            }
            
            //Shoot animations
            if (scopedIn && !IsReloading)
            {
                anim.Play("ScopedInFire");
                if (currentAmmo != 0)
                {
                    StartCoroutine(WaitForPump());
                }
            }
            else
            {
                anim.Play("ScopeOutFire");
                if (currentAmmo != 0)
                {
                    StartCoroutine(WaitForPump());
                }
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
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopeOutFire") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("PumpOut") && animToggle)
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
                anim.GetCurrentAnimatorStateInfo(0).IsName("ScopedInFire") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("PumpIn") && animToggle) //If either scope or scopedIn is playing
            {
                scopedIn = false;
                anim.Play("ScopeOut");
                playerScript.movementSpeed = 6f;
            }
        }
        //Reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 5 && holdingAmmo > 0 && !IsReloading)
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
        if (holdingAmmo > 0)
        {
            currentAmmo++;
            holdingAmmo--;
            if (holdingAmmo <= 0)
            {
                anim.SetTrigger("ReloadingDone");
            }
        }
        if (currentAmmo >= 5)
        {
            anim.SetTrigger("ReloadingDone");
        }
        scopedIn = false;
        canFire = true;

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
            return new Vector3(Random.Range(-0.04f, .04f), Random.Range(-0.04f, 0.04f), Random.Range(-0.04f, 0.04f));
        }
        else
        {
            return new Vector3(Random.Range(-0.08f, .08f), Random.Range(-0.08f, 0.08f), Random.Range(-0.08f, 0.08f));
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

    public void ZoomOut()
    {
        mainCamera.Lens.FieldOfView = 60;
    }
    public void ZoomIn()
    {
        mainCamera.Lens.FieldOfView = 50;
    }

    private IEnumerator WaitForPump() //Delay before next shot
    {
        yield return new WaitForSeconds(.25f);
        PlayReload();
        yield return new WaitForSeconds(.15f);
        canFire = true;
    }
    public void PlayReload()
    {
        pumpAudio.pitch = Random.Range(.9f, 1.1f);
        pumpAudio.Play();
    }

}
