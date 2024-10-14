using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaveManager : MonoBehaviour
{
    public MonsterSpawner MonsterSpawnScript;
    public GameObject SkeletonPrefab;


    private List<GameObject> wave1;
    private List<GameObject> wave2;
    public List<GameObject> currentWave;
    private int wave = 1;

    private bool startWave = true;
    private void Start()
    {
        Debug.Log("Starting Wave 1");
        wave1 = new List<GameObject>() { SkeletonPrefab };
        wave2 = new List<GameObject>() { SkeletonPrefab, SkeletonPrefab, SkeletonPrefab };
        currentWave = wave1;
    }

    private void Update()
    {
        Debug.Log(wave);

        if (startWave)
        {
            startWave = false;
            switch (wave)
            {
                case 1:
                    currentWave = wave1;
                    MonsterSpawnScript.spawnDelay = 1f;
                    break;
                case 2:
                    Debug.Log("Starting Wave2");
                    currentWave = wave2;
                    MonsterSpawnScript.spawnDelay = 1f;
                    break;
                default:
                    currentWave = new List<GameObject>();
                    break;
            }
        }
        if (currentWave.Count <= 0)
        {
            StartCoroutine(WaveDelay(1f));
            
        }
    }

    private IEnumerator WaveDelay(float waveDelay)
    {
        yield return new WaitForSeconds(waveDelay);
        startWave = true;
        wave++;
    }

}
