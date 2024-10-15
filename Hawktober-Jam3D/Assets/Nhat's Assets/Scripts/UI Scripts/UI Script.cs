using UnityEngine;
using TMPro;
using System.Collections;

public class UIScript : MonoBehaviour
{
    

    private Vector3 activeContainerPosition = new Vector3(200, 150, 0);
    private Vector3 inactiveContainerPosition = new Vector3(-300, 150, 0);
    private float containerMoveSpeed = 40f;

    private Vector3 waveActiveContainerPosition = new Vector3(0, -100, 0);
    private Vector3 waveInActiveContainerPosition = new Vector3(0, 200, 0);

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

    //Wave Container
    public RectTransform waveContainer;
    private TMP_Text waveTMPro;
    public MonsterSpawner spawnScript;
    private Vector3 waveVelocity = Vector3.zero;

    [HideInInspector]
    public bool ShowWave = false;
    private float waveShowTime = 2f;
    private float timer = 2f;
    



    [HideInInspector]
    public int ammoArrayPosition;

    void Start()
    {
        gunTMPro = gunContainer.GetChild(0).GetComponent<TMP_Text>();
        shotgunTMPro = shotgunContainer.GetChild(0).GetComponent<TMP_Text>();
        waveTMPro = waveContainer.GetChild(0).GetComponent<TMP_Text>();
        ammoArrayPosition = 2;
    }
    void Update()
    {
        switch (ammoArrayPosition)
        {
            case 0:
                shotgunContainer.anchoredPosition3D = Vector3.SmoothDamp(shotgunContainer.anchoredPosition3D,
                    activeContainerPosition, ref shotgunVelocity, containerMoveSpeed * Time.deltaTime);
                HideGunContainer();
                break;
                
            case 1:
                gunContainer.anchoredPosition3D = Vector3.SmoothDamp(gunContainer.anchoredPosition3D,
                    activeContainerPosition, ref gunVelocity, containerMoveSpeed * Time.deltaTime);
                HideShotGunContainer();
                break;
            case 2:
                HideGunContainer();
                HideShotGunContainer();
                break;
        }

        if (ShowWave)
        {
            waveContainer.anchoredPosition3D = Vector3.SmoothDamp(waveContainer.anchoredPosition3D,
                    waveActiveContainerPosition, ref waveVelocity, containerMoveSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = waveShowTime;
                ShowWave = false;
            }
        }
        else
        {
            waveContainer.anchoredPosition3D = Vector3.SmoothDamp(waveContainer.anchoredPosition3D,
                    waveInActiveContainerPosition, ref waveVelocity, containerMoveSpeed * Time.deltaTime);
        }

        



        gunTMPro.text = $"{gunScript.currentAmmo}/{gunScript.holdingAmmo}";
        shotgunTMPro.text = $"{shotgunScript.currentAmmo}/{shotgunScript.holdingAmmo}";
        waveTMPro.text = $"WAVE {spawnScript.wave}";
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
