using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShieldSpell", menuName = "Spells/New Shield Spell")]
public class ShieldSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        PutOnCooldown();
        //FindObjectOfType<AudioManager>().Play("HealSound");
        spellCaster.statusEffects.Add(new Shield(3, spellCaster));
        return true;
    }
}
