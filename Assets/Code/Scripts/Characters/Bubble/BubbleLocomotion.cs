using System;
using System.Collections;
using Code.Scripts.Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Characters.Bubble
{
    public class BubbleLocomotion : CharacterLocomotion
    {
        private BubbleManager _bubbleManager;
        
        [HideInInspector]public float verticalMovement;
        [HideInInspector]public float horizontalMovement;
        [HideInInspector]public float moveAmount;
        
        [Header("Movement Settings")] 
        private Vector3 _targetRotationDirection;
        private Vector3 _movementDirection;
        [SerializeField] private float movingSpeed=5f;
        [SerializeField] private float maximumSpeed = 4.5f;
        [SerializeField] private float rotationSpeed = 20f;
        private float _dashCooldown;
        private float _jumpCooldown;
        private float _jumpResistanceCd;

        public Vector3 defaultPosition;
        
        [Header("States Settings")]
        private bool isDashing = false;
        private bool isJumping = false;
        private bool isGrounded;
        
        
        private float dashTimeLeft = 0f;
        private float jumpTimeLeft = 0f;
        private Vector3 dashVelocity;
        private Vector3 jumpVelocity;
        
        private float dashForce = 10f;
        private float dashDuration = 0.8f;
        private float jumpForce = 7.5f;
        private float jumpDuration = 0.8f;

        private void Awake()
        {
            _bubbleManager = GetComponent<BubbleManager>();
        }

        private void Start()
        {
            defaultPosition = transform.position;
        }

        private void GetAllMovements()
        {
            verticalMovement = PlayerInputManager.Instance.verticalInput;
            horizontalMovement = PlayerInputManager.Instance.horizontalInput;
        }

        public void HandleAllMovements()
        {
            HandleGroundedMovement();
            HandleRotations();
            HandleDash();
            HandleJump();
            handleShooting();
        }

        private void HandleGroundedMovement()
        {     GetAllMovements();
            _movementDirection = BubbleCam.Instance.transform.forward * verticalMovement;
            _movementDirection += BubbleCam.Instance.transform.right * horizontalMovement;
            _movementDirection.Normalize();
            _movementDirection.y = 0;
            
            if(_dashCooldown>0f)
                _dashCooldown -= Time.deltaTime;

            if (!isDashing && PlayerInputManager.Instance.moveAmount > 0.5f)
            {
                Vector3 movementForce = new Vector3(_movementDirection.x, 0, _movementDirection.z) * movingSpeed;
                _bubbleManager.rb.AddForce(movementForce, ForceMode.Force);

                Vector3 flatVelocity = new Vector3(_bubbleManager.rb.linearVelocity.x, 0, _bubbleManager.rb.linearVelocity.z);
                if (flatVelocity.magnitude > maximumSpeed)
                {
                    flatVelocity = flatVelocity.normalized * maximumSpeed;
                    _bubbleManager.rb.linearVelocity = new Vector3(flatVelocity.x, _bubbleManager.rb.linearVelocity.y, flatVelocity.z);
                }
            }
            else if (!isDashing)
            {
                Vector3 decelerationForce = new Vector3(_bubbleManager.rb.linearVelocity.x, 0, _bubbleManager.rb.linearVelocity.z) * -movingSpeed;
                _bubbleManager.rb.AddForce(decelerationForce, ForceMode.Force);
            }
            
        }

        private void HandleRotations()
        {
            _targetRotationDirection = Vector3.zero;
            _targetRotationDirection = BubbleCam.Instance.cameraObject.transform.forward * verticalMovement;
            _targetRotationDirection += BubbleCam.Instance.cameraObject.transform.right * horizontalMovement;
            _targetRotationDirection.Normalize();
            _targetRotationDirection.y = 0;

            if (_targetRotationDirection == Vector3.zero)
                _targetRotationDirection = transform.forward;
            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation =
                Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
            
        }

        private void HandleDash()
        {
            if (PlayerInputManager.Instance.dashInput  )
            {
                PlayerInputManager.Instance.dashInput = false;
                if (!isDashing && _dashCooldown<=0f)
                {
                   
                
                    isDashing = true;
                    dashTimeLeft = dashDuration;
                
                    Vector3 dashDirection = new Vector3(_movementDirection.x, 0, _movementDirection.z).normalized;
                    dashVelocity = dashDirection * dashForce;


                    _bubbleManager.rb.linearDamping = 0;
                }
               
            }
            
            if (isDashing)
            {
                PerformDash();
                _dashCooldown = 1f;
            }
        }

        private void PerformDash()
        {
            if (dashTimeLeft > 0)
            {
                Vector3 currentVelocity = _bubbleManager.rb.linearVelocity;
                
                _bubbleManager.rb.linearVelocity = new Vector3(dashVelocity.x, currentVelocity.y, dashVelocity.z);

                dashTimeLeft -= Time.fixedDeltaTime;
            }
            else
            {
                isDashing = false;
                
                _bubbleManager.rb.linearDamping = Mathf.Lerp(_bubbleManager.rb.linearDamping, 1, Time.fixedDeltaTime * 5);
            }
        }
        private void HandleJump()
        {
            if (PlayerInputManager.Instance.jumpInput  )
            {
                PlayerInputManager.Instance.jumpInput = false;
                if (!isJumping && _jumpCooldown <= 0f)
                {
                    
                    Debug.Log("dumping original value: " + _bubbleManager.rb.linearDamping);
                    Debug.Log("Jump initiated.");

                    _jumpCooldown = 3f; // Reset jump cooldown
                    _jumpResistanceCd = 5f;


                    isJumping = true;
                    isGrounded = false;
                    _bubbleManager.rb.linearDamping = 1;
                    jumpTimeLeft = jumpDuration;
                    jumpVelocity = Vector3.up * jumpForce;


                    PerformJump();
                }
            }

            if (_jumpCooldown > 0f)
            {
                _jumpCooldown -= Time.deltaTime;
                _jumpCooldown = Mathf.Clamp(_jumpCooldown, 0, float.MaxValue);
            }
        }

        private void PerformJump()
        {
            Vector3 currentVelocity = _bubbleManager.rb.linearVelocity;
            _bubbleManager.rb.linearVelocity = new Vector3(currentVelocity.x, jumpVelocity.y, currentVelocity.z);

            Debug.Log("dumping change value  " + _bubbleManager.rb.linearDamping);
        }


        private void OnCollisionEnter(Collision collision)
        {
            // Ensure the ground has the "Ground" tag
            
                isGrounded = true;
                isJumping = false;
                Debug.Log("velocity  thenya" + _bubbleManager.rb.linearVelocity.y);
                if (_bubbleManager.rb.linearVelocity.y > 5 || _bubbleManager.rb.linearVelocity.y < 4)
                {
                    _bubbleManager.rb.linearVelocity = new Vector3(0,5f,0) ;
                }
               
                _bubbleManager.rb.linearDamping = 0; 
              
            
        }

        public void RestNow()
        {
            transform.position = defaultPosition;
        }

        public void  handleShooting()
        { 
            if (PlayerInputManager.Instance.shootInput && _bubbleManager.bubbleData.item) 
            {
                PlayerInputManager.Instance.shootInput = false;
                float shootForce = 10f;
                _bubbleManager.bubbleData.item.GetComponent<ItemManager>().ShootItem(_movementDirection,10f);
                
                _bubbleManager.bubbleData.item=null;
                
            }
        }
    }
}