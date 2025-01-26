using UnityEngine;

public class CercleKillScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second for world X-axis rotation
    [SerializeField] private float movementSpeed = 2f; // Speed of movement along local Z-axis
    [SerializeField] private float movementRange = 1f; // Distance the object moves back and forth along the Z-axis

    private float movementOffset;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        
        movementOffset = Mathf.PingPong(Time.time * movementSpeed, movementRange * 2) - movementRange;
        
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, movementOffset);
    }
}
