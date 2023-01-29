using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public string id { get; private set; }
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private GameObject _itemCountGameObject;

    public void Set(InventoryItem inventoryItem)
    {
        id = inventoryItem.data.id;
        _icon.sprite = inventoryItem.data.icon;
        _itemName.text = inventoryItem.data.itemName;
        _itemCount.text = inventoryItem.stackSize.ToString();
        if (inventoryItem.stackSize <= 1)
        {
            _itemCountGameObject.SetActive(false);
        }
    }

    public void UpdateCount(InventoryItem inventoryItem)
    {
        _itemCount.text = inventoryItem.stackSize.ToString();
        if (inventoryItem.stackSize > 1)
        {
            _itemCountGameObject.SetActive(true);
        }
    }

    public void Remove()
    {
        var slot = InventorySystem.shared.Inventory.Find(t => t.data.id == id);
        if (slot != null)
        {
            InventorySystem.shared.RemoveItem(slot.data);
        }
        Destroy(gameObject);
    }
}
