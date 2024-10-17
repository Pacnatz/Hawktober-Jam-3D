using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector]
    public bool CameraLock = false;
    [SerializeField]
    private PlayerMove playerScript;

    private bool startCamera;

    private float rotationX;

    void Start()
    {
        StartCoroutine(StartCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (startCamera && !CameraLock)
        {
            Debug.Log("Should be working?");
            //Rotate camera object by rotationX & clamp rotationX to prevent issue with flipping screen
            rotationX -= Input.GetAxis("Mouse Y") * playerScript.cameraSensitivity * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -80, 80);

            transform.rotation = Quaternion.Euler(rotationX, playerScript.rotationY, 0); //rotationY is player's rotation
        }
    }

    IEnumerator StartCamera()
    {
        yield return new WaitForSeconds(.5f);
        startCamera = true;
    }
}
