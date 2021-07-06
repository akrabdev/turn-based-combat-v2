using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLightningboltSpell", menuName = "Spells/New Lightningbolt Spell")]
public class LightningboltSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            if (isProjectile)
            {
                GameObject instantiatedProj = Instantiate(projectile, spellCaster.transform.position, Quaternion.identity);
                Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
                instantiatedProjComponent.Fire(spellCaster, target, damage, element, follow, followSpeed);
                FindObjectOfType<AudioManager>().Play(projectileSoundEffectName);
            }
            /////TODO: STUN IF PROJECTILE HITS
            target.statusEffects.Add(new Stun(2, target));
            return true;
        }
        else
            return false;



        //    if (IsSpellReady() && spellCaster.currentMana >= manaCost)
        //{
        //    PutOnCooldown();
        //    spellCaster.currentMana -= manaCost;
        //    spellCaster.SetMana();
            
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }
}
