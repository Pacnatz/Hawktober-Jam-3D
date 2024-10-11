using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public Transform leftForeArm;
    public GameObject BonePrefab;


    public void ThrowBone()
    {
        Instantiate(BonePrefab, leftForeArm.position, leftForeArm.rotation);
    }
}
