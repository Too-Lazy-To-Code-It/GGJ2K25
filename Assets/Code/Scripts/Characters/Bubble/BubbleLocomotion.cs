using System;
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

        private void Awake()
        {
            _bubbleManager = GetComponent<BubbleManager>();
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

        }

        private void HandleGroundedMovement()
        {     GetAllMovements();
            _movementDirection = BubbleCam.Instance.transform.forward * verticalMovement;
            _movementDirection += BubbleCam.Instance.transform.right * horizontalMovement;
            _movementDirection.Normalize();
            _movementDirection.y = 0;

            if (PlayerInputManager.Instance.moveAmount > 0.5f)
            {
                Vector3 horizontal= Vector3.ProjectOnPlane(_movementDirection, Vector3.up);
                _bubbleManager.rb.AddForce(horizontal * movingSpeed, ForceMode.Force);
    
                if (_bubbleManager.rb.linearVelocity.magnitude > maximumSpeed)
                {
                    Vector3 clampedVelocity = _bubbleManager.rb.linearVelocity.normalized * maximumSpeed;
                    _bubbleManager.rb.linearVelocity = new Vector3(clampedVelocity.x, _bubbleManager.rb.linearVelocity.y, clampedVelocity.z);
                }
            }
            else 
            {
                Vector3 horizontalDrag = Vector3.ProjectOnPlane(_bubbleManager.rb.linearVelocity, Vector3.up);
                _bubbleManager.rb.AddForce(-horizontalDrag * movingSpeed, ForceMode.Force);
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
    }
}