using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float startDelay = 1.5f;
    public float spawnInterval = 1;
    public float spawnRangeX, spawnRangeZ;
    public Transform enemyParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("EnemySpawning", startDelay, spawnInterval);
    }

    void EnemySpawning()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        Vector3 randomSpawn = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        Instantiate(enemyPrefabs[randomIndex], randomSpawn, Quaternion.identity, enemyParent);
    }
}
