using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// struct uses values not references , we dont need class here
[Serializable]
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

    public override bool Equals(object obj)
    {
        return obj is ItemSlot slot &&
               EqualityComparer<InventoryItem>.Default.Equals(item, slot.item) &&
               quantity == slot.quantity;
    }

    public override int GetHashCode()
    {
        int hashCode = -301187666;
        hashCode = hashCode * -1521134295 + EqualityComparer<InventoryItem>.Default.GetHashCode(item);
        hashCode = hashCode * -1521134295 + quantity.GetHashCode();
        return hashCode;
    }

    // Override ==: To compare if two slots have same item
    public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }
    public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }

}
