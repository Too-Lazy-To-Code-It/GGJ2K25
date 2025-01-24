using System;
using UnityEngine;

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
        [SerializeField] private float movingSpeed=5.5f;
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
                Vector3 movePosition=_bubbleManager.rb.position + _movementDirection *(movingSpeed * Time.fixedDeltaTime);
                _bubbleManager.rb.MovePosition(movePosition);
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