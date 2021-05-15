using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
// TODO Cooldowns?
public class HotbarSlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;
    private HotbarItem slotItem = null;


    public override HotbarItem SlotItem
    {
        get { return slotItem; }
        set { slotItem = value; UpdateSlotUI(); }

    }
    public bool AddItem(HotbarItem itemToAdd)
    {
        if (slotItem != null) { return false; }

        SlotItem = itemToAdd;
        return true;
    }
    // press key > listen input
    public void UseSlot(int index)
    {
        // loop through them problem if it doesnt exist there will be a problem 
        if (index != SlotIndex)
        {
            return;
        }

        //Use Item
    }

    // On Dropping on HotbarSlot
    public override void OnDrop(PointerEventData eventData)
    {
        // Make sure that the Item Drag is itemDragHandler
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if (itemDragHandler == null) { return; }

        // Casting to know what is the type of the item inventorySlot OR SpellSlot: Set  HotbarSlot : Swap

        InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
        if (inventorySlot != null)
        {
            SlotItem = inventorySlot.ItemSlot.item;
            return;
        }

        //TODO SPells

        HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
        if (hotbarSlot != null)
        {
            // Swap:
            HotbarItem oldItem = SlotItem;
            SlotItem = hotbarSlot.SlotItem;
            hotbarSlot.SlotItem = oldItem;
            return;
        }
    }

    public override void UpdateSlotUI()
    {
        if (slotItem == null)
        {
            // disable


            EnableSlotUI(false);
            return;
        }

        itemIconImage.sprite = SlotItem.Icon;
        EnableSlotUI(true);

        SetItemQuantityUI();

        //TODO UPDATE COOLDOWN

    }


    private void SetItemQuantityUI()
    {
        if (SlotItem is InventoryItem inventoryItem)
        {
            // cehcking if the player still have item referenced in the HotbarSlot:
            if (inventory.ItemContainer.HasItem(inventoryItem))
            {
                int quantityCount = inventory.ItemContainer.GetTotalQuantity(inventoryItem);
                // one item left doesnt show a number
                itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";

            }
            else
            {
                //Clear Slot if no more left
                SlotItem = null;
            }
        }
        // No Quanttiy for spells
        else
        {
            itemQuantityText.enabled = false;
        }
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }



}
