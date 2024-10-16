using UnityEngine;

public class ShovelScript : WeaponScript
{
    [SerializeField]
    private PlayerMove playerScript;
    [SerializeField]
    private LayerMask graveDirtLayer;

    [HideInInspector]
    public bool animToggle = true;    //Toggled by animator
    [HideInInspector]
    public bool freezeCamera = false; //Toggled by animator / Turned off by SwitchWeapons script
    
    private GraveDirtScript selectedGraveDirt;

    private Animator anim;
    private Transform mainCamera;

    private bool isDiggable;

    [HideInInspector]
    public GameObject selectedMonster;
    private float meleeDamage = 20;

    private UIScript uiScript;
    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
        uiScript = FindAnyObjectByType<UIScript>();
    }
    private void Update()
    {
        GetInput();
        CheckRayAtGrave();
    }
    private void GetInput()
    {
        //Hit
        if (Input.GetMouseButtonDown(0))
        {
            if (animToggle)
            {
                anim.Play("Hit");
            }
        }
        //Dig
        if (Input.GetMouseButtonDown(1))
        {
            if (isDiggable)
            {
                if (animToggle)
                {
                    anim.Play("Dig"); //Calls FreezeCamera
                }
            }
        }
        //Freeze Camera and MovementSpeed if digging
        if (freezeCamera)
        {
            playerScript.cameraSensitivity = 80;
            playerScript.movementSpeed = 1;
        }
        else
        {
            playerScript.cameraSensitivity = uiScript.sensitivitySlider.value; //Resets it back to adjusted sensitivity
            playerScript.movementSpeed = 6f;
        }
    }

    private void CheckRayAtGrave()
    {
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit graveDirt, 3.5f, graveDirtLayer)) //Shoots raycast from camera to graveyard dirt
        {
            selectedGraveDirt = graveDirt.collider.gameObject.GetComponent<GraveDirtScript>();
            if (selectedGraveDirt.GraveDiggable && selectedGraveDirt.PlayerOnTop) //if grave is diggable and player is on top
            {
                isDiggable = true;
            }
            else isDiggable = false;
        }
        else isDiggable = false;
    }
    public void PlayDirtAnimation() //Called from event from animator
    {
        selectedGraveDirt.PlayAnimation();
    }

    public void HitEnemy()
    {
        if (selectedMonster != null)
        {
            selectedMonster.TryGetComponent<Skeleton>(out var skeleScript);
            if (skeleScript)
            {
                skeleScript.Health -= meleeDamage;
            }
        }
        
    }

    public override void Activate() //Inherited from weaponscript
    {
        anim.Play("Draw");
    }

    public override void DeActivate() //Inherited from weaponscript
    {
        anim.Play("Dock");
    }
}
