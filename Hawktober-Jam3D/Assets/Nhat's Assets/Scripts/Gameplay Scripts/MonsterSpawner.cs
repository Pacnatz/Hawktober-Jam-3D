using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public UIScript uiScript;

    public GameObject[] spawnLocations;
    public GameObject skeletonPrefab;
    public GameObject pumpkinPrefab;

    private float timer = 2f;
    [HideInInspector]
    public float waveTimer = 0;
    [HideInInspector]
    public float waveTimerStart = 1;
    private float spawnDelay = 10f;

    //Skele var
    private float throwSpeed = 1f;
    private float boneSpeed = 40f;
    private float boneDamage = 5f;
    private float skeleHealth = 100f;
    //Pumpkin var
    private float pumpkinDamage = 20f;
    private float pumpkinHealth = 20f;
    private float pumpkinSpawnChance = .2f; //0 to 1 percent spawn chance with each skeleton spawn

    [HideInInspector]
    public int wave = 0;
    private int currentIndex = 0;
    private List<GameObject> currentWave;
    private List<GameObject> wave1 = new List<GameObject>();
    private List<GameObject> wave2 = new List<GameObject>();
    private List<GameObject> wave3 = new List<GameObject>();
    private List<GameObject> wave4 = new List<GameObject>();
    private List<GameObject> wave5 = new List<GameObject>();
    private List<GameObject> wave6 = new List<GameObject>();
    private List<GameObject> wave7 = new List<GameObject>();
    private List<GameObject> wave8 = new List<GameObject>();
    private List<GameObject> wave9 = new List<GameObject>();
    private List<GameObject> wave10 = new List<GameObject>();
    private List<GameObject> wave11 = new List<GameObject>();
    private List<GameObject> wave12 = new List<GameObject>();
    private List<GameObject> wave13 = new List<GameObject>();
    private List<GameObject> wave14 = new List<GameObject>();
    private List<GameObject> wave15 = new List<GameObject>();
    private List<GameObject> wave16 = new List<GameObject>();
    private List<GameObject> wave17 = new List<GameObject>();
    private List<GameObject> wave18 = new List<GameObject>();
    private List<GameObject> wave19 = new List<GameObject>();
    private List<GameObject> wave20 = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> CurrentEnemies;

    [HideInInspector]
    public bool isActive = false;
    private bool waveInProgress = true;



    private void Start()
    {
        wave = 0;
        //wave1 = new List<GameObject>() { pumpkinPrefab, pumpkinPrefab, pumpkinPrefab, pumpkinPrefab, pumpkinPrefab };
        //Wave set up
        for (int i = 0; i < 5; i++) {
            wave1.Add(skeletonPrefab);
        }
        for (int i = 0; i < 5; i++){
            wave2.Add(skeletonPrefab);
        }
        for (int i = 0; i < 8; i++){
            wave3.Add(skeletonPrefab);
        }
        for (int i = 0; i < 10; i++){
            wave4.Add(skeletonPrefab);
        }
        for (int i = 0; i < 10; i++){
            wave5.Add(skeletonPrefab);
        }
        for (int i = 0; i < 12; i++){
            wave6.Add(skeletonPrefab);
        }
        for (int i = 0; i < 12; i++){
            wave7.Add(skeletonPrefab);
        }
        for (int i = 0; i < 14; i++){
            wave8.Add(skeletonPrefab);
        }
        for (int i = 0; i < 14; i++){
            wave9.Add(skeletonPrefab);
        }
        for (int i = 0; i < 15; i++){
            wave10.Add(skeletonPrefab);
        }
        for (int i = 0; i < 16; i++)
        {
            wave11.Add(skeletonPrefab);
        }
        for (int i = 0; i < 16; i++)
        {
            wave12.Add(skeletonPrefab);
        }
        for (int i = 0; i < 18; i++)
        {
            wave13.Add(skeletonPrefab);
        }
        for (int i = 0; i < 19; i++)
        {
            wave14.Add(skeletonPrefab);
        }
        for (int i = 0; i < 20; i++)
        {
            wave15.Add(skeletonPrefab);
        }
        for (int i = 0; i < 22; i++)
        {
            wave16.Add(skeletonPrefab);
        }
        for (int i = 0; i < 23; i++)
        {
            wave17.Add(skeletonPrefab);
        }
        for (int i = 0; i < 25; i++)
        {
            wave18.Add(skeletonPrefab);
        }
        for (int i = 0; i < 30; i++)
        {
            wave19.Add(skeletonPrefab);
        }
        for (int i = 0; i < 999; i++)
        {
            wave20.Add(skeletonPrefab);
        }
        StartCoroutine(StartGame()); //Starts wave after 2 seconds
    }



    private void Update()
    {
        if (isActive) //If undefined wave, stop spawning
        {
            

            timer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
            waveTimer = Mathf.Clamp(waveTimer, 0f, 9999f);

            if (timer <= 0 && waveInProgress)
            {
                SpawnMonster();
                timer = spawnDelay;
                float choice = Random.Range(0f, 1f);
                if (choice < pumpkinSpawnChance)
                {
                    SpawnPumpkin();
                }
            }

            if (CurrentEnemies.Count <= 0 && !waveInProgress)
            {
                
                currentIndex = 0;
                timer = 2f;
                SetNewWave(++wave);
                waveInProgress = true;
                uiScript.ShowWave = true;
            }

            if (waveTimer <= 0)
            {
                foreach (GameObject monster in CurrentEnemies)
                {
                    //Set notice area of skeleton
                    monster.transform.TryGetComponent<Skeleton>(out var skeleton);
                    if (skeleton)
                    {
                        skeleton.WalkRange = 100f;
                    }
                }
            }
        }
        
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        SetNewWave(++wave);
        isActive = true;
        uiScript.ShowWave = true;
    }

    private void SpawnMonster()
    {
        GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)]; //Get a spawn location

        GameObject monsterToSpawn = currentWave[currentIndex];
        GameObject monster = Instantiate(monsterToSpawn, spawnLoc.transform.localPosition, spawnLoc.transform.localRotation);

        //Access skeleton health
        monster.transform.TryGetComponent<Skeleton>(out var skeleton);
        if (skeleton)
        {
            skeleton.Health = skeleHealth;
        }
        //Check if this monster has skeletonAttack script
        monster.transform.GetChild(0).TryGetComponent<SkeletonAttack>(out var skeletonAttack);
        if (skeletonAttack)
        {
            skeletonAttack.SetThrowSpeed(throwSpeed);
            skeletonAttack.SetBoneSpeed(boneSpeed);
            skeletonAttack.SetBoneDamage(boneDamage);
        }


        CurrentEnemies.Add(monster);
        currentIndex++;

        if (currentIndex >= currentWave.Count)
        {
            waveInProgress = false; //SetNewWave() is called in Update
        }
    }

    private void SpawnPumpkin()
    {
        GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)]; //Get a spawn location

        GameObject monsterToSpawn = pumpkinPrefab;
        GameObject monster = Instantiate(monsterToSpawn, spawnLoc.transform.localPosition, spawnLoc.transform.localRotation);

        //Access pumpkin health
        monster.transform.TryGetComponent<Pumpkin>(out var pumpkin);
        if (pumpkin)
        {
            pumpkin.Health = pumpkinHealth;
            pumpkin.Damage = pumpkinDamage;
        }

        CurrentEnemies.Add(monster);

        //Make decision if to spawn another pumpkin
        float choice = Random.Range(0f, 1f);
        if (pumpkinSpawnChance > .5)
        {
            if (choice < .7)
            {
                SpawnPumpkin();
            }
        }
        else
        {
            if (choice < pumpkinSpawnChance)
            {
                SpawnPumpkin();
            }
        }
    }

    private void SetNewWave(int wave) //Called every new wave, these are the settings for each wave.
    {
        switch (wave)
        {
            case 1:
                SetWaveSettings(9f, .9f, 6f, 6f, 80f, wave1, 20f, 20f, .30f);
                break;
            case 2:
                SetWaveSettings(8f, 1f, 10f, 7f, 90f, wave2, 20f, 20f, .30f);
                break;
            case 3:
                SetWaveSettings(8f, 1.05f, 8f, 8f, 100f, wave3, 20f, 20f, .35f);
                break;
            case 4:
                SetWaveSettings(7.5f, 1.1f, 9f, 9f, 105f, wave4, 20f, 20f, .40f);
                break;
            case 5:
                SetWaveSettings(7f, 1.1f, 9f, 10f, 110f, wave5, 20f, 20f, .45f);
                break;
            case 6:
                SetWaveSettings(7f, 1.15f, 9f, 11f, 115f, wave6, 25f, 25f, .50f);
                break;
            case 7:
                SetWaveSettings(6.5f, 1.15f, 10f, 11f, 125f, wave7, 25f, 25f, .60f);
                break;
            case 8:
                SetWaveSettings(6.5f, 1.2f, 10f, 13f, 130f, wave8, 25f, 30f, .60f);
                break;
            case 9:
                SetWaveSettings(6.5f, 1.3f, 11f, 14f, 140f, wave9, 25f, 30f, .70f);
                break;
            case 10:
                SetWaveSettings(6f, 1.4f, 12f, 15f, 150f, wave10, 30f, 35f, .70f);
                break;
            case 11:
                SetWaveSettings(6f, 1.5f, 13f, 15f, 155f, wave11, 30f, 35f, .80f);
                break;
            case 12:
                SetWaveSettings(5.5f, 1.6f, 14f, 16f, 160f, wave12, 30f, 35f, .80f);
                break;
            case 13:
                SetWaveSettings(5.5f, 1.65f, 15f, 18f, 165f, wave13, 30f, 35f, .90f);
                break;
            case 14:
                SetWaveSettings(5.5f, 1.75f, 17f, 20f, 170f, wave14, 30f, 35f, .90f);
                break;
            case 15:
                SetWaveSettings(5f, 1.8f, 18f, 22f, 170f, wave15, 35f, 35f, 1f);
                break;
            case 16:
                SetWaveSettings(5f, 1.85f, 19f, 22f, 170f, wave16, 35f, 35f, 1f);
                break;
            case 17:
                SetWaveSettings(5f, 1.85f, 19f, 23f, 175f, wave17, 35f, 35f, 1f);
                break;
            case 18:
                SetWaveSettings(4.5f, 1.9f, 20f, 24f, 175f, wave18, 35f, 40f, 1f);
                break;
            case 19:
                SetWaveSettings(4.5f, 1.9f, 20f, 25f, 175f, wave19, 35f, 40f, 1f);
                break;
            case 20:
                SetWaveSettings(4f, 2f, 22f, 25f, 180f, wave20, 40f, 40f, 1f);
                break;
            default:
                isActive = false;
                break;

        }
        waveTimerStart = ((spawnDelay - 0.2f) * currentWave.Count) - 5f; //After all enemies have spawned and 10 seconds have elapsed, set notice area to max
        waveTimer = waveTimerStart;
    }
    private void SetWaveSettings(float _spawnDelay, float _throwSpeed, float _boneSpeed, float _boneDamage, float _skeleHealth, List<GameObject> _wave,
                                 float _pumpkinDamage, float _pumpkinHealth, float _pumpkinChance)
    {
        spawnDelay = _spawnDelay;
        throwSpeed = _throwSpeed;
        boneSpeed = _boneSpeed;
        boneDamage = _boneDamage;
        skeleHealth = _skeleHealth;
        currentWave = _wave;
        pumpkinDamage = _pumpkinDamage;
        pumpkinHealth = _pumpkinHealth;
        pumpkinSpawnChance = _pumpkinChance;
    }

}
