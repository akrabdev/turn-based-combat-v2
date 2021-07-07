using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArrowshowerSpell", menuName = "Spells/New Arrowshower Spell")]
public class ArrowshowerSpell : Spell
{
    //Unit spellCaster;
    //Unit target;
    //Projectile _projectile;
    //ParticleSystem[] _effect;
    //int _damage;
    //Element _element;
    //bool _follow;
    //int _followSpeed;
    //string _projectileSoundEffectName;

    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            GameObject instantiatedProj = Instantiate(projectile, spellCaster.transform.position, Quaternion.identity);
            Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
            instantiatedProjComponent.effect = effect[0];
            instantiatedProjComponent.Fire(spellCaster, target, damage, element, follow, followSpeed, multiple, multipleNumber);
            FindObjectOfType<AudioManager>().Play(projectileSoundEffectName);
            return true;
        }
        
        else
        {
            return false;
        }

    }

    
}
