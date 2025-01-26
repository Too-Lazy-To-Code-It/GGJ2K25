using Code.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Characters.Npcs
{
    public class EnemyAction : MonoBehaviour
    {
                [HideInInspector] public EnemyManager manager;
        private NavMeshAgent _agent;

        [SerializeField] private float meleeAttackRange = 2f; 
        [SerializeField] private float rangedAttackRange = 10f; 
        [SerializeField] private GameObject projectilePrefab;  
        [SerializeField] private Transform firePoint;         

        private GameObject attackCollider;
        private float rangedAttackCooldown = 1.5f;
        private float nextRangedAttackTime;

        private void Awake()
        {
            manager = GetComponent<EnemyManager>();
            _agent = GetComponent<NavMeshAgent>();

            if (manager.enemyData.type == EnemyType.Melee)
                CreateAttackCollider();
            if (manager.enemyData.type == EnemyType.Ranged)
                CreateFirePoint();
        }
        private void CreateFirePoint()
        {
            GameObject firePointObject = new GameObject("FirePoint");
            firePointObject.transform.SetParent(transform);
            firePointObject.transform.localPosition = new Vector3(0, 0.25f, 1.5f);
            firePointObject.transform.localRotation = Quaternion.identity;

            firePoint = firePointObject.transform;
        }
        private void CreateAttackCollider()
        {
            attackCollider = new GameObject("AttackCollider");
            attackCollider.transform.SetParent(transform);
            attackCollider.transform.localPosition = Vector3.zero;

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
                if (manager.enemyData.type == EnemyType.Melee && distanceToPlayer <= meleeAttackRange)
                {
                    manager.enemyData.state = State.Attacking;
                }
                else if (manager.enemyData.type == EnemyType.Ranged && distanceToPlayer <= rangedAttackRange)
                {
                    manager.enemyData.state = State.Attacking;
                }
            }

            if (manager.enemyData.state == State.Attacking)
            {
                if (manager.enemyData.type == EnemyType.Melee)
                {
                    AttackPlayerMelee();
                }
                else if (manager.enemyData.type == EnemyType.Ranged)
                {
                    AttackPlayerRanged();
                }
            }
        }

        private void AttackPlayerMelee()
        {
            _agent.ResetPath();

            attackCollider.SetActive(true);
            Invoke(nameof(DisableAttackCollider), 0.5f);
        }

        private void AttackPlayerRanged()
        {
            _agent.ResetPath(); 
            Vector3 direction = (manager._player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            if (Time.time >= nextRangedAttackTime)
            {
                FireProjectile();
                nextRangedAttackTime = Time.time + rangedAttackCooldown; 
            }
        }

        private void FireProjectile()
        {
            if (projectilePrefab != null && firePoint != null)
            {
                // Instantiate and fire the projectile
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (manager._player.transform.position - firePoint.position).normalized;
                    rb.linearVelocity = direction * 10f; 
                }
            }
        }

        private void DisableAttackCollider()
        {
            attackCollider.SetActive(false);
            manager.enemyData.state = State.Chasing;
        }
    }
    }

