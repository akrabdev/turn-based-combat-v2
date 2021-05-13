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

}
