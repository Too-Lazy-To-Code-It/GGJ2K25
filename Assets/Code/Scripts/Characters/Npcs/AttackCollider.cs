﻿using Code.Scripts.Characters.Bubble;
using UnityEngine;

namespace Code.Scripts.Characters.Npcs
{
    public class AttackCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<BubbleManager>().bubbleData.heartLevel--;
            }
        }
    }
}