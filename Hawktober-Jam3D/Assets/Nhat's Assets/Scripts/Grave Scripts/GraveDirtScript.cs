using UnityEngine;

public class GraveDirtScript : MonoBehaviour
{
    [HideInInspector]
    public bool GraveDiggable = true;  //Toggled by animation in phase 4
    [HideInInspector]
    public bool PlayerOnTop = false;

    [SerializeField]
    private GameObject gunAmmoBoxPrefab;
    [SerializeField]
    private GameObject shotgunAmmoBoxPrefab;
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
                anim.Play("Phase3");
                GraveDiggable = false; //Stops shovel to continue digging
                break;
            default:
                animationFrame = 0;
                anim.Play("Start");
                break;
        }
    }
    public void ResetAnimation() //Called from animatino player
    {
        animationFrame = 3;
        GraveDiggable = true;
        PlayAnimation();
    }
    public void SpawnAmmo() //Called from animation player
    {
        int choice = Random.Range(1, 3);
        Debug.Log(choice);
        switch (choice)
        {
            case 1:
                Instantiate(gunAmmoBoxPrefab, ammoSpawnPos.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(shotgunAmmoBoxPrefab, ammoSpawnPos.position, Quaternion.identity);
                break;
        }
        
    }


}
