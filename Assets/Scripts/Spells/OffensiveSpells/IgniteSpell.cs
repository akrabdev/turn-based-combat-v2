using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIgniteSpell", menuName = "Spells/New Ignite Spell")]
public class IgniteSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        if (IsSpellReady() && spellCaster.currentMana >= manaCost)
        {
            PutOnCooldown();
            target.statusEffects.Add(new DOT(5, 10, element, target));
            return true;
        }
        else
        {
            return false;
        }
    }
}
