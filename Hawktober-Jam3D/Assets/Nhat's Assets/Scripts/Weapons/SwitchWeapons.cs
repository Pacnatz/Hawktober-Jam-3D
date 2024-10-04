using System.Collections;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;
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

        /*
        //Toggle scopedIn for gun off (could code this better)
        if (oldWeapon == weapons[1])
        {
            oldWeapon.GetComponent<GunScript>().scopedIn = false;
        }
        */

        yield return new WaitForSeconds(DockTime); //How long the animation takes
        currentWeapon = newWeapon;
        StartCoroutine(DrawWeapon(newWeapon));
    }

    private void UnFreezePlayer() //Unfreezes player when digging
    {
        weapons[0].GetComponent<ShovelScript>().freezeCamera = false;
    }
}