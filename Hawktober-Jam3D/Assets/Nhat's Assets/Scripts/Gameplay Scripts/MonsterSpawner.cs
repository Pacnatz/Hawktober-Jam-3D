using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public UIScript uiScript;

    public GameObject[] spawnLocations;
    public GameObject skeletonPrefab;

    private float timer = 4f;
    private float spawnDelay = 10f;
    private float throwSpeed = 1f;
    private float boneSpeed = 40f;


    [HideInInspector]
    public int wave = 0;
    private int currentIndex = 0;
    private List<GameObject> currentWave;
    private List<GameObject> wave1;
    private List<GameObject> wave2;

    [HideInInspector]
    public List<GameObject> CurrentEnemies;

    private bool isActive = true;
    private bool waveInProgress = true;

    private void Start()
    {
        //wave1 = new List<GameObject>() { skeletonPrefab };
        wave1 = new List<GameObject>() { skeletonPrefab, skeletonPrefab, skeletonPrefab, skeletonPrefab, skeletonPrefab };
        wave2 = new List<GameObject>() { skeletonPrefab, skeletonPrefab, skeletonPrefab };

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (isActive) //If undefined wave, stop spawning
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && waveInProgress)
            {
                SpawnMonster();
                timer = spawnDelay;
            }

            if (CurrentEnemies.Count <= 0 && !waveInProgress)
            {
                
                currentIndex = 0;
                timer = 2f;
                SetNewWave(++wave);
                waveInProgress = true;
                uiScript.ShowWave = true;
            }
        }
        
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        SetNewWave(++wave);
        uiScript.ShowWave = true;
    }

    private void SpawnMonster()
    {
        GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)]; //Get a spawn location

        GameObject monsterToSpawn = currentWave[currentIndex];
        GameObject monster = Instantiate(monsterToSpawn, spawnLoc.transform.localPosition, spawnLoc.transform.localRotation);


        //Check if this monster has skeletonAttack script
        monster.transform.GetChild(0).TryGetComponent<SkeletonAttack>(out var skeletonAttack);
        if (skeletonAttack)
        {
            skeletonAttack.SetThrowSpeed(throwSpeed);
            skeletonAttack.SetBoneSpeed(boneSpeed);
        }


        CurrentEnemies.Add(monster);
        currentIndex++;

        if (currentIndex >= currentWave.Count)
        {
            waveInProgress = false; //SetNewWave() is called in Update
        }
    }

    private void SetNewWave(int wave)
    {
        switch (wave)
        {
            case 1:
                spawnDelay = 10f;
                throwSpeed = .9f;
                boneSpeed = 8f;
                currentWave = wave1;
                break;
            case 2:
                spawnDelay = 9f;
                throwSpeed = 1f;
                boneSpeed = 10f;
                currentWave = wave2;

                break;
            default:
                isActive = false;
                break;

        }
    }

}
