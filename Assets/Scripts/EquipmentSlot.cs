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
            if (slotItem.accessory.ID != EquipmentID)
            {
                EquipmentID = slotItem.accessory.ID;
                Amount = slotItem.accessory.quantity;
                EquipmentType = slotItem.accessory.itemtype;

                SpeedBoost = slotItem.accessory.SpeedBoost;
                JumpBoost = slotItem.accessory.JumpBoost;
                AirJumps = slotItem.accessory.AirJumps;
                RollStrength = slotItem.accessory.RollStrength;
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
