using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryItemData itemData;

    public void OnItemPickUp()
    {
        InventorySystem.shared.AddItem(itemData);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        OnItemPickUp();
    }
}
