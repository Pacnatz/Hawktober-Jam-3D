using UnityEngine;

public class ShotgunAmmoBox : AmmoBox
{

    ShotgunScript shotgunScript;

    private const int MAX_SHOTGUN_AMMO = 96;

    private void Start()
    {
        shotgunScript = Camera.main.transform.Find("ShotGun").GetComponent<ShotgunScript>();
        endPos = transform.position + new Vector3(0, 1.2f, 0);
        particles = transform.Find("Particle System").GetComponent<ParticleSystem>();
    }


    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!floatingUp && shotgunScript.holdingAmmo < MAX_SHOTGUN_AMMO)
            {
                shotgunScript.holdingAmmo += 12;
                shotgunScript.holdingAmmo = Mathf.Clamp(shotgunScript.holdingAmmo, 0, MAX_SHOTGUN_AMMO);
                Destroy(gameObject);
            }
        }
    }
}
