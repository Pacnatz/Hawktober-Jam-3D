using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float Health;

    private Transform mainCamera;
    private Rigidbody cameraRb;

    private CameraScript cameraScript;
    private PlayerMove playerMove;

    public Volume postProcess;
    private Vignette vignette;
    private ColorAdjustments colorAdj;

    private UIScript uiScript;

    private bool playerDead = false;

    private void Start()
    {
        Health = 100;
        mainCamera = transform.Find("Camera");

        cameraScript = FindAnyObjectByType<CameraScript>();
        playerMove = FindAnyObjectByType<PlayerMove>();

        postProcess.profile.TryGet(out vignette);
        postProcess.profile.TryGet(out colorAdj);

        uiScript = FindAnyObjectByType<UIScript>();
    }

    private void Update()
    {
        Health = Mathf.Clamp(Health, 0, 100);
        if (Health <= 0)
        {
            if (!playerDead)
            {
                //For some reason, need to hardcode the animation here
                cameraScript.enabled = false;
                playerMove.enabled = false;
                cameraRb = mainCamera.gameObject.AddComponent<Rigidbody>();
                cameraRb.useGravity = true;
                cameraRb.AddTorque(transform.right * Random.Range(-2f, -.5f));
                cameraRb.AddTorque(transform.up * Random.Range(-1f, 1f));
            }
            playerDead = true;
            Die();
        }
    }

    private void Die()
    {

        
        if (mainCamera.localPosition.y > .51f)
        {
            playerMove.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        else
        {
            

            colorAdj.colorFilter.Override(new Color(255, 160, 160)); //Supposed to change to red but this looks cooler
            uiScript.GameOver();
        }
    }
}
