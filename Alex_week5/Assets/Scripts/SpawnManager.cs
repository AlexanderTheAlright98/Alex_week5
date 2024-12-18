using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerPrefabs;
    public float startDelay = 1.5f;
    public float spawnInterval = 1;
    public float powerStartDelay = 2.5f;
    public float powerSpawnInterval = 3;
    public float spawnRangeX, spawnRangeZ;
    public Transform enemyParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("EnemySpawning", startDelay, spawnInterval);
        InvokeRepeating("PowerUpSpawning", powerStartDelay, powerSpawnInterval);
    }

    void EnemySpawning()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        Vector3 randomSpawn = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        Instantiate(enemyPrefabs[randomIndex], randomSpawn, Quaternion.identity, enemyParent);
    }

    void PowerUpSpawning()
    {
        int randomIndex2 = Random.Range(0, powerPrefabs.Length);

        Vector3 randomSpawn2 = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        Instantiate(powerPrefabs[randomIndex2], randomSpawn2, Quaternion.identity);
    }
}
