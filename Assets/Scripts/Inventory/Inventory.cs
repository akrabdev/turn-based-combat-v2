using System;
using UnityEngine;

public class Inventory : ScriptableObject, IItemContainer
{
    // Inventory consits of ItemSlots Size 20 
    private ItemSlot[] itemSlots = new ItemSlot[20];

    public Action OnItemsUpdated = delegate { };

    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];


    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        // Look if item (to be added) exist in inventory
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // Slot not empty?
            if (itemSlots[i] != null)
            {
                // Slot with same item to be added?
                if (itemSlots[i] == itemSlot)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;
                    // enough space in the slot?
                    if (itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;
                        itemSlot.quantity = 0;

                        //invoke
                        OnItemsUpdated.Invoke();

                        return itemSlot;

                    }
                    // not enough space in slot but RemainingSpace > 0
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }

        }

        // Empty Inv or Add New Item
        for (int i = 0; i < itemSlots.Length; i++)
        {

            // Found Empty Slot?
            if (itemSlots[i] == null)
            {
                if (itemSlot.quantity <= itemSlot.item.MaxStack)
                {

                    itemSlots[i] = itemSlot;
                    itemSlot.quantity = 0;

                    //invoke 
                    OnItemsUpdated.Invoke();
                    return itemSlot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }

            }

        }

        //invoke
        OnItemsUpdated.Invoke();
        return itemSlot;



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
        // safety check: slotIndex in range;
        if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

        itemSlots[slotIndex] = new ItemSlot();

        // here we need to invoke event to update ui
        OnItemsUpdated.Invoke();
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null)
            {
                // Maching Item Found
                if (itemSlots[i].item == itemSlot.item)
                {
                    // Slot doesnt have enough quantity
                    if (itemSlots[i].quantity < itemSlot.quantity)
                    {
                        // decrement quantity of required item to be removed
                        itemSlot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                    }
                    // correct item with enough quantity
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;
                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();
                            // invoke
                            //TODO Refactor?
                            OnItemsUpdated.Invoke();
                            return;
                        }
                    }

                }

            }
        }
    }

    public void Swap(int indexOne, int indexTwo)
    {
        // Caching two slots to be swapped
        //firstSlor: Source
        ItemSlot firstSlot = itemSlots[indexOne];
        // secondSlot: Dest.
        ItemSlot secondSlot = itemSlots[indexTwo];

        // Dragging and releasing on the same spot .. Nothing happens
        if (firstSlot == secondSlot) { return; }

        // Dragging into empty slot?:
        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;
                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    // accumulate into second slot
                    itemSlots[indexTwo].quantity += firstSlot.quantity;
                    // clear firstslot
                    itemSlots[indexOne] = new ItemSlot();
                    //invoke
                    OnItemsUpdated.Invoke();
                    return;
                }
            }


        }
        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;
        //invoke
        OnItemsUpdated.Invoke();
        return;
    }

}
