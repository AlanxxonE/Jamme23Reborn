using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;

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


    public void DealDamage(float damage = 0)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Debug.Log("Dead");
        }
    }
}
