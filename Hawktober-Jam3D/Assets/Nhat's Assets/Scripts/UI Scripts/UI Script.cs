using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{

    private Vector3 activeContainerPosition = new Vector3(200, 150, 0);
    private Vector3 inactiveContainerPosition = new Vector3(-200, 150, 0);
    private float containerMoveSpeed = 100f;

    //M1911 Container
    public RectTransform gunContainer;
    private TMP_Text gunTMPro;
    public GunScript gunScript;
    private Vector3 gunVelocity = Vector3.zero;

    //Shotgun Container
    public RectTransform shotgunContainer;
    private TMP_Text shotgunTMPro;
    public ShotgunScript shotgunScript;
    private Vector3 shotgunVelocity = Vector3.zero;

    public int ammoArrayPosition = 0;

    void Start()
    {
        gunContainer = gunContainer.GetComponent<RectTransform>();
        gunTMPro = gunContainer.GetChild(0).GetComponent<TMP_Text>();
        shotgunTMPro = shotgunContainer.GetChild(0).GetComponent<TMP_Text>();
    }
    void Update()
    {
        switch (ammoArrayPosition)
        {
            case 0:
                HideGunContainer();
                HideShotGunContainer();
                break;
            case 1:
                gunContainer.anchoredPosition3D = Vector3.SmoothDamp(gunContainer.anchoredPosition3D,
                    activeContainerPosition, ref gunVelocity, containerMoveSpeed * Time.deltaTime);
                HideShotGunContainer();
                break;
            case 2:
                shotgunContainer.anchoredPosition3D = Vector3.SmoothDamp(shotgunContainer.anchoredPosition3D,
                    activeContainerPosition, ref shotgunVelocity, containerMoveSpeed * Time.deltaTime);
                HideGunContainer();
                break;
        }


        gunTMPro.text = $"{gunScript.currentAmmo}/{gunScript.holdingAmmo}";
        shotgunTMPro.text = $"{shotgunScript.currentAmmo}/{shotgunScript.holdingAmmo}";
    }

    private void HideGunContainer()
    {
        gunContainer.anchoredPosition3D = Vector3.SmoothDamp(gunContainer.anchoredPosition3D,
                    inactiveContainerPosition, ref gunVelocity, containerMoveSpeed * Time.deltaTime);
    }
    private void HideShotGunContainer()
    {
        shotgunContainer.anchoredPosition3D = Vector3.SmoothDamp(shotgunContainer.anchoredPosition3D,
                    inactiveContainerPosition, ref shotgunVelocity, containerMoveSpeed * Time.deltaTime);
    }
}
