using System;
using UnityEngine;
using UnityEngine.Events;

public class Spellbook : MonoBehaviour, ISpellContainer
{
    // [SerializeField] private VoidEvent onSpellbookItemsUpdated = null;

    [SerializeField] private int size = 4;
    [SerializeField] private UnityEvent onSpellbookSpellsUpdated = null;
    // for testing
    // [SerializeField] private SpellSlot testItemSlot = new SpellSlot();



    private SpellSlot[] spellSlots = new SpellSlot[0];


    public void Start()
    {
        spellSlots = new SpellSlot[size];
    }

    public SpellSlot AddItem(SpellSlot spellSlot)
    {
        // Look if item (to be added) exist in spellbook
        for (int i = 0; i < spellSlots.Length; i++)
        {
            // Slot not empty?
            if (spellSlots[i].spell != null)
            {
                // Slot with same item to be added?
                if (spellSlots[i] == spellSlot)
                {
                    return spellSlot;
                }
            }

        }

        // Empty Inv or Add New Item
        for (int i = 0; i < spellSlots.Length; i++)
        {
            // Found Empty Slot?
            if (spellSlots[i].spell == null)
            {
                spellSlots[i] = new SpellSlot(spellSlot.spell);
                onSpellbookSpellsUpdated.Invoke();
                return spellSlot;
            }
        }
        //invoke
        onSpellbookSpellsUpdated.Invoke();
        return spellSlot;
    }

    // Update is called once per frame
    public bool HasItem(Spell spell)
    {
        // Arrays more memory efficient , Lists for dynamic..

        foreach (SpellSlot spellSlot in spellSlots)
        {
            // Slot empty? 
            if (spellSlot.spell == null) { continue; }

            // Not the Item?
            if (spellSlot.spell != spell) { continue; }

            return true;

        }
        // didnt find in all item slots
        return false;
    }

    public SpellSlot GetSlotByIndex(int index)
    {

        return spellSlots[index];
    }

    //public void RemoveAt(int slotIndex)
    //{
    //    // safety check: slotIndex in range;
    //    if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

    //    itemSlots[slotIndex] = new SpellSlot();

    //    // here we need to invoke event to update ui
    //    onInventoryItemsUpdated.Raise();
    //}


    // public void OnEnable()
    // {
    //     SpellbookContainer.OnItemsUpdated += onSpellbookItemsUpdated.Raise;

    // }

    // public void OnDisable()
    // {
    //     SpellbookContainer.OnItemsUpdated -= onSpellbookItemsUpdated.Raise;
    // }
    // [ContextMenu("Test Add Spell")]
    // public void TestAdd()
    // {
    //     SpellbookContainer.AddItem(testItemSlot);
    // }

}
