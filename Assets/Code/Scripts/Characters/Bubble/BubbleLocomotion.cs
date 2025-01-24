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

        private void HandleAllMovements()
        {
            
        }

        private void HandleGroundedMovement()
        {
            
        }

        private void HandleRotations()
        {
            
        }
    }
}