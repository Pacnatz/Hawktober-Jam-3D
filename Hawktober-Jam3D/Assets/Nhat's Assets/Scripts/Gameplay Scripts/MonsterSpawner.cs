using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public WaveManager WaveManagerScript;
    public GameObject[] spawnLocations;
    public GameObject skeletonPrefab;
    [HideInInspector]
    public bool canSpawn = false;
    [HideInInspector]
    public float spawnDelay = 10f;
    private float timer = 2f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        canSpawn = true;
        timer = spawnDelay;

        GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)];

        if (WaveManagerScript.currentWave.Count > 0)
        {
            GameObject monsterToSpawn = WaveManagerScript.currentWave[0];
            WaveManagerScript.currentWave.Remove(monsterToSpawn);

            GameObject skele = Instantiate(monsterToSpawn, spawnLoc.transform.localPosition, spawnLoc.transform.localRotation);
        }
    }


}
