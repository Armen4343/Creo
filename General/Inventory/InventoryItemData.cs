using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
}
