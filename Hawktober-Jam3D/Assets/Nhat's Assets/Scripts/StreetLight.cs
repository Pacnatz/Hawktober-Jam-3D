using System.Collections;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    [SerializeField]
    private GameObject light1;
    [SerializeField]
    private GameObject light2;
    [SerializeField]
    private Renderer lightMesh;
    private Material lightMat;

    private float countDown = 0;

    private void Start()
    {
        lightMat = lightMesh.material;

    }

    private void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            countDown = Random.Range(0, 20);
            int randomNumber = Random.Range(1, 50);
            if (randomNumber == 1)
            {
                light1.SetActive(false);
                light2.SetActive(false);
                lightMat.DisableKeyword("_EMISSION");
                StartCoroutine(FlickerLight(Random.Range(0.0f, 1.0f)));
            }
        }
    }

    private IEnumerator FlickerLight(float time)
    {
        yield return new WaitForSeconds(time);
        light1.SetActive(true);
        light2.SetActive(true);
        lightMat.EnableKeyword("_EMISSION");
    }
}
