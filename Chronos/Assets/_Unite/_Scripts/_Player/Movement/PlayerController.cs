using Unite.Core.Input;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerController : MonoBehaviour, IHandlePlayerMovement
    {
        [Header("Movement Variables")]
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float speedMultiplier = 10f;

        [Header("Jump Variables")] 
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float jumpCooldown;
        [SerializeField]
        private float airMultiplier;

        [Header("Ground Check")] 
        [SerializeField]
        private float groundDrag;
        [SerializeField]
        private float raycastYOffset;
        [SerializeField]
        private float raycastLength;
        [SerializeField]
        private LayerMask groundLayerMask;

        [Header("Orientation")]
        [SerializeField]
        private Transform orientation;

        [Header("Stats")] 
        [SerializeField] 
        private StatTypeSO speedStatType;

        private Vector3 moveDirection;

        private Rigidbody rb;

        private bool isGrounded;

        private float horizontalInput;
        private float verticalInput;

        private bool readyToJump;

        private PlayerStatsHandler statsHandler;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        private void Start()
        {
            readyToJump = true;
        }

        private void Update()
        {
            isGrounded = Physics.Raycast(transform.position + (Vector3.up * raycastYOffset),
                Vector3.down, raycastLength, groundLayerMask);
            
            GetInput();
            SpeedControl();

            if (isGrounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void GetInput()
        {
            Vector2 moveVector = InputManager.Instance.GetMovementVectorNormalized();
            horizontalInput = moveVector.x;
            verticalInput = moveVector.y;
        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if(isGrounded)
                rb.AddForce(moveDirection.normalized * (moveSpeed * speedMultiplier), ForceMode.Force);
            else
                rb.AddForce(moveDirection.normalized * (moveSpeed * speedMultiplier * airMultiplier), ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }

        public void HandleJumpAction()
        {
            if (readyToJump && isGrounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector3 startPos = transform.position + (Vector3.up * raycastYOffset);
            
            Gizmos.DrawRay(startPos, Vector3.down * raycastLength);
        }

        public void PerformSetup(PlayerStatsHandler playerStatsHandler)
        {
            statsHandler = playerStatsHandler;
        }

        public void UpdateSpeedFromStats()
        {
            Stat speedStat = statsHandler.GetStat(speedStatType);
            if (speedStat == null) return;

            float baseSpeed = speedStat.Value;
            moveSpeed = baseSpeed;
        }

        public void ModifySpeed(float modifier)
        {
            moveSpeed += modifier;
        }

        public void ToggleMovement(bool toggle)
        {
        }
    }
}