using UnityEngine;

namespace Code.Scripts.ScriptableObject
{
    public enum EnemyType
    {
        Ranged,
        Melee
    }

    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : UnityEngine.ScriptableObject
    {
        public string enemyName;
        public float speed;
        public EnemyType type;
    
    }
}