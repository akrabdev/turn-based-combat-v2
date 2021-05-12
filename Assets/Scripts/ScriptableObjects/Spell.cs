using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/NewSpell")]
public class Spell : ScriptableObject
{
    // Start is called before the first frame update
    [Header("About this spell")]

    public string spellName;
    public string description;
    public enum Element { Fire, Energy }
    public Element element;

    [Header("Spell stats")]
    public int currentCooldown = 0;
    public int maxCooldown;
    public int damage;
    public int manaCost;

    public void CastSpell()
    {

        PutOnCooldown();
    }

    public void PutOnCooldown()
    {
        CooldownManager.instance.StartCooldown(this);
    }

    public bool IsSpellReady()
    {
        if (currentCooldown <= 0)
            return true;
        else
            return false;
    }
}
