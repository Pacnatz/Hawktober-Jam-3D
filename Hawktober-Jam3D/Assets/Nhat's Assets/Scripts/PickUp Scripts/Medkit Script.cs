using UnityEngine;

public class MedkitScript : AmmoBox
{
    private Player playerScript;



    private void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        endPos = transform.position + new Vector3(0, 1.2f, 0);
        particles = transform.Find("Particle System").GetComponent<ParticleSystem>();
        particles.Play();
    }


    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!floatingUp)
            {
                playerScript.Health += 10;
                Destroy(gameObject);
            }
            
        }
    }
}
