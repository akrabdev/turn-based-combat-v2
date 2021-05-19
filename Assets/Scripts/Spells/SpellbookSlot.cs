using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellbookSlot : ItemSlotUI, IDropHandler
{
    //refs: 
    [SerializeField] private Spellbook spellbook = null;
    //[SerializeField] private TextMeshProUGUI itemQuantityText = null;


    public override HotbarItem SlotItem
    {
        get { return SpellSlot.spell; }
        set { }
    }

    public SpellSlot SpellSlot => spellbook.GetSlotByIndex(SlotIndex);

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (itemDragHandler == null) { return; }


    }

    public override void UpdateSlotUI()
    {
        if (SpellSlot.spell == null)
        {
            EnableSlotUI(false);
            return;
        }
        EnableSlotUI(true);

        itemIconImage.sprite = SpellSlot.spell.Icon;
        //itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }


    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        //itemQuantityText.enabled = enable;
    }




}
