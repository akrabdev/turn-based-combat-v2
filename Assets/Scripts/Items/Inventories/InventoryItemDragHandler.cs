using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : ItemDragHandler
{

    //Refs:
    [SerializeField] private ItemDestroyer itemDestroyer = null;

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            base.OnPointerUp(eventData);

            if (eventData.hovered.Count == 0)
            {
                //destroy item or drop item
                InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);


            }
        }
    }


}
