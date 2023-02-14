using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;
    public EnemyBehaviour EnemyBehaviour { get; set; }

    private float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public void DealDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            EnemyBehaviour.Dead();
        }

        print(Health);
    }
}