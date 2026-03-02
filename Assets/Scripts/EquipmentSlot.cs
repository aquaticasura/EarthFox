using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{

    public Slot slot;
    public string EquipmentName;
    public int EquipmentID;
    public int Amount;
    public string EquipmentType;
    public float SpeedBoost;
    public float JumpBoost;
    public int AirJumps;
    public float RollStrength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (slot.currentItem != null)
        {
            Item slotItem = slot.currentItem.GetComponent<Item>();
            if (slotItem.ID != EquipmentID)
            {
                EquipmentID = slotItem.ID;
                Amount = slotItem.quantity;
                EquipmentType = slotItem.itemtype;

                SpeedBoost = slotItem.SpeedBoost;
                JumpBoost = slotItem.JumpBoost;
                AirJumps = slotItem.AirJumps;
                RollStrength = slotItem.RollStrength;
                Debug.Log(EquipmentID);


            }
        }
        else
        {
            EquipmentID = 0;
            return;
        }
    }
}
