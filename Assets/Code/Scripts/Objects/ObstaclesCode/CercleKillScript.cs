using UnityEngine;

public class CercleKillScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second for X-axis rotation
    [SerializeField] private float positionSpeed = 2f; // Speed of movement along Z-axis
    [SerializeField] private float positionMinZ = -1f; // Minimum Z position
    [SerializeField] private float positionMaxZ = 1f; // Maximum Z position

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;
    }

    void Update()
    {
        // Rotate continuously along the X-axis
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);

        // Calculate the oscillating Z position using Mathf.PingPong
        float zPosition = Mathf.Lerp(positionMinZ, positionMaxZ, Mathf.PingPong(Time.time * positionSpeed, 1));
        
        // Apply the new Z position while keeping X and Y unchanged
        transform.position = new Vector3(initialPosition.x, initialPosition.y, zPosition);
    }
}
