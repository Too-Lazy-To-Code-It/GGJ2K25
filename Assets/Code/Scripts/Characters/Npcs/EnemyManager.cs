using System;
using Code.Scripts.ScriptableObject;
using UnityEngine;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyData enemyData;
        private EnemyAction _enemyAction;
        private EnemyLocomotion _enemyLocomotion;
        public GameObject _player;

        private void Awake()
        {
            _enemyAction = GetComponent<EnemyAction>();
            _enemyLocomotion = GetComponent<EnemyLocomotion>();
            _player = GameObject.FindGameObjectWithTag("Player");
            enemyData.state = State.Idle;
        }

        void Update()
        {

            if (enemyData.state == State.Idle)
            {
                _enemyLocomotion.idling();
            }

            if (_enemyLocomotion.isPlayerClose(_player.transform.position))
            {
                enemyData.state = State.Chasing;
            }
            else
            {
                enemyData.state = State.Idle;
            }


            _enemyAction.HandleState();
        }


    }
}