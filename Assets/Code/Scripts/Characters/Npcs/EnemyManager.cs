using System;
using Code.Scripts.ScriptableObject;
using UnityEngine;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyManager : MonoBehaviour
    {
        //public EnemyData enemyData;
        private EnemyAction _enemyAction;
        private EnemyLocomotion _enemyLocomotion;

        private void Awake()
        {
            //_enemyAction = GetComponent<EnemyAction>();
            _enemyLocomotion = GetComponent<EnemyLocomotion>();
        }

        private void Update()
        {
            _enemyLocomotion.idling();
        }
    }
}