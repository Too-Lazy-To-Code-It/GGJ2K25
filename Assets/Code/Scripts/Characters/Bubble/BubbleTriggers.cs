using UnityEngine;

public class BubbleTriggers : MonoBehaviour
{

    UIManager managerUi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        managerUi = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "JumpTuto")
            managerUi.Tuto("Press Space (Keyboard) / A (Gamepad) to Jump");
        if (other.gameObject.name == "DashTuto")
            managerUi.Tuto("Press LShift (Keyboard) / B (Gamepad) to Jump");
        if (other.gameObject.name == "ShootTuto")
            managerUi.Tuto("Absorb the box by getting close to it \n Press Ctrl (Keyboard) / X (Gamepad) to Shoot the box into the Target");
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "JumpTuto")
            managerUi.Tuto("");
        if (other.gameObject.name == "DashTuto")
            managerUi.Tuto("");
        if (other.gameObject.name == "ShootTuto")
            managerUi.Tuto("");
    }
}
