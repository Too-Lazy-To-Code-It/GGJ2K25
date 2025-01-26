using UnityEngine;

public class HammerObstaclesScript : MonoBehaviour
{
    public float speed = 1f; // Speed of the oscillation
    public float maxRotation = 60f;
    Quaternion currentRotation;
    void Start()
    {
        // Get the current local rotation
        currentRotation = transform.localRotation;

    }

   
    void Update()
    {
        float angle = Mathf.PingPong(Time.time * speed, maxRotation * 2) - maxRotation;

        transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, angle);
    }
}
