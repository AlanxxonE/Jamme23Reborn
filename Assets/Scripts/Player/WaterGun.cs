using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Scripts.Player
{
    public class WaterGun : MonoBehaviour
    {
        ParticleSystem particles;
        private List<ParticleCollisionEvent> collisionEvents;
        [SerializeField]
        private float individualParticleForce = 1f;

        [SerializeField]
        private float individualParticleDamage = 0.1f;

        private void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            collisionEvents= new List<ParticleCollisionEvent>();
        }
        void OnParticleCollision(GameObject other)
        {
            var EnemyHealth = other.GetComponent<EnemyHealth>();

            if (EnemyHealth)
            {
                Debug.Log("enemy");
            }
            else
            {
                Debug.Log("Fail");
            }
            int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);

            Rigidbody rb = other.GetComponentInChildren<Rigidbody>();
            int i = 0;

            while (i < numCollisionEvents)
            {
                if(EnemyHealth)
                {
                    EnemyHealth.DealDamage(individualParticleDamage);
                }
                if (rb)
                {
                    Vector3 force = collisionEvents[i].velocity * individualParticleForce;
                    rb.AddForce(force);
                }
                i++;
            }
        }
    }
}
