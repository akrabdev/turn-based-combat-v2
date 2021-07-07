using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRageSpell", menuName = "Spells/New Rage Spell")]
public class RageSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            //FindObjectOfType<AudioManager>().Play("HealSound");
            spellCaster.statusEffects.Add(new Rage(3, 2, spellCaster));
            if(!TrainingManager.instance.trainingMode)
                DamagePopupManager.instance.Setup("RAAAGE!", Color.red, spellCaster.transform);
            spellCaster.TakeDamage(20, null, element);
            return true;
        }
        else
            return false;
    }
}
