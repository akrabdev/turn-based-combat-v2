using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// abstract as it will pass some jobs to ex. Ammo Weapon Consumable
public abstract class InventoryItem : HotbarItem
{
    [Header("Item Data")]
    // Min prevent setting variable the specified value
    [SerializeField] [Min(0)] private int sellPrice = 1;
    [SerializeField] [Min(1)] private int maxStack = 1;


    public override string ColouredName
    {
        get
        {
            // TODO;
            return Name;
        }



    }
    //getters:
    public int SellPrice => sellPrice;
    public int MaxStack => maxStack;



}
