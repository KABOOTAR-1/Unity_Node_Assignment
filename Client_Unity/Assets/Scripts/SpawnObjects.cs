using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float spawnInterval = 0.7f;

    private const float SpawnXMin = -6f;
    private const float SpawnXMax = 6f;
    private const float SpawnYMin = -4f;
    private const float SpawnYMax = 4f;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 0,spawnInterval);
    }


    private void SpawnObject()
    {
        GameObject prefabToSpawn = Random.Range(0, 6) == 0 ? bombPrefab : pickupPrefab;
        Instantiate(prefabToSpawn, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(SpawnXMin, SpawnXMax);
        float randomY = Random.Range(SpawnYMin, SpawnYMax);
        return new Vector2(randomX, randomY);
    }
}