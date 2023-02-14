using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyType
    {
        SplashEnemy,
        CoolEnemy
    }

    public EnemyType currentEnemyType;

    private Transform targetToChase;

    private Vector3 directionTowardsTarget;

    private NavMeshAgent agent;

    private void OnEnable()
    {
        GetComponent<EnemyHealth>().EnemyBehaviour = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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

    private void DirectionToFollow()
    {
        directionTowardsTarget = targetToChase.transform.position;
    }

    private void AssignMovementBasedOnEnemyType()
    {
        switch (currentEnemyType)
        {
            case EnemyType.SplashEnemy:
                agent.SetDestination(directionTowardsTarget);
                break;
        }
    }

    public void Dead()
    {
        agent.enabled = false;
        Destroy(gameObject);
        //Insert animation here
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ToothBehaviour>() != null)
        {
            collision.gameObject.GetComponent<ToothBehaviour>().CheckForToothCollisionBasedOnType(ToothBehaviour.ToothInteraction.EnemyToothInteraction);
        }
    }
}