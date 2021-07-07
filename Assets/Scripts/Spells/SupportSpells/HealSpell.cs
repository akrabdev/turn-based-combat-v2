using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealSpell", menuName = "Spells/New Heal Spell")]
public class HealSpell : Spell
{
    // Start is called before the first frame update
    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        //FindObjectOfType<AudioManager>().Play("HealSound");
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {
            if (spellCaster.currentHP == spellCaster.maxHP)
            {
                return false;
            }
            else
            {
                spellCaster.Heal(damage);
                return true;
            }
        }
        else
            return false;


            //ParticleSystem healing = Instantiate(effect[0], spellCaster.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            //Destroy(healing.gameObject, 1f);
            //StartCoroutine(InformationBarManager.instance.UpdateText("You feel renewed strength."));
    }
}
