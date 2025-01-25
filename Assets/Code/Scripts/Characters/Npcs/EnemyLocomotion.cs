using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyLocomotion : MonoBehaviour
    {
        private float _roamRadius=15f;
        private float _idleTime = 1f;
        
        private NavMeshAgent _agent;
        private float _idleTimer;
        private Transform _target;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
        }

        private void Start()
        {
            SetNewRandomDestination();
            
        }

        public void idling()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                _idleTimer += Time.deltaTime;

                if (_idleTimer >= _idleTime)
                {
                    SetNewRandomDestination();
                    _idleTimer = 0f;
                }
            }
            
        }

        private void SetNewRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere*_roamRadius;
            randomDirection+=transform.position;
            
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomDirection, out hit, _roamRadius, NavMesh.AllAreas))
                _agent.SetDestination(hit.position);
        }
    }
}