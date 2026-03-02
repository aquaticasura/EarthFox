using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Scriptable Objects/Accessory")]
public class Accessory : ScriptableObject
{
    public int ID;
    public string Name;
    public int quantity = 1;
    public string itemtype;
    public float SpeedBoost;
    public float JumpBoost;
    public int AirJumps;
    public float RollStrength;
    public Sprite accessorysprite;
}
