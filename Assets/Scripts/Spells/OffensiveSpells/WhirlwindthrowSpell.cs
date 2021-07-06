using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWhirlwindthrowSpell", menuName = "Spells/New Whirlwindthrow Spell")]
public class WhirlwindthrowSpell : Spell
{
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            if (isProjectile)
            {
                GameObject instantiatedProj = Instantiate(projectile, spellCaster.transform.position, Quaternion.identity);
                Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
                instantiatedProjComponent.effect = effect[0];
                instantiatedProjComponent.Fire(spellCaster, target, damage, element, follow, followSpeed);
                FindObjectOfType<AudioManager>().Play(projectileSoundEffectName);
            }
            return true;
        }
        
        else
        {
            return false;
        }

    }
}
