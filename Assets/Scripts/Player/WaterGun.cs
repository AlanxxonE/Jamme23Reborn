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
        private ParticleSystem particles;
        private List<ParticleCollisionEvent> collisionEvents;

        [SerializeField]
        private float individualParticleForce = 5f;

        [SerializeField]
        private float individualParticleDamage = 10f;

        private void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
        }

        private void OnParticleCollision(GameObject other)
        {
            print(other.name);

            if (other.name.Contains("Enemy"))
            {
                int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);
                var enemy = other.GetComponent<EnemyHealth>();
                var rb = other.GetComponent<Rigidbody>();
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    enemy.DealDamage(individualParticleDamage);
                    if (rb)
                    {
                        Vector3 force = collisionEvents[i].velocity * individualParticleForce;
                        rb.AddForce(force);
                    }
                }
            }
        }
    }
}