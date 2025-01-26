using UnityEngine;

public class HammerObstaclesScript : MonoBehaviour
{
    public float speed = 1f; // Speed of the oscillation
    public float maxRotation = 60f;
    void Start()
    {
        
    }

   
    void Update()
    {
        float angle = Mathf.PingPong(Time.time * speed, maxRotation * 2) - maxRotation;

        // Apply the rotation
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
