using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Data/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public string obstacleName;
    public float pushForce;
    public float damage;
    public float health;

}
