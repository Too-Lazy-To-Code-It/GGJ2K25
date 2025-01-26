using System;
using Code.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyAction : MonoBehaviour
    {
        [HideInInspector]public EnemyManager manager;
        NavMeshAgent _agent;

        private void Awake()
        {
            manager = GetComponent<EnemyManager>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public void HandleState()
        {
            if (manager.enemyData.state == State.Chasing)
                _agent.SetDestination(manager._player.transform.position);

        }
        
    }
}