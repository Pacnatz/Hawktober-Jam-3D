using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public ShovelScript shovel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            shovel.selectedMonster = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            shovel.selectedMonster = null;
        }
    }
}
