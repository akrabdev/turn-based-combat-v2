using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarItemDragHandler : ItemDragHandler
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            base.OnPointerUp(eventData);
            //release over nothing
            if (eventData.hovered.Count == 0)
            {
                // clearing the refernce of HotbarSlot the item was in
                (ItemSlotUI as HotbarSlot).SlotItem = null;
            }
        }
    }

}
