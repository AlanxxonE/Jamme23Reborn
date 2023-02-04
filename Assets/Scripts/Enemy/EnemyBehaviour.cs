using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 0.2f;

        DirectionToFollow();
    }

    // Update is called once per frame
    void Update()
    {
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
        directionTowardsTarget = targetToChase.transform.position - this.transform.position;
    }

    void AssignMovementBasedOnEnemyType()
    {
        switch(currentEnemyType)
        {
            case EnemyType.SplashEnemy:
                DirectionToFollow();
                this.transform.Translate(directionTowardsTarget * enemySpeed *Time.deltaTime);
                break;
        }
    }
}
