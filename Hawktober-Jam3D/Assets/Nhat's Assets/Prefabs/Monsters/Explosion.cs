using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Light explosionLight;
    private float diminishRate = 60f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        explosionLight.intensity -= diminishRate * Time.deltaTime;
        if (explosionLight.intensity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
