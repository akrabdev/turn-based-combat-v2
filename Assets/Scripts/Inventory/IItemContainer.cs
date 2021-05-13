
// What an Item Container need?
public interface IItemContainer

{
    // in interfaces assumes public by default

    // Add Item Method
    ItemSlot AddItem(ItemSlot itemSlot);

    // Remove Item Method
    void RemoveItem(ItemSlot itemSlot);
    void RemoveAt(int slotIndex);

    // Swap two items in container
    void Swap(int indexOne, int indexTwo);

    // Container has item?
    bool HasItem(InventoryItem item);

    // total item quantity in container
    int GetTotalQuantity(InventoryItem item);




}
