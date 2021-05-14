using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useText = "Does something, maybe?";
    public override string GetInfoDisplayText()
    {
        // StringBuilder better than Concatenating strings, because it dosnt create new Strings
        StringBuilder builder = new StringBuilder();
        // Consumable name
        builder.Append(Name).AppendLine();
        // Consumable Effect
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        // Extra Info Max Stack & Sell Price
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

        return builder.ToString();
    }

}
