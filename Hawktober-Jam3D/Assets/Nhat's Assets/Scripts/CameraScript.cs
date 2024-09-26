using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private PlayerMove playerScript;
    [SerializeField]
    private float cameraSensitivity = 500f;

    private bool startCamera;

    private float rotationX;

    void Start()
    {
        StartCoroutine(StartCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (startCamera)
        {
            //Rotate camera object by rotationX & clamp rotationX to prevent issue with flipping screen
            rotationX -= Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -80, 80);

            transform.rotation = Quaternion.Euler(rotationX, playerScript.rotationY, 0);
        }
    }

    IEnumerator StartCamera()
    {
        yield return new WaitForSeconds(.5f);
        startCamera = true;
    }
}
