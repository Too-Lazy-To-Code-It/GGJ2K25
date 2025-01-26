using System.Collections;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject panelLoading, panelMenu; // Reference to the Panel GameObject
    public float delay = 2f; // Time in seconds before deactivating
                             // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (panelLoading != null)
        {
            panelLoading.SetActive(true);
            panelMenu.SetActive(false); 
            // Start the coroutine to deactivate the panel
            StartCoroutine(DeactivateAfterDelay());
        }
        else
        {
            Debug.LogError("No panel assigned to deactivate!");
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Deactivate the panel
        panelLoading.SetActive(false);
        panelMenu.SetActive(true);
        Debug.Log("Panel deactivated!");
    }
}
