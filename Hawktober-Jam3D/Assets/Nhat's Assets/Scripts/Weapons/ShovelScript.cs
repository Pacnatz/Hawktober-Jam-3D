using UnityEngine;

public class ShovelScript : WeaponScript
{

    [HideInInspector]
    public bool FreezeCamera = false; //Called from animation
    [HideInInspector]
    public bool isDiggable;
    [HideInInspector]
    public GameObject selectedGraveDirt;

    [SerializeField]
    private PlayerMove playerScript;

    private Animator anim;

    
    private void Start()
    {
        anim = GetComponent<Animator>();    
    }
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //Dig
        if (Input.GetMouseButtonDown(1))
        {
            if (isDiggable)
            {
                anim.Play("Dig");
                
            }
        }
        //Freeze Camera and MovementSpeed if digging
        if (FreezeCamera)
        {
            playerScript.cameraSensitivity = 80;
            playerScript.movementSpeed = 1;
        }
        else
        {
            playerScript.cameraSensitivity = 500f;
            playerScript.movementSpeed = 5f;
        }
    }

    public override void Activate() //Inherited from weaponscript
    {
        anim.Play("Draw");
    }

    public override void DeActivate()
    {
        anim.Play("Dock");
    }


}
