using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(nameof(EnemySpawn));
    }

    IEnumerator EnemySpawn()
    {
        int currentSpawnPoint = Random.Range(0, 5);
        Vector3 enemySpawnPosition = spawnPoints[currentSpawnPoint].GetComponent<Transform>().position;
        Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(EnemySpawn));
    }
}
