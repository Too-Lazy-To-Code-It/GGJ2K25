
using Code.Scripts.Characters.Bubble;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text instructions;
    public GameObject dashIcon, jumpIcon;

    public BubbleLocomotion bubbleLocomotion;

    bool jumpOnCD, dashOnCD = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tuto("Use Z Q S D (Keyboard) / Left Joystick to move ");
    }

    // Update is called once per frame
    void Update()
    {
        if ((bubbleLocomotion.isJumping)&&(!jumpOnCD))
        {
            jumpOnCD = true;
            
            JumpCD(bubbleLocomotion.jumpTimeLeft);
        }
        
        if ((bubbleLocomotion.isDashing) && (!dashOnCD))
        {
            dashOnCD = true;
            
            DashCD(bubbleLocomotion.dashTimeLeft);
        }
    }

    public void DashCD(float _x)
    {
        Image _DashImg = dashIcon.GetComponent<Image>();
        TMP_Text _DashText = dashIcon.GetComponentInChildren<TMP_Text>();
        Color currentColor = _DashImg.color;
        currentColor.a = 0.5f;
        _DashImg.color = currentColor;

        _DashText.text = _x.ToString("0.00");

        StartCoroutine(CountdownCoroutine(_x, _DashText, _DashImg));
        
    }
    
    public void JumpCD(float _x)
    {
        
        Image _JumpImg = jumpIcon.GetComponent<Image>();
        TMP_Text _JumpText = jumpIcon.GetComponentInChildren<TMP_Text>();
        Color currentColor = _JumpImg.color;
        currentColor.a = 0.5f;
        _JumpImg.color = currentColor;

        _JumpText.text = _x.ToString("0.00");

        StartCoroutine(CountdownCoroutine(_x, _JumpText, _JumpImg));
       


    }

    IEnumerator CountdownCoroutine(float countdownTime, TMP_Text countdownText, Image _img)
    {

        float currentTime = countdownTime;

        // Keep looping until the countdown reaches 0
        while (currentTime > 0)
        {
            // Update the text with the current time, formatted to 1 decimal place
            countdownText.text = currentTime.ToString("F1");

            // Wait for 1 frame
            yield return null;

            // Decrease the time by the time passed since the last frame
            currentTime -= Time.deltaTime;

            // Ensure it doesn't go below 0
            if (currentTime < 0)
            {
                currentTime = 0;
            }
        }

        // Set the final value to 0 when done
        countdownText.text = "";
        resetIcons(countdownText, _img);

    }

    private void resetIcons(TMP_Text countdownText, Image _img)
    {
        Color currentColor = _img.color;
        currentColor.a = 1f;
        _img.color = currentColor;
        countdownText.text = "";

        if (_img.gameObject.name == "Jump")
        {
            jumpOnCD = false;
        }
        else
        {
            dashOnCD = false;
        }
    }


    public void Tuto(string _text)
    {
        instructions.text = _text;
    }
    //"Press Space (Keyboard) / A (Gamepad) to Jump";
    //public void TutoDash()
    //{
    //    instructions.text = "Press LShift (Keyboard) / B (Gamepad) to Jump";
    //}

    //public void TutoShoot()
    //{
    //    instructions.text = "Absorb the box by getting close to it";

    //    instructions.text = "Press Ctrl (Keyboard) / X (Gamepad) to Shoot the box into the Target";
    //}
}
