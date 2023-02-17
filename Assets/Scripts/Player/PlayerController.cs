using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        //Rigidbody
        private Rigidbody rb;

        //Particles
        [SerializeField]
        private ParticleSystem[] particles;

        //Camera
        private Camera cam;

        [SerializeField, Range(0, 100)]
        private float mouseSensitivity;

        private float cameraRotationY;

        [SerializeField]
        private float minRotationY = -90;

        [SerializeField]
        private float maxRotationY = 90;

        //Movement
        [SerializeField]
        [Range(0, 100)]
        private float movementSpeed = 1.0f;

        private Vector2 movement = new Vector2(0, 0);

        #region Getters/Setters

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
            set
            {
                if (rb != null) return;
                rb = value;
            }
        }

        #endregion Getters/Setters

        private void Awake()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            cam = Camera.main;
            RB = GetComponent<Rigidbody>();
            RigidbodyConstraints();
        }

        private void FixedUpdate()
        {
            //RB.angularVelocity = Vector3.zero;
            HandleMovement();
        }

        private void LateUpdate()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                FindObjectOfType<LevelManager>().LoadSceneByIndex(0);
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }

        private void HandleMovement()
        {
            float speedX = movement.x * movementSpeed;
            float speedY = movement.y * movementSpeed;

            Vector3 velocityX = speedX * RB.transform.right;
            Vector3 velocityY = speedY * RB.transform.forward;

            RB.velocity = velocityX + velocityY;
        }

        public void Look(InputAction.CallbackContext context)
        {
            float lookAmount = context.ReadValue<Vector2>().y;

            cameraRotationY -= lookAmount * mouseSensitivity;
            cameraRotationY = Mathf.Clamp(cameraRotationY, minRotationY, maxRotationY);

            cam.transform.localRotation = Quaternion.Euler(cameraRotationY, 0, 0);
        }

        public void Turn(InputAction.CallbackContext context)
        {
            float turnAmount = context.ReadValue<Vector2>().x * mouseSensitivity;
            transform.Rotate(0, turnAmount, 0);
            RB.transform.forward = transform.forward;
        }

        public void Fire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                foreach (var particle in particles)
                {
                    particle.Play();
                }
            }
            else if (context.canceled)
            {
                foreach (var particle in particles)
                {
                    particle.Stop();
                }
            }
        }

        public void RigidbodyConstraints()
        {
            //RB.constraints = UnityEngine.RigidbodyConstraints.FreezeRotationX;
            //RB.constraints = UnityEngine.RigidbodyConstraints.FreezeRotationZ;
            //RB.useGravity = false;
        }
    }
}