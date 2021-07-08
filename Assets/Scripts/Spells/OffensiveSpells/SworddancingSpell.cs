using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSworddancingSpell", menuName = "Spells/New Sworddancing Spell")]
public class SworddancingSpell : Spell
{

    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            target.TakeDamage(damage, spellCaster, element);
            if(!TrainingManager.instance.trainingMode)
                Instantiate(effect[0], target.transform.position, Quaternion.identity);
            return true;
        }
        else
            return false;
    }
}
