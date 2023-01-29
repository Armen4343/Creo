using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Slot _slot;
    [SerializeField] private Transform _contentTransform;

    private List<Slot> _slots = new List<Slot>(30);

    public void AddSlot(InventoryItem inventoryItem)
    {
        Slot slot = Instantiate(_slot, _contentTransform);
        slot.Set(inventoryItem);
        _slots.Add(slot);
    }

    public void UpdateSlot(InventoryItem inventoryItem)
    {
        var slot = _slots.Find(t => t.id == inventoryItem.data.id);
        if (slot != null)
        {
            slot.UpdateCount(inventoryItem);
        }
    }

    public void RemoveSlot(InventoryItem inventoryItem)
    {
        var slot = _slots.Find(t => t.id == inventoryItem.data.id);
        if (slot != null)
        {
            _slots.Remove(slot);
            Destroy(slot);
        }
    }
}
