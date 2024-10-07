using System;
using System.Collections;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private UIScript uiScript;

    public float DockTime = .25f;

    private GameObject currentWeapon;
    private bool isSwitchingWeapon = false;

    private void Start()
    {
        currentWeapon = weapons[0]; //Start with shovel
        StartCoroutine(DrawWeapon(currentWeapon));

        //Safe mode sets all weapons active
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(true);
        }

    }

    private void Update()
    {
        GetInput();

        
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != weapons[0] && !isSwitchingWeapon) //Make sure not to switch out weapon with same weapon
        {
            StartCoroutine(DockWeapon(currentWeapon, weapons[0]));
            UnFreezePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != weapons[1] && !isSwitchingWeapon) 
        {
            StartCoroutine(DockWeapon(currentWeapon, weapons[1]));
            UnFreezePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeapon != weapons[2] && !isSwitchingWeapon)
        {
            StartCoroutine(DockWeapon(currentWeapon, weapons[2]));
            UnFreezePlayer();
        }
    }

    private IEnumerator DrawWeapon(GameObject weapon)
    {
        yield return new WaitForSeconds(.1f); //Idk why but this delay is needed
        weapon.GetComponent<WeaponScript>().Activate();

        yield return new WaitForSeconds(DockTime);
        isSwitchingWeapon = false;
    }

    private IEnumerator DockWeapon(GameObject oldWeapon, GameObject newWeapon)
    {
        isSwitchingWeapon = true;
        oldWeapon.GetComponent<WeaponScript>().DeActivate();

        uiScript.ammoArrayPosition = Array.IndexOf(weapons, newWeapon); //Pull current weapon ammo card out
        yield return new WaitForSeconds(DockTime); //How long the animation takes
        currentWeapon = newWeapon;
        StartCoroutine(DrawWeapon(newWeapon));
    }

    private void UnFreezePlayer() //Unfreezes player when digging
    {
        weapons[0].GetComponent<ShovelScript>().freezeCamera = false;
    }







    //****Temporary Fix... Called from animation event for gun reload functions**** Unable to call gunscript events from mainCamera animationplayer
    //TO FIX: Move animator from mainCamera to M1911 Object, fix all animations..
    public void SlowPlayer()
    {
        weapons[1].GetComponent<GunScript>().SlowPlayer();
    }
    public void UnSlowPlayer()
    {
        weapons[1].GetComponent<GunScript>().UnSlowPlayer();
    }
    public void UpdateAmmo()
    {
        weapons[1].GetComponent<GunScript>().UpdateAmmo();
    }
    public void ScopedIn()
    {
        weapons[1].GetComponent<GunScript>().ScopeIn();
    }
}