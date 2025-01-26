using Code.Scripts.Characters.Bubble;
using Code.Scripts.ScriptableObject;
using UnityEngine;

namespace Code.Scripts.Objects
{
    public class ItemManager : MonoBehaviour
    {
        ItemData _itemData;
        private Transform _absorbTransform;
        private BubbleManager _currentBubbleManager; 
        private float pickupCooldown = 0f;




        private void OnCollisionEnter(Collision other)
        {
            if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))) return;
            if (other.gameObject.CompareTag("Player"))
            {
                var bubbleManager = other.gameObject.GetComponent<BubbleManager>();
                if (!bubbleManager.bubbleData.hasItem  && pickupCooldown <= 0f )
                {
                    
                    
                    bubbleManager.bubbleData.hasItem = true;
                    _currentBubbleManager = bubbleManager;
                    _absorbTransform = other.gameObject.transform.Find("Absorb");
                    gameObject.transform.SetParent(_absorbTransform);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                }
            }

            if (other.gameObject.CompareTag("Enemy"))
                Destroy(other.gameObject);
        }

        public void ShootItem(Vector3 direction, float force)
        {Debug.Log("i am here");
            pickupCooldown = 2f;
            if (_absorbTransform == null) return;
            transform.SetParent(null);
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            
            
            // Apply force to shoot the item
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
        
        private void Update()
        {
            handleShooting();
        }
        public void  handleShooting()
        { 
            if (PlayerInputManager.Instance.shootInput) 
        {
            PlayerInputManager.Instance.shootInput = false;
            Vector3 shootDirection = transform.forward; 
            float shootForce = 10f;
            GetComponent<ItemManager>().ShootItem(shootDirection, shootForce);
            _currentBubbleManager.bubbleData.hasItem = false;
                
        }
            if (pickupCooldown > 0f)
            {
                pickupCooldown -= Time.deltaTime;
                pickupCooldown = Mathf.Clamp(pickupCooldown, 0f, float.MaxValue);
            }
        }
    }
}