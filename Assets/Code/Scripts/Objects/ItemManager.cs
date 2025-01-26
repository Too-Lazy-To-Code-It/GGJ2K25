using Code.Scripts.Characters.Bubble;
using Code.Scripts.ScriptableObject;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Objects
{
    public class ItemManager : MonoBehaviour
    {
        ItemData _itemData;
        private Transform _absorbTransform;
        private BubbleManager _currentBubbleManager;

        private Vector3 initialPosition;  // Initial position of the object
        private Quaternion initialRotation; // Initial rotation of the object

        private void Start()
        {

                initialPosition = this.transform.position;
                initialRotation = this.transform.rotation;
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))) return;
            if (other.gameObject.CompareTag("Player"))
            {
                var bubbleManager = other.gameObject.GetComponent<BubbleManager>();
                if (!bubbleManager.bubbleData.item  )
                {
                    
                 
                    bubbleManager.bubbleData.item = gameObject;
                    _currentBubbleManager = bubbleManager;
                    _absorbTransform = other.gameObject.transform.Find("Absorb");
                    gameObject.transform.SetParent(_absorbTransform);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                     
                }
            }


            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                
            }

        }

        public void ShootItem(Vector3 direction, float force)
        {
            if (_absorbTransform == null) return;
            transform.SetParent(null);
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.AddForce(direction * force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Target"))
            {
                other.gameObject.transform.parent.gameObject.GetComponent<PuzzleMechanics>().PlatformUp(other.transform.position.y);

                Respawn();
            }

            if (other.gameObject.CompareTag("Respawn"))
            {
                Respawn();
                Destroy(this.gameObject);
                Debug.Log("Respawning the box");
            }
        }

        void Respawn()
        {
            //this.gameObject.transform.position = initialPosition;
            //this.gameObject.transform.rotation = initialRotation;

            GameObject _GO = Instantiate(this.gameObject, initialPosition, initialRotation);
            Rigidbody _r = _GO.GetComponent<Rigidbody>();
            Destroy(_r);




        }

    }
}