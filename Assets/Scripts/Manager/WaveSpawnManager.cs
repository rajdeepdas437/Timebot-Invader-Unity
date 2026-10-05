using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawnManager : MonoBehaviour
{
    public Wave[] waves;
    private int nextWave = 0;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float waveCountDown;
    private float timeBetweenEnemySearch = 1f;

    [SerializeField] Transform[] spawnPoints;
    private bool wavesCompleted=false;


    private enum SpawningStates
    {
        spawning,
        waiting,
        counting
    }

    private SpawningStates spawnState;
    
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        spawnState = SpawningStates.counting;
    }

    
    void Update()
    {
        if(spawnState == SpawningStates.waiting)
        {
            if(!EnemiesAreAlive())
            {
                Debug.Log("Wave completed");
                StartNextWave();
            }
            else return;
        }
        
        if(!wavesCompleted)
        {
            if(waveCountDown <= 0)
            {
                if(spawnState != SpawningStates.spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }
        }
        
    }

    IEnumerator SpawnWave(Wave waveToSpawn)
    {
        spawnState = SpawningStates.spawning;

        for(int i=0; i<waveToSpawn.amountOfEnemies; i++)
        {
            int randomEnemyNumber = Random.Range(0, waveToSpawn.enemies.Length);
            SpawnEnemy(waveToSpawn.enemies[randomEnemyNumber]);
            yield return new WaitForSeconds(waveToSpawn.spawnDelay);
        }

        spawnState = SpawningStates.waiting;
    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Debug.Log("Spawning the enemy : " + enemyToSpawn.name);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyToSpawn, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
    }

    private bool EnemiesAreAlive()
    {
        timeBetweenEnemySearch -= Time.deltaTime;

        if(timeBetweenEnemySearch <= 0)
        {
            timeBetweenEnemySearch = 1f;

            if(FindObjectsByType<EnemyController>().Length == 0)
            {
                return false;
            }
        }

        return true;
    }

    void StartNextWave()
    {
        spawnState = SpawningStates.counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave+1 == waves.Length)
        {
            wavesCompleted=true;
            LevelManager.instance.LevelPicker();
            Debug.Log("waves completed");
        }
        else
        {
            nextWave++;
        }
    }

    public bool IsWaveCompleted()
    {
        return wavesCompleted;
    }
}

[System.Serializable]

public class Wave
{
    public string name;
    public GameObject[] enemies;
    public int amountOfEnemies;
    public float spawnDelay;
}
