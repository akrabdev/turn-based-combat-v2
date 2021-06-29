using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShieldSpell", menuName = "Spells/New Shield Spell")]
public class ShieldSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            spellCaster.statusEffects.Add(new Shield(3, spellCaster));
            return true;
        }
        else
            return false;
        
    }
}
