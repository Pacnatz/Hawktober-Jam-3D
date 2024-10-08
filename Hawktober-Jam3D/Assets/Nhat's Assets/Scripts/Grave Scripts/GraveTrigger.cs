using UnityEngine;

public class GraveTrigger : MonoBehaviour
{
    private GraveDirtScript graveDirt;

    void Start()
    {
        graveDirt = transform.parent.GetComponent<GraveDirtScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            graveDirt.PlayerOnTop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            graveDirt.PlayerOnTop = false;
        }
    }
}
