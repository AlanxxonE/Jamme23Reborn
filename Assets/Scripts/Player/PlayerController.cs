using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEditor.TerrainTools;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        Rigidbody rb;

        public Rigidbody RB
        {
            get
            {
                if (rb == null)
                {
                    rb = GetComponent<Rigidbody>();
                }
                return rb;
            }
        }

        private void Awake()
        {
            RigidbodyConstraints();
        }
        private void Update()
        {
            
        }

        
        public void Move(float moveZ = 0, float moveX = 0)
        {
            
        }

        public void RigidbodyConstraints()
        {
            RB.constraints = UnityEngine.RigidbodyConstraints.FreezeRotationX;
            RB.constraints = UnityEngine.RigidbodyConstraints.FreezeRotationZ;
            RB.useGravity = false;
        }
    }
}