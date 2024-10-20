using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
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

    [HideInInspector]
    public int ammoArrayPosition;

    //Wave Container
    public RectTransform waveContainer;
    private TMP_Text waveTMPro;
    public MonsterSpawner spawnScript;
    private Vector3 waveVelocity = Vector3.zero;

    [HideInInspector]
    public bool ShowWave = false;
    private float waveShowTime = 2f;
    private float timer = 2f;

    

    //Menu variables
    public GameObject PauseMenu;
    [HideInInspector]
    public bool isPaused = false;
    public GameObject SettingsMenu;
    public Slider sensitivitySlider;
    public Slider audioSlider;

    //GameOver variables;
    public GameObject gameOverContainer;


    private PlayerMove playerMove;
    private Player player;

    public Slider playerHealthSlider;
    public TMP_Text playerHealthTMP;

    private SwitchWeapons switchWeaponsScript;

    void Start()
    {

        gunTMPro = gunContainer.GetChild(0).GetComponent<TMP_Text>();
        shotgunTMPro = shotgunContainer.GetChild(0).GetComponent<TMP_Text>();
        waveTMPro = waveContainer.GetChild(0).GetComponent<TMP_Text>();
        playerMove = FindAnyObjectByType<PlayerMove>();
        player = FindAnyObjectByType<Player>();

        switchWeaponsScript = FindAnyObjectByType<SwitchWeapons>();
        ammoArrayPosition = 2;


        isPaused = false;
        PauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    void Update()
    {
        //Ammo text smooth damp
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
        //Show wave smooth damp
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

        


        //Text values
        gunTMPro.text = $"{gunScript.currentAmmo}/{gunScript.holdingAmmo}";
        shotgunTMPro.text = $"{shotgunScript.currentAmmo}/{shotgunScript.holdingAmmo}";
        waveTMPro.text = $"WAVE {spawnScript.wave}";

        playerHealthSlider.value = player.Health / 100;
        playerHealthTMP.text = $"HEALTH: {player.Health}";
        

        //Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                PauseMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
        }
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

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        gameOverContainer.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void ResumeGame()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void ShowSettings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void SensitivitySlider()
    {
        playerMove.cameraSensitivity = sensitivitySlider.value;
    }

    public void VolumeSlider()
    {
        switchWeaponsScript.AudioVolume = audioSlider.value;
    }

    public void HideSettings()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
    
}
