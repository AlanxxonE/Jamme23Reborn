using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private enum EnemyType
    {
        SplashEnemy,
        CoolEnemy
    }

    [SerializeField]
    private EnemyType currentEnemyType;

    private Transform targetToChase;

    private float enemySpeed;

    private Vector3 directionTowardsTarget;

    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 2;

        AssignEnemyToTarget(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        AssignMovementBasedOnEnemyType();
    }

    void AssignEnemyToTarget(Transform targetToAssign)
    {
        targetToChase = targetToAssign;
    }

    void DirectionToFollow()
    {
        directionTowardsTarget = this.transform.position - targetToChase.transform.position;
    }

    void AssignMovementBasedOnEnemyType()
    {
        switch(currentEnemyType)
        {
            case EnemyType.SplashEnemy:
                this.transform.Translate(directionTowardsTarget * enemySpeed);
                break;
        }
    }
}
