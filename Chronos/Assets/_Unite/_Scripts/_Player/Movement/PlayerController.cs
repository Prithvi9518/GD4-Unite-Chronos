using System;
using System.Collections;
using Unite.Core.Input;
using Unite.EventSystem;
using Unite.StatSystem;
using Unite.WeaponSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerController : MonoBehaviour, IHandlePlayerMovement
    {
        [SerializeField] 
        private PlayerMovementData movementData;

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
        private StatTypeSO speedBoostStatType;

        [Header("Events for Analytics")] 
        [SerializeField]
        private GameEvent onDashUsedUpdateAnalytics;

        private const float SpeedDiffTolerance = 0.001f;

        private Vector3 moveDirection;
        private float moveSpeed;

        private Rigidbody rb;

        private bool isGrounded;
        private bool readyToJump;

        private RaycastHit slopeHit;
        private bool exitingSlope;

        private float horizontalInput;
        private float verticalInput;

        private bool isDashing;

        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;
        private MovementState lastMovementState;
        private bool keepMomentum;
        
        private float speedChangeFactor;

        private float maxYSpeed;

        private Coroutine smoothLerpCoroutine;

        private PlayerStatsHandler statsHandler;
        private PlayerCameraHandler cameraHandler;
        private PlayerDashHandler dashHandler;
        private PlayerFootsteps playerFootsteps;
        private WeaponBobAndSway weaponBobAndSway;

        private MovementState currentState;

        private bool cameraInitialized;

        private bool isMoving;

        private bool canDash;
        private bool isWalkingSlowly;

        private float speedBoost;

        public Transform Orientation => orientation;
        public PlayerCameraHandler CameraHandler => cameraHandler;
        public Rigidbody PlayerRigidbody => rb;
        public PlayerMovementData MovementData => movementData;
        public MovementState CurrentMovementState => currentState;
        public bool IsDashing { get; set; }
        public float MaxYSpeed { get; set; }
        public bool IsGrounded => isGrounded;
        public GameEvent OnDashUsed_UpdateAnalytics => onDashUsedUpdateAnalytics;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            
            cameraHandler = GetComponent<PlayerCameraHandler>();
            playerFootsteps = GetComponent<PlayerFootsteps>();
            playerFootsteps.ToggleFootstepSounds(true);
        }

        private void Start()
        {
            readyToJump = true;
            canDash = true;
            isMoving = true;
        }

        public void InitializeCamera()
        {
            Debug.Log("PlayerController - InitializeCamera()");
            Camera cam = Managers.GameManager.Instance.PlayerCamera;
            cameraHandler.InitializeCamera(cam);
            dashHandler = new PlayerDashHandler(this);
            cameraInitialized = true;
        }

        public void InitializeWeaponHolder()
        {
            Debug.Log("PlayerController - InitializeWeaponHolder()");
            WeaponHolder weaponHolder = Managers.GameManager.Instance.WeaponsHolder;
            if (!weaponHolder.TryGetComponent(out WeaponBobAndSway wbs)) return;
            
            weaponBobAndSway = wbs;
            weaponBobAndSway.PerformSetup(this);
        }

        private void Update()
        {
            if (!cameraInitialized) return;
            if (!isMoving) return;
            
            isGrounded = Physics.Raycast(transform.position + (Vector3.up * raycastYOffset),
                Vector3.down, raycastLength, groundLayerMask);
            
            GetInput();
            SpeedControl();
            UpdateState();

            if (isGrounded && currentState != MovementState.Dashing)
                rb.drag = movementData.GroundDrag;
            else
                rb.drag = 0;
            
            dashHandler.DoUpdate();
        }

        private void FixedUpdate()
        {
            if (!cameraInitialized) return;
            if(!isMoving) return;
            
            MovePlayer();
        }

        private void UpdateState()
        {
            if (isDashing)
            {
                currentState = MovementState.Dashing;
                desiredMoveSpeed = movementData.DashSpeed;
                speedChangeFactor = movementData.DashSpeedChangeFactor;
            }
            else if (isGrounded && InputManager.Instance.IsSprintActionPressed())
            {
                currentState = MovementState.Sprinting;
                desiredMoveSpeed = (isWalkingSlowly) ? movementData.SlowWalkSpeed : movementData.SprintSpeed;
            }
            else if (isGrounded)
            {
                currentState = MovementState.Walking;
                desiredMoveSpeed = (isWalkingSlowly) ? movementData.SlowWalkSpeed : movementData.DefaultSpeed;
            }
            else
            {
                currentState = MovementState.Air;

                if (desiredMoveSpeed < movementData.SprintSpeed)
                {
                    desiredMoveSpeed = movementData.DefaultSpeed;
                }
                else
                {
                    desiredMoveSpeed = movementData.SprintSpeed;
                }
            }

            bool desiredMoveSpeedChanged = Math.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > SpeedDiffTolerance;
            if (lastMovementState == MovementState.Dashing) keepMomentum = true;

            if (desiredMoveSpeedChanged)
            {
                if (keepMomentum)
                {
                    if(smoothLerpCoroutine != null)
                        StopCoroutine(smoothLerpCoroutine);
                    smoothLerpCoroutine = StartCoroutine(SmoothlyLerpMoveSpeed());
                }
                else
                {
                    if(smoothLerpCoroutine != null)
                        StopCoroutine(smoothLerpCoroutine);
                    
                    moveSpeed = desiredMoveSpeed + speedBoost;
                }
            }

            lastDesiredMoveSpeed = desiredMoveSpeed;
            lastMovementState = currentState;
        }

        private void GetInput()
        {
            Vector2 moveVector = InputManager.Instance.GetMovementVectorNormalized();
            horizontalInput = moveVector.x;
            verticalInput = moveVector.y;
        }

        private void MovePlayer()
        {
            if (currentState == MovementState.Dashing) return;
            
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * (moveSpeed * movementData.SlopeSpeedMultiplier), ForceMode.Force);

                if (rb.velocity.y > 0)
                {
                    rb.AddForce(Vector3.down * movementData.SlopeDownwardForce, ForceMode.Force);
                }
            }
            
            if(isGrounded)
                rb.AddForce(moveDirection.normalized * (moveSpeed * movementData.SpeedMultiplier), ForceMode.Force);
            else
                rb.AddForce(moveDirection.normalized * (moveSpeed * movementData.SpeedMultiplier * movementData.AirMultiplier), ForceMode.Force);

            rb.useGravity = !OnSlope();

            if (isGrounded)
            {
                if(horizontalInput != 0 || verticalInput != 0)
                    playerFootsteps.TryPlayFootstepSounds();
            }
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

            if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
                rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }

        private void Jump()
        {
            exitingSlope = true;
            
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            rb.AddForce(transform.up * movementData.JumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
            exitingSlope = false;
        }

        public void HandleJumpAction()
        {
            if (!isMoving) return;
            
            if (readyToJump && isGrounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), movementData.JumpCooldown);
            }
        }

        public void HandleDashAction()
        {
            if (!isMoving) return;
            if (!canDash) return;
            
            dashHandler.Dash();
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

        private IEnumerator SmoothlyLerpMoveSpeed()
        {
            float time = 0;
            float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
            float startValue = moveSpeed;

            float boostFactor = speedChangeFactor;

            while (time < difference)
            {
                moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
                time += Time.deltaTime * boostFactor;

                yield return null;
            }

            moveSpeed = desiredMoveSpeed + speedBoost;
            speedChangeFactor = 1f;
            keepMomentum = false;
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

        public void UpdateSpeedBoostFromStats()
        {
            Stat speedBoostStat = statsHandler.GetStat(speedBoostStatType);
            if (speedBoostStat == null) return;

            float baseSpeedBoost = speedBoostStat.Value;
            speedBoost = baseSpeedBoost;
        }

        public void ModifySpeed(float modifier)
        {
            moveSpeed += modifier;
        }

        public void EnableMovement()
        {
            rb.isKinematic = false;
            isMoving = true;
        }

        public void DisableMovement()
        {
            rb.isKinematic = true;
            isMoving = false;
        }

        public void EnableSlowWalking()
        {
            isWalkingSlowly = true;
        }
        
        public void DisableSlowWalking()
        {
            isWalkingSlowly = false;
        }
        
        public void EnableDash()
        {
            canDash = true;
        }
        
        public void DisableDash()
        {
            canDash = false;
        }
    }
}