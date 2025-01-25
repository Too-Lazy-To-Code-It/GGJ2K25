using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int damage;
}
