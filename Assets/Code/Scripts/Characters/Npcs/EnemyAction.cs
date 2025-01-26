using Code.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyAction : MonoBehaviour
    {
        [HideInInspector] public EnemyManager manager;
        private NavMeshAgent _agent;

        [SerializeField] private float meleeAttackRange = 2f; // Melee attack range
        private GameObject attackCollider;                   // Auto-created collider

        private void Awake()
        {
            manager = GetComponent<EnemyManager>();
            _agent = GetComponent<NavMeshAgent>();

            CreateAttackCollider(); // Automatically create the collider
        }

        private void CreateAttackCollider()
        {
            // Create a new GameObject as the attack collider
            attackCollider = new GameObject("AttackCollider");
            attackCollider.transform.SetParent(transform); // Attach it to the enemy
            attackCollider.transform.localPosition = Vector3.zero; // Position it at the enemy's center
            
            SphereCollider sphereCollider = attackCollider.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = meleeAttackRange;
            Rigidbody rb = attackCollider.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            attackCollider.SetActive(false);
            attackCollider.AddComponent<AttackCollider>();
        }

        public void HandleState()
        {
            if (manager.enemyData.state == State.Chasing)
            {
                _agent.SetDestination(manager._player.transform.position);

                float distanceToPlayer = Vector3.Distance(transform.position, manager._player.transform.position);
                if (distanceToPlayer <= meleeAttackRange && manager.enemyData.type == EnemyType.Melee)
                {
                    manager.enemyData.state = State.Attacking;
                }
            }

            if (manager.enemyData.state == State.Attacking)
            {
                AttackPlayer();
            }
        }

        private void AttackPlayer()
        {
            _agent.ResetPath(); // Stop moving while attacking

            // Enable attack collider
            attackCollider.SetActive(true);

            // Optionally add attack animations and logic here
            Invoke(nameof(DisableAttackCollider), 0.5f); // Disable collider after attack duration
        }

        private void DisableAttackCollider()
        {
            attackCollider.SetActive(false);
            manager.enemyData.state = State.Chasing; // Return to chasing after attack
        }
    }
}
