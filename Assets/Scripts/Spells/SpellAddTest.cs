using UnityEngine;


public class SpellAddTest : MonoBehaviour, IInteractable
{
    [SerializeField] private SpellSlot spellSlot;

    public void Interact(GameObject other)

    {
        // is the thing the item interacting with got itemcontainer?
        // var itemContainer = other.GetComponent<IItemContainer>();

        var spellContainer = other.GetComponent<ISpellContainer>();

        if (spellContainer == null) { return; }
        // item fully added? still error prone
        spellContainer.AddItem(spellSlot);
        Destroy(gameObject);
    }
}