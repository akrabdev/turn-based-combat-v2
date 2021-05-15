
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// Base Class for every  slot hotbar slot inventory slot, spell slot
public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    // protected : accessed by children
    //refernece image
    [SerializeField] protected Image itemIconImage = null;

    // 
    public int SlotIndex { get; private set; }

    // GETTER , setter for SlotItem
    public abstract HotbarItem SlotItem { get; set; }
    // when opened
    // Update
    private void OnEnable() => UpdateSlotUI();

    // first ever  open
    protected virtual void Start()
    {

        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }
    // handling dropping item on slot
    public abstract void OnDrop(PointerEventData eventData);
    //How Slot is updated , depends on inventory, skills (cooldown etc)
    public abstract void UpdateSlotUI();
    // maybe for inventory we need to disable quantity
    protected virtual void EnableSlotUI(bool enable)
    {
        itemIconImage.enabled = enable;
    }

}

