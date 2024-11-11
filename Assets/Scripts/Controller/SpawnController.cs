using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Spawn Config")]
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private Transform[] spawnPoints;  
    [SerializeField] private float spawnInterval = 5f; 

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); 

            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}
