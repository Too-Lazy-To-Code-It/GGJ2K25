using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Characters.Bubble
{
    public class BubbleManager : CharacterManager
    { 
        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Update()
        {
            
        }

        private void LateUpdate()
        {
            BubbleCam.Instance.HandleAllCameraActions();
        }
    }
}