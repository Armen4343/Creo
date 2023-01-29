using System.Collections.Generic;
using General;
using UnityEngine;

public class InventorySystem : MonoBehaviour,IService
{
	[SerializeField] private InventoryUI _inventoryUI;

	private SerializedDictionary<string, InventoryItem> items = new SerializedDictionary<string, InventoryItem>(30);
	private List<InventoryItem> inventory = new List<InventoryItem>(30);

	public List<InventoryItem> Inventory => inventory;

	private void Awake() => SL.AddSingle(this,SetMode.Force);

	public InventoryItem GetItem(string id)
	{
		if (items.TryGetValue(id, out InventoryItem value))
		{
			return value;
		}
		return null;
	}

	public void AddItem(InventoryItemData itemData)
	{
		if (items.TryGetValue(itemData.id, out InventoryItem value))
		{
			value.AddToInventory();
			_inventoryUI.UpdateSlot(value);
		}
		else
		{
			InventoryItem newItem = new InventoryItem(itemData);
			inventory.Add(newItem);
			items.Add(itemData.id, newItem);
			_inventoryUI.AddSlot(newItem);
		}
	}

	public void RemoveItem(string id)
	{
		if (items.TryGetValue(id, out InventoryItem value))
		{
			value.RemoveFromInventory();
			_inventoryUI.UpdateSlot(value);

			if (value.stackSize == 0)
			{
				inventory.Remove(value);
				items.Remove(id);
				_inventoryUI.RemoveSlot(value);
			}
		}
	}
}