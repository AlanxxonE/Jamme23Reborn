using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private enum EnemyState
    {
        WALKING, HIT, EXPLODE, DIE
    };

    private EnemyState enemyState = EnemyState.WALKING;
    private Animator anim;
    private Transform playerTransform;
    private float angle = 0;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponentInChildren<Animator>();
        FindPlayer();
        Walk();
    }

    private void FixedUpdate()
    {
        Vector3 direction = playerTransform.position - transform.position;
        angle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Direction", Mathf.Abs(angle / 180));
        if (Mathf.Abs(angle) >= 45 && Mathf.Abs(angle) <= 135)
        {
            anim.transform.forward = transform.right;
        }
        else
        {
            anim.transform.forward = transform.forward;
        }
    }

    public void Walk()
    {
        switch (enemyState)
        {
            case EnemyState.EXPLODE:
            case EnemyState.DIE:
                break;

            default:
                SetState(EnemyState.WALKING, "Hit", false);
                break;
        }
    }

    public void Explode()
    {
        switch (enemyState)
        {
            case EnemyState.DIE:
                break;

            default:
                SetState(EnemyState.EXPLODE, "Explode", true);
                Destroy(this.gameObject, 1.15f);
                break;
        }
    }

    public void Die()
    {
        switch (enemyState)
        {
            case EnemyState.EXPLODE:
                break;

            default:
                SetState(EnemyState.DIE, "Death", true);
                Destroy(this.gameObject, 1.15f);
                break;
        }
    }

    public void Hit()
    {
        switch (enemyState)
        {
            case EnemyState.EXPLODE:
            case EnemyState.DIE:
                break;

            default:
                SetState(EnemyState.HIT, "Hit", true);
                break;
        }
    }

    private void SetState(EnemyState state, string variable, bool value)
    {
        enemyState = state;
        anim.SetBool(variable, value);
    }

    public void FindPlayer()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void SetSpeed(float speed)
    {
        anim.SetFloat("Velocity", speed);
    }
}