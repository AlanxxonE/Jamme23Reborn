using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyType
    {
        SplashEnemy,
        CoolEnemy
    }

    public EnemyType currentEnemyType;

    private Transform targetToChase;

    private float enemySpeed;

    private Vector3 directionTowardsTarget;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        enemySpeed = 0.2f;

        DirectionToFollow();

        AssignMovementBasedOnEnemyType();
    }

    public void AssignEnemyToTarget(Transform targetToAssign)
    {
        targetToChase = targetToAssign;
    }

    public void AdaptEnemyType(EnemyType enemyType)
    {
        currentEnemyType = enemyType;
    }

    void DirectionToFollow()
    {
        directionTowardsTarget = targetToChase.transform.position;
    }

    void AssignMovementBasedOnEnemyType()
    {
        switch(currentEnemyType)
        {
            case EnemyType.SplashEnemy:
                agent.SetDestination(directionTowardsTarget);
                break;
        }
    }
}
