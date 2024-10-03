using UnityEngine;

public class GraveDirtScript : MonoBehaviour
{
    [HideInInspector]
    public bool PlayerOnTop = false;
    public LayerMask graveDirtLayer;
    private Transform mainCamera;
    private ShovelScript shovelScript;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        shovelScript = mainCamera.Find("Shovel").GetComponent<ShovelScript>();
    }

    private void Update()
    {
        if (PlayerOnTop)
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, 2.5f, graveDirtLayer))
            {
                shovelScript.isDiggable = true;
                shovelScript.selectedGraveDirt = gameObject;
            }
        }
        else
        {
            shovelScript.isDiggable = false;
        }
        
    }

    public void PlayAnimation(int frame)
    {

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
