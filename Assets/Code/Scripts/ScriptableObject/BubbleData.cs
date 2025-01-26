using UnityEngine;

[CreateAssetMenu(fileName = "BubbleData", menuName = "Data/BubbleData")]
public class BubbleData : ScriptableObject
{
    public enum BubbleState
    {
        Idle,
        Moving,
        Jumping,
        Dashing,
        
    }

    [Header("Bubble State")]
    public BubbleState currentState = BubbleState.Idle;
    public int score;
    public GameObject item;

    [Header("Heart Level")]
    [Range(0, 2)] 
    public int heartLevel;
    
}