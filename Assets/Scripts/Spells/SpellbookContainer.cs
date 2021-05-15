using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellbookContainer : SSpellContainer
{
    private SpellbookItemSlot[] spellSlots = new SpellbookItemSlot[0];

    public Action OnItemsUpdated = delegate { };
    // Constructor
    public SpellbookContainer(int size) => spellSlots = new SpellbookItemSlot[size];

    public SpellbookItemSlot GetSlotByIndex(int index) => spellSlots[index];


    public SpellbookItemSlot AddItem(SpellbookItemSlot itemSlot)
    {
        // Look if item (to be added) exist in inventory
        for (int i = 0; i < spellSlots.Length; i++)
        {
            // Slot not empty?
            if (spellSlots[i].item != null)
            {
                // Slot with same item to be added?
                if (spellSlots[i].item == itemSlot.item)
                {
                    return itemSlot;
                }
            }

        }

        // Empty Inv or Add New Item
        for (int i = 0; i < spellSlots.Length; i++)
        {
            // Found Empty Slot?
            if (spellSlots[i].item == null)
            {
                spellSlots[i] = new SpellbookItemSlot(itemSlot.item);
                OnItemsUpdated.Invoke();
                return itemSlot;
            }
        }
        //invoke
        OnItemsUpdated.Invoke();
        return itemSlot;
    }

    // Update is called once per frame
    public bool HasItem(Spell spell)
    {
        // Arrays more memory efficient , Lists for dynamic..

        foreach (SpellbookItemSlot itemSlot in spellSlots)
        {
            // Slot empty? 
            if (itemSlot.item == null) { continue; }

            // Not the Item?
            if (itemSlot.item != spell) { continue; }

            return true;

        }
        // didnt find in all item slots
        return false;
    }

    //public void RemoveAt(int slotIndex)
    //{
    //    // safety check: slotIndex in range;
    //    if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

    //    itemSlots[slotIndex] = new SpellbookItemSlot();

    //    // here we need to invoke event to update ui
    //    OnItemsUpdated.Invoke();
    //}

}
