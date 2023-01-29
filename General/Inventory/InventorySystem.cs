using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	[SerializeField] private InventoryUI _inventoryUI;

    private Dictionary<InventoryItemData, InventoryItem> items;
	private List<InventoryItem> inventory = new List<InventoryItem>(30);

	public static InventorySystem shared;

	public List<InventoryItem> Inventory => inventory;

	private void Awake()
	{
		items = new Dictionary<InventoryItemData, InventoryItem>();

		if (shared != null && shared != this)
		{
			Destroy(this);
		}
		else
		{
			shared = this;
			//DontDestroyOnLoad(shared);
		}
	}

	public InventoryItem GetItem(InventoryItemData itemData)
	{
		if (items.TryGetValue(itemData, out InventoryItem value))
		{
			return value;
		}
		return null;
	}

	public void AddItem(InventoryItemData itemData)
	{
		if (items.TryGetValue(itemData, out InventoryItem value))
		{
			value.AddToInventory();
			_inventoryUI.UpdateSlot(value);
		}
		else
		{
			InventoryItem newItem = new InventoryItem(itemData);
			inventory.Add(newItem);
			items.Add(itemData, newItem);
			_inventoryUI.AddSlot(newItem);
		}
	}

	public void RemoveItem(InventoryItemData itemData)
	{
		if (items.TryGetValue(itemData, out InventoryItem value))
		{
			value.RemoveFromInventory();
			_inventoryUI.UpdateSlot(value);

			if (value.stackSize == 0)
			{
				inventory.Remove(value);
				items.Remove(itemData);
				_inventoryUI.RemoveSlot(value);
			}
		}
	}
}
