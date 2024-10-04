using UnityEngine;

public class GraveDirtScript : MonoBehaviour
{
    [HideInInspector]
    public bool GraveDiggable = true;  //Toggled by animation in phase 4
    [HideInInspector]
    public bool PlayerOnTop = false;

    [SerializeField]
    private GameObject ammoBoxPrefab;
    [SerializeField]
    private Transform ammoSpawnPos;

    private int animationFrame = 0;
    private Animator anim;
    
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }
    public void PlayAnimation()
    {
        switch (animationFrame)
        {
            case 0:
                animationFrame++;
                anim.Play("Phase1");
                break;
            case 1:
                animationFrame++;
                anim.Play("Phase2");
                break;
            case 2:
                animationFrame++;
                anim.Play("Phase3");
                break;
            case 3:
                anim.Play("Phase4");
                GraveDiggable = false; //Stops shovel to continue digging
                break;
            default:
                animationFrame = 0;
                anim.Play("Start");
                break;
        }
    }
    public void ResetAnimation()
    {
        animationFrame = 4;
        GraveDiggable = true;
        PlayAnimation();
    }
    public void SpawnAmmo()
    {
        GameObject ammoBox = Instantiate(ammoBoxPrefab, ammoSpawnPos.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerOnTop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerOnTop = false;
        }
    }
}
