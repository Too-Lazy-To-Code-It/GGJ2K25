using System;
using UnityEngine;

namespace Code.Scripts.Objects
{
    public class CollectableManager : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            other.gameObject.GetComponent<BubbleData>().score++;
        }
    }
}