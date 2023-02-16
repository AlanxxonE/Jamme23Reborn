using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private enum EnemyState
    {
        WALKING, HIT, EXPLODE, DIE
    };

    private readonly EnemyState enemyState = EnemyState.WALKING;
    public Animator anim;
    private Transform playerTransform;
    private float angle = 0;

    private void FixedUpdate()
    {
        Vector3 direction = playerTransform.position - transform.position;
        angle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Vertical", Mathf.Abs(angle / 180));
        if (Mathf.Abs(angle) >= 45 && Mathf.Abs(angle) <= 135)
        {
            anim.transform.forward = gameObject.transform.right;
        }
        else
        {
            anim.transform.forward = gameObject.transform.forward;
        }
    }

    public void FindPlayer()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
}