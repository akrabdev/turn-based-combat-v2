using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sits on the UI
public class Hotbar : MonoBehaviour
{
    //can resize in inspector
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];


    // can be used in like spellbook where to shift + click instead of dragging
    public void Add(HotbarItem itemToAdd)
    {
        foreach (HotbarSlot hotbarSlot in hotbarSlots)
        {
            if (hotbarSlot.AddItem(itemToAdd))
            {
                return;
            }
        }
    }
}
