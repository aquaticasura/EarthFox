using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    bool colliding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = Object.FindFirstObjectByType<InventoryController>();
    }
    private void Update()
    {
        colliding = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliding) return;
        colliding = true;
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    
                    Destroy(collision.gameObject);
                    item.PickUp();
                }
            }
        }
    }
}

