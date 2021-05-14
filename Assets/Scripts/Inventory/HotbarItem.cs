using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// For anything that can go to hotbar
public abstract class HotbarItem : ScriptableObject
{
    [Header("Basic Info")]
    // new for avoiding name string conflict
    [SerializeField] private new string name = "New Hotbar Item Name";
    // Item icon
    [SerializeField] private Sprite icon = null;

    // name getter
    public string Name => name;
    // colouredName getter (alternative syntax)
    // For item rarity 
    // abstract .. other classes job
    public abstract string ColouredName { get; }
    // Sprite getter
    public Sprite Icon => icon;

    // different classes display different information
    public abstract string GetInfoDisplayText();


}
