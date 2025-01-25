﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Characters.Bubble
{
    public class BubbleManager : CharacterManager
    {
        [HideInInspector]public Rigidbody rb;
        [HideInInspector]public BubbleLocomotion locomotion;
        //public BubbleData bubbleData;
        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
            locomotion = GetComponent<BubbleLocomotion>();
            
            rb.freezeRotation = true;
            
        }

        private void Update()
        {
            if (PlayerInputManager.Instance.interactInput)
            {
                PlayerInputManager.Instance.interactInput = false;
                locomotion.RestNow();
            }
        }

        private void FixedUpdate()
        {
            locomotion.HandleAllMovements();
        }

        private void LateUpdate()
        {
            BubbleCam.Instance.HandleAllCameraActions();
        }
    }
}