using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public Transform leftForeArm;
    public GameObject BonePrefab;

    private float throwSpeed = 1f;

    
    private float boneSpeed = 40;
    private float boneDamage = 5f;

    private Animator topAnim;

    private void Start()
    {
        topAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (topAnim)
        {
            topAnim.SetFloat("ThrowSpeed", throwSpeed);
        }
        
    }

    public void ThrowBone()
    {
        GameObject bone = Instantiate(BonePrefab, leftForeArm.position, leftForeArm.rotation);
        bone.GetComponent<BoneProjectile>().moveSpeed = boneSpeed;
        bone.GetComponent<BoneProjectile>().Damage = boneDamage;
    }

    public void SetThrowSpeed(float multiplier)
    {
        throwSpeed = multiplier;
    }
    public void SetBoneSpeed(float value)
    {
        boneSpeed = value;
    }
    public void SetBoneDamage(float value)
    {
        boneDamage = value;
    }
}
