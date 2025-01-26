using System;
using UnityEngine;

namespace Code.Scripts.Characters
{
    public class CharacterManager : MonoBehaviour
    {
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Merge Happening here");
            }
            if (other.gameObject.CompareTag("Trap"))
            {
                Debug.Log("esfjsedfjsdlfjosdjfosod");
                gameObject.GetComponent<BubbleData>().heartLevel--;
                if (other.gameObject.GetComponent<BubbleData>().heartLevel <= 0)
                {
                    Debug.Log("Game Over Screen");
                }

                
            }
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}