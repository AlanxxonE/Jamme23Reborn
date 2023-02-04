using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateEnemiesBasedOnSpawnAndTargetLocations", 0, 2f);
        InvokeRepeating("GenerateEnemiesBasedOnSpawnAndTargetLocations", 0, 2f);
        InvokeRepeating("GenerateEnemiesBasedOnSpawnAndTargetLocations", 0, 2f);
    }

    void GenerateEnemiesBasedOnSpawnAndTargetLocations()
    {
        if (targetPointLocations.Count > 0 && spawnPointLocations.Count > 0)
        {
            int randSpawn = Random.Range(0, spawnPointLocations.Count - 1);

            int randTarget = Random.Range(0, targetPointLocations.Count - 1);

            GameObject enemyClone = Instantiate(enemyPrefab, spawnPointLocations[randSpawn].position, Quaternion.identity);

            enemyClone.AddComponent<EnemyBehaviour>().AssignEnemyToTarget(targetPointLocations[randTarget]);

            enemyClone.GetComponent<EnemyBehaviour>().AdaptEnemyType(EnemyBehaviour.EnemyType.SplashEnemy);
        }
    }
}
