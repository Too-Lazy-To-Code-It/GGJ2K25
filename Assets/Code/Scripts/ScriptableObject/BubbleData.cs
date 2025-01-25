using UnityEngine;

[CreateAssetMenu(fileName = "BubbleData", menuName = "Scriptable Objects/BubbleData")]
class BubbleData : ScriptableObject
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
    public bool HasItem;

    [Header("Heart Level")]
    [Range(0, 2)] 
    public int heartLevel;


    public void ResetData()
    {
        currentState = BubbleState.Idle;
        score = 0;
        HasItem = false;
        heartLevel = 1;
    }
}