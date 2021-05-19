using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// struct uses values not references , we dont need class here
[Serializable]
public struct SpellSlot
{
    public Spell spell;

    // constructor 
    public SpellSlot(Spell spell)
    {
        this.spell = spell;
    }

    public override bool Equals(object obj)
    {
        return obj is SpellSlot slot &&
               EqualityComparer<Spell>.Default.Equals(spell, slot.spell);
    }

    public override int GetHashCode()
    {
        return -844852431 + EqualityComparer<Spell>.Default.GetHashCode(spell);
    }

    // Override ==: To compare if two slots have same spell
    public static bool operator ==(SpellSlot a, SpellSlot b) { return a.Equals(b); }
    public static bool operator !=(SpellSlot a, SpellSlot b) { return !a.Equals(b); }

}
