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

        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}