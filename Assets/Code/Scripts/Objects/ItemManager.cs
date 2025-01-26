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
                Destroy(other.gameObject);
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

    }
}