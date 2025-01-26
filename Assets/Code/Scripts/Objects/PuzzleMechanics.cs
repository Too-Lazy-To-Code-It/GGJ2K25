using TMPro;
using UnityEngine;

public class PuzzleMechanics : MonoBehaviour
{
    [SerializeField] GameObject PlatformTatla3;
    public float speed = 2f;       // Speed of movement
    private bool shouldMove = false;
    Vector3 targetPosition; // Flag to control when the object should move
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // Move the GameObject towards the target position
            PlatformTatla3.transform.position = Vector3.MoveTowards(PlatformTatla3.transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the GameObject has reached the target position
            if (Vector3.Distance(PlatformTatla3.transform.position, targetPosition) < 0.01f)
            {
                shouldMove = false; // Stop moving
                Debug.Log("Reached the target position!");
                Destroy(this);
            }
        }
    }

    public void PlatformUp(float _newPosition)
    {
        Debug.Log("Target Hit!!");
        Vector3 newPosition = new Vector3(PlatformTatla3.transform.position.x, PlatformTatla3.transform.position.y + 2f, PlatformTatla3.transform.position.z);
       
        targetPosition = newPosition;
        shouldMove = true;

    }
}
