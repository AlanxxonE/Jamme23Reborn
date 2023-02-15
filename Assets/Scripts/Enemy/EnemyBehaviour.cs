using System;
using System.Collections;
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

    public enum EnemyMode
    {
        Agent,
        Physic
    }

    public EnemyType currentEnemyType;

    private Transform targetToChase;

    private Vector3 directionTowardsTarget;

    private NavMeshAgent agent;

    private Collider col;

    private Rigidbody rb;

    [SerializeField]
    private float damageCoolDown = 3f;

    private void OnEnable()
    {
        GetComponent<EnemyHealth>().EnemyBehaviour = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

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
        agent.isStopped = true;
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

    public void ChangeMode(EnemyMode em)
    {
        switch (em)
        {
            default:
            case EnemyMode.Agent:
                rb.isKinematic = true;
                agent.enabled = true;
                //agent.isStopped = false;
                agent.SetDestination(directionTowardsTarget);
                break;

            case EnemyMode.Physic:
                //agent.isStopped = true;
                agent.enabled = false;
                rb.isKinematic = false;
                break;
        }
    }

    public IEnumerator DoAfterDelay(float delaySeconds, Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (rb.velocity.magnitude <= 0.1f)
        {
            thingToDo();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}