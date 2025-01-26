using UnityEngine;

public class TrampolineObstacleScript : MonoBehaviour
{
    [SerializeField] private float scaleMin = 0f;  // Minimum Y scale
    [SerializeField] private float scaleMax = 1.5f; // Maximum Y scale
    [SerializeField] private float speed = 2f; // Speed of the scaling loop

    private Vector3 initialScale;

    void Start()
    {
        // Store the original scale of the GameObject
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the oscillating scale factor using Mathf.PingPong
        float scaleY = Mathf.Lerp(scaleMin, scaleMax, Mathf.PingPong(Time.time * speed, 1));
        
        // Apply the scale while keeping the X and Z scale intact
        transform.localScale = new Vector3(initialScale.x, scaleY, initialScale.z);
    }

}
