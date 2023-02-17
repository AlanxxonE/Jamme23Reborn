using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyMode
    {
        Agent,
        Physic
    }

    private Transform targetToChase;
    private Vector3 directionTowardsTarget;
    private NavMeshAgent agent;
    private Collider col;
    private Rigidbody rb;
    private EnemyAnimationController anim;

    [SerializeField]
    private float damageCoolDown = 3f;

    private void OnEnable()
    {
        GetComponent<EnemyHealth>().EnemyBehaviour = this;
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            anim.SetSpeed(agent.velocity.magnitude);
        }
        if (agent.isActiveAndEnabled)
        {
            if (agent.remainingDistance - agent.stoppingDistance <= 0.1)
            {
                anim.Explode();
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        anim = transform.GetComponent<EnemyAnimationController>();

        DirectionToFollow();

        AssignMovementBasedOnEnemyType();
    }

    public void AssignEnemyToTarget(Transform targetToAssign)
    {
        targetToChase = targetToAssign;
    }

    private Vector3 DirectionToFollow()
    {
        directionTowardsTarget = targetToChase.transform.position;
        return directionTowardsTarget;
    }

    private void AssignMovementBasedOnEnemyType()
    {
        agent.SetDestination(DirectionToFollow());
    }

    public void Dead()
    {
        //agent.isStopped = true;
        anim.Die();
        //Insert animation here
    }

    public void ChangeMode(EnemyMode em)
    {
        switch (em)
        {
            default:
            case EnemyMode.Agent:
                rb.isKinematic = true;
                agent.enabled = true;
                agent.isStopped = false;
                agent.SetDestination(DirectionToFollow());
                break;

            case EnemyMode.Physic:
                agent.isStopped = true;
                agent.enabled = false;
                rb.isKinematic = false;
                break;
        }
    }

    public void RecoverFromHit()
    {
        ChangeMode(EnemyMode.Agent);
        anim.Walk();
    }

    public IEnumerator DoAfterDelay(float delaySeconds, Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo();
    }

    public IEnumerator DoWhenStopped(Action thingToDo)
    {
        while (rb.velocity.magnitude <= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        thingToDo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tooth"))
        {
        }
    }

    public void Hit()
    {
        ChangeMode(EnemyMode.Physic);
        anim.Hit();
        StartCoroutine(DoWhenStopped(() =>
        {
            RecoverFromHit();
        }));
    }
}