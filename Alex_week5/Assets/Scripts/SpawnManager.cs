using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject[] powerPrefabs;
    public float powerStartDelay;
    public float powerSpawnInterval;
    public float spawnRangeX, spawnRangeZ;
    public Transform enemyParent;
    public int enemyCount;
    public int enemyWave = 1;

    void Start()
    {
        EnemySpawning(enemyWave);
        InvokeRepeating("PowerUpSpawning", powerStartDelay, powerSpawnInterval);
    }
    void Update()
    {
        enemyCount = FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            EnemySpawning(enemyWave);
        }

        if (enemyWave % 5 == 0 && enemyCount == 0)
        {
            SpawnBossWave();
        }
    }
    void EnemySpawning(int enemiesToSpawn)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        for(int i = 0; i < enemiesToSpawn + 2; i++)
        {
            Instantiate(enemyPrefabs[randomIndex], EnemySpawnPosition(), Quaternion.identity, enemyParent);
        }
    }
    void PowerUpSpawning()
    {
        int randomIndex2 = Random.Range(0, powerPrefabs.Length);
        Vector3 randomSpawn2 = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        for (int p = 0; p < 1; p++)
        {
            Instantiate(powerPrefabs[randomIndex2], randomSpawn2, Quaternion.identity);
        }
    }
    void SpawnBossWave()
    {
        int randomIndex3 = Random.Range(0, bossPrefabs.Length); 
        for(int b = 0; b < 1; b++)
        {
            Instantiate(bossPrefabs[randomIndex3], EnemySpawnPosition(), Quaternion.identity);
        }
    }
    private Vector3 EnemySpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomSpawn = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomSpawn;
    }
}
