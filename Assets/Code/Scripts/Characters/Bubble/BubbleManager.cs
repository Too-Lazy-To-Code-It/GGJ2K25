using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Characters.Bubble
{
    public class BubbleManager : CharacterManager
    {
        [HideInInspector]public Rigidbody rb;
        [HideInInspector]public BubbleLocomotion locomotion;
        public BubbleData bubbleData;
        public AudioSource audioSource;
 int reset = 1 ;
        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
            locomotion = GetComponent<BubbleLocomotion>();
            audioSource = GetComponent<AudioSource>();
            bubbleData.heartLevel = 1;
            bubbleData.item = null;
            rb.freezeRotation = true;
            
            
        }

        private void Update()
        {
            if (bubbleData.heartLevel <= 0 &&  reset == 1 )
            {
                audioSource.Play();
                gameObject.SetActive(false);
                reset = 0;

            }

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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Trap"))
            {
                Debug.Log("esfjsedfjsdlfjosdjfosod");
                bubbleData.heartLevel--;
                if (bubbleData.heartLevel <= 0)
                {
                    
                    Debug.Log("Game Over Screen");
                }

                
            }
        }
    }
}