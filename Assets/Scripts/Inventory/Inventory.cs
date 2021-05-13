using UnityEngine;

public class Inventory : ScriptableObject, IItemContainer
{
    // Inventory consits of ItemSlots Size 20 
    private ItemSlot[] itemSlots = new ItemSlot[20];


    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        throw new System.NotImplementedException();
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            // Slot empty? 
            if (itemSlot.item == null) { continue; }
            // Not the Item?
            if (itemSlot.item != item) { continue; }

            totalCount += itemSlot.quantity;

        }

        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        // Arrays more memory efficient , Lists for dynamic..

        foreach (ItemSlot itemSlot in itemSlots)
        {
            // Slot empty? 
            if (itemSlot.item == null) { continue; }

            // Not the Item?
            if (itemSlot.item != item) { continue; }

            return true;

        }
        // didnt find in all item slots
        return false;
    }

    public void RemoveAt(int slotIndex)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        throw new System.NotImplementedException();
    }

    public void Swap(int indexOne, int indexTwo)
    {
        // Caching two Items to be swapped
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];

        // Dragging and releasing on the same spot
        if (firstSlot == secondSlot) { return; }
    }
}
