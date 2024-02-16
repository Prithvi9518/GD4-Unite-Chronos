using JetBrains.Annotations;
using Unite.Core.Input;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerController : MonoBehaviour, IHandlePlayerMovement
    {
        [Header("Movement Variables")]
        [SerializeField]
        private float walkSpeed = 7f;
        [SerializeField]
        private float sprintSpeed;
        [SerializeField]
        private float speedMultiplier = 10f;
        [SerializeField]
        private float slopeSpeedMultiplier = 20f;
        [SerializeField]
        private float slopeDownwardForce = 80f;

        [Header("Jump Variables")] 
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float jumpCooldown;
        [SerializeField]
        private float airMultiplier;

        [Header("Ground and Slope Check")] 
        [SerializeField]
        private float groundDrag;
        [SerializeField]
        private float raycastYOffset;
        [SerializeField]
        private float raycastLength;
        [SerializeField]
        private LayerMask groundLayerMask;
        [SerializeField]
        private float maxSlopeAngle = 40f;

        [Header("Orientation")]
        [SerializeField]
        private Transform orientation;

        [Header("Stats")] 
        [SerializeField] 
        private StatTypeSO speedStatType;

        private Vector3 moveDirection;
        private float moveSpeed;

        private Rigidbody rb;

        private bool isGrounded;
        private bool readyToJump;

        private RaycastHit slopeHit;
        private bool exitingSlope;

        private float horizontalInput;
        private float verticalInput;

        private PlayerStatsHandler statsHandler;

        [UsedImplicitly]
        private MovementState currentState;

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
            UpdateState();

            if (isGrounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void UpdateState()
        {
            if (isGrounded && InputManager.Instance.IsSprintActionPressed())
            {
                currentState = MovementState.Sprinting;
                moveSpeed = sprintSpeed;
            }
            else if (isGrounded)
            {
                currentState = MovementState.Walking;
                moveSpeed = walkSpeed;
            }
            else
            {
                currentState = MovementState.Air;
            }
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

            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * (moveSpeed * slopeSpeedMultiplier), ForceMode.Force);

                if (rb.velocity.y > 0)
                {
                    rb.AddForce(Vector3.down * slopeDownwardForce, ForceMode.Force);
                }
            }
            
            if(isGrounded)
                rb.AddForce(moveDirection.normalized * (moveSpeed * speedMultiplier), ForceMode.Force);
            else
                rb.AddForce(moveDirection.normalized * (moveSpeed * speedMultiplier * airMultiplier), ForceMode.Force);

            rb.useGravity = !OnSlope();
        }

        private void SpeedControl()
        {
            if (OnSlope() && !exitingSlope)
            {
                if (rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                if (!(flatVel.magnitude > moveSpeed)) return;
                
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            exitingSlope = true;
            
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
            exitingSlope = false;
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

        private bool OnSlope()
        {
            if (!Physics.Raycast(transform.position + (Vector3.up * raycastYOffset),
                    Vector3.down, raycastLength, groundLayerMask)) return false;
            
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
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