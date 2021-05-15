using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    //Refs: 
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI areYouSureText = null;

    // Store slotIndex : Index of draggedItem
    private int slotIndex = 0;

    // MonoBehaviour.OnDisable() :
    /*
     This function is called when the behaviour becomes disabled.
     This is also called when the object is destroyed and can be used for any cleanup code. When scripts are 
     reloaded after compilation has finished, OnDisable will be called, followed by an OnEnable after the script has been loaded.
    */

    private void OnDisable() => slotIndex = -1;

    // Activate DestroyItemPanel
    // Called in Drag Handler
    public void Activate(ItemSlot itemSlot, int slotIndex)
    {

        this.slotIndex = slotIndex;

        areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColouredName}?";
        // Shows DesotryItemPanel
        gameObject.SetActive(true);
    }


    public void Destroy()
    {
        inventory.ItemContainer.RemoveAt(slotIndex);
        // Hides DesotryItemPanel
        gameObject.SetActive(false);
    }
}

