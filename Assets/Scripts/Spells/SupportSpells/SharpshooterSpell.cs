using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSharpshooterSpell", menuName = "Spells/New Sharpshooter Spell")]
public class SharpshooterSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            //FindObjectOfType<AudioManager>().Play("HealSound");
            spellCaster.statusEffects.Add(new DamageBuff(3, 1, spellCaster));
            if (!TrainingManager.instance.trainingMode)
            {
                DamagePopupManager.instance.Setup("SHARPSHOOOOOOTEEER!", Color.red, spellCaster.transform);
                FindObjectOfType<AudioManager>().Play("HealSound");
            }
            return true;
        }
        else
            return false;
    }
}
