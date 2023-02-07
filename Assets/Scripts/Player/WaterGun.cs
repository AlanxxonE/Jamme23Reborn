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
        private float individualParticleForce = 5f;

        [SerializeField]
        private float individualParticleDamage = 10f;

        private void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            collisionEvents= new List<ParticleCollisionEvent>();
        }
        void OnParticleTrigger(GameObject other)
        {
            var EnemyHealth = other.GetComponent<EnemyHealth>();


            int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);

            Rigidbody rb = other.GetComponent<Rigidbody>();

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
