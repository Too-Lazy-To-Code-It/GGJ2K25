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
                if(!other.gameObject.GetComponent<BubbleData>().hasItem )
                    other.gameObject.GetComponent<BubbleData>().hasItem = true;
            }
            if(other.gameObject.CompareTag("Enemy"))
                Destroy(other.gameObject);
            
            
            
        }
    }
}