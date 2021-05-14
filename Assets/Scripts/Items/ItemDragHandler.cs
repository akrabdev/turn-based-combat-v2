using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// safety checker
[RequireComponent(typeof(CanvasGroup))]

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //refs: 

    [SerializeField] protected ItemSlotUI itemSlotUI = null;
    private CanvasGroup canvasGroup = null;
    //When drag and release  snap back to its origianl parent
    private Transform originalParent = null;

    private bool isHovering = false;

    // ItemSlotUI Getter
    public ItemSlotUI ItemSlotUI => itemSlotUI;

    private void Start() => canvasGroup = GetComponent<CanvasGroup>();


    private void OnDisable()
    {
        if (isHovering)
        {
            //raise event
            isHovering = false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //raise event
            originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }
    // button released
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;

        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        // raise event
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {

        // raise event
        isHovering = false;
    }
}

