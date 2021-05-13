using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// struct uses values not references , we dont need class here
public struct ItemSlot
{
    public InventoryItem item;
    public int quantity;

    // constructor 
    public ItemSlot(InventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    // Override ==: To compare if two slots have same item
    public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }
    public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }

}
