using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellbook", menuName = "Items/Spellbook")]
public class Spellbook : ScriptableObject
{
    [SerializeField] private VoidEvent onSpellbookItemsUpdated = null;
    [SerializeField] private SpellbookItemSlot testItemSlot = new SpellbookItemSlot();
    public SpellbookContainer SpellbookContainer { get; } = new SpellbookContainer(4);


    public void OnEnable()
    {
        SpellbookContainer.OnItemsUpdated += onSpellbookItemsUpdated.Raise;

    }

    public void OnDisable()
    {
        SpellbookContainer.OnItemsUpdated -= onSpellbookItemsUpdated.Raise;
    }
    [ContextMenu("Test Add Spell")]
    public void TestAdd()
    {
        SpellbookContainer.AddItem(testItemSlot);
    }

}
