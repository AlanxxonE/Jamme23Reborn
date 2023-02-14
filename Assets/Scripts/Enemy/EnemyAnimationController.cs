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
    private readonly Transform playerTransform;

    public bool Walk()
    {
        if (enemyState != EnemyState.EXPLODE || enemyState != EnemyState.DIE || enemyState != EnemyState.HIT)
        {
            anim.SetInteger("State", 0);
            return true;
        }
        return false;
    }

    public bool Hit()
    {
        if (enemyState != EnemyState.EXPLODE || enemyState != EnemyState.DIE)
        {
            anim.SetInteger("State", 1);
            return true;
        }
        return false;
    }

    public bool Explode()
    {
        if (enemyState != EnemyState.DIE)
        {
            anim.SetInteger("State", 2);
            return true;
        }
        return false;
    }

    public void Die()
    {
        anim.SetInteger("State", 3);
    }

    private void LateUpdate()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 45f)
        {
            anim.SetInteger("view", 0);                 //front
        }
        else if (Mathf.Abs(angle) > 135)
        {
            anim.SetInteger("view", 1);                 //back
        }
        else
        {
            if (angle < 0)
            {
                anim.SetBool("left", true);
                anim.gameObject.GetComponent<SpriteRenderer>().flipY = true;            //left
            }
            else
            {
                anim.SetBool("left", false);
                anim.gameObject.GetComponent<SpriteRenderer>().flipY = false;           //right
            }
            anim.SetInteger("view", 2);                 //side
        }
    }
}