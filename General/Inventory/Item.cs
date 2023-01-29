using General;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryItemData itemData;
    private InventorySystem inventorySystem;

    private void Start() => SL.GetSingle(out inventorySystem);

    private void OnItemPickUp()
    {
        inventorySystem.AddItem(itemData);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        OnItemPickUp();
    }
}