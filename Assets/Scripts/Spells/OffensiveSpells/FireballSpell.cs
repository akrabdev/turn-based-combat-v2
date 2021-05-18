using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireballSpell", menuName = "Spells/New Fireball Spell")]
public class FireballSpell : Spell
{
    public override void CastSpell(Unit spellCaster, Unit target)
    {
        if (IsSpellReady())
        {
            PutOnCooldown();
            spellCaster.currentMana -= manaCost;
            spellCaster.SetMana();
            if (isProjectile)
            {
                GameObject instantiatedProj = Instantiate(projectile, spellCaster.transform.position, Quaternion.identity);
                Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
                instantiatedProjComponent.effect = effect[0];
                instantiatedProjComponent.Fire(spellCaster, target, damage, element, follow, followSpeed);
                FindObjectOfType<AudioManager>().Play(projectileSoundEffectName);
            }
        }
        else
        {

        }

    }
}
