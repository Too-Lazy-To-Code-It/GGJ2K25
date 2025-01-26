using UnityEngine;

namespace Code.Scripts.Characters.Npcs
{
    public class Projectile : MonoBehaviour
    {
        public float damage = 10f;
        public float lifeTime = 5f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                    other.gameObject.SetActive(false);
            }
        }
    }
}