using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPointLocations;

    [SerializeField]
    private List<Transform> targetPointLocations;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int maxEnemies = 10;

    [SerializeField]
    private int numberOfEnemiesToSpawn = 3;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            InvokeRepeating(nameof(GenerateEnemiesBasedOnSpawnAndTargetLocations), 0, 2f);
        }
    }

    private void GenerateEnemiesBasedOnSpawnAndTargetLocations()
    {
        if (targetPointLocations.Count > 0 && spawnPointLocations.Count > 0)
        {
            int randSpawn = Random.Range(0, spawnPointLocations.Count - 1);

            int randTarget = Random.Range(0, targetPointLocations.Count - 1);

            GameObject enemyClone = Instantiate(enemyPrefab, spawnPointLocations[randSpawn].position, Quaternion.identity);

            enemyClone.AddComponent<EnemyBehaviour>().AssignEnemyToTarget(targetPointLocations[randTarget]);
        }
    }
}