
// What an Item Container need?
public interface ISpellContainer

{
    // in interfaces assumes public by default

    // Add Item Method
    SpellSlot AddItem(SpellSlot spellSlot);

    // Swap two items in coontainer

    // Container has item?
    bool HasItem(Spell spell);

    SpellSlot GetSlotByIndex(int index);




}
