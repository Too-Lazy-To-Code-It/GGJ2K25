using Code.Scripts.Characters.Bubble;
using Code.Scripts.ScriptableObject;
using UnityEngine;

namespace Code.Scripts.Objects
{
    public class ItemManager : MonoBehaviour
    {
        ItemData _itemData;


        private void OnCollisionEnter(Collision other)
        {
            if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))) return;
            if (other.gameObject.CompareTag("Player"))
            {
                if (!other.gameObject.GetComponent<BubbleManager>().bubbleData.hasItem)
                {
                    other.gameObject.GetComponent<BubbleManager>().bubbleData.hasItem = true;
                    Transform absorbTransform = other.gameObject.transform.Find("Absorb");
                    gameObject.transform.SetParent(absorbTransform);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                }
            }

            if (other.gameObject.CompareTag("Enemy"))
                Destroy(other.gameObject);
        }

        public void EatITem()
        {
        }
    }
}