using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject skeletonPrefab;


    private float timer = 2f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 10f;

            GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)];

            GameObject skele = Instantiate(skeletonPrefab, spawnLoc.transform.localPosition, spawnLoc.transform.localRotation);
        }
    }


}
