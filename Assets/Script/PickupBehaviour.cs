using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    private Item currentItem;

    public void DoPickup(Item item)
    {
        if (inventory.IsFull())
        {
            return;
        }

        inventory.AddItem(item.itemData);
        Destroy(item.gameObject);
    }

    public void AddItemToInventory()
    {
        inventory.AddItem(currentItem.itemData);
        Destroy(currentItem.gameObject);

        currentItem = null;
    }
}
