using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float Health;

    private Transform mainCamera;

    private CameraScript cameraScript;
    private PlayerMove playerMove;

    private void Start()
    {
        Health = 100;
        mainCamera = transform.Find("Camera");

        cameraScript = FindAnyObjectByType<CameraScript>();
        playerMove = FindAnyObjectByType<PlayerMove>();
    }

    private void Update()
    {
        Debug.Log(mainCamera.localPosition);
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //For some reason, need to hardcode the animation here
        if (mainCamera.localPosition.z > -.3f && mainCamera.localPosition.y > 1.5f)
        {
            cameraScript.enabled = false;
            playerMove.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            playerMove.enabled = false;
            mainCamera.transform.localPosition += new Vector3(0, -1, -1) * Time.deltaTime;
            mainCamera.transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
        }
    }
}
