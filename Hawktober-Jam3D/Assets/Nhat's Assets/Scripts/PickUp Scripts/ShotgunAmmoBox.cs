using UnityEngine;

public class ShotgunAmmoBox : AmmoBox
{

    ShotgunScript shotgunScript;

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
            if (!floatingUp)
            {
                shotgunScript.holdingAmmo += 12;
                Destroy(gameObject);
            }
        }
    }
}
