using UnityEngine;


public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSlot itemSlot;

    public void Interact(GameObject other)

    {
        // is the thing the item interacting with got itemcontainer?
        // var itemContainer = other.GetComponent<IItemContainer>();

        var itemContainer = other.GetComponent<Unit>().inventory;

        if (itemContainer == null) { return; }
        // item fully added? still error prone
        if (itemContainer.AddItem(itemSlot).quantity == 0)
        {
            Destroy(gameObject);
        };
    }
}