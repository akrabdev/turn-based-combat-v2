using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBloodstealSpell", menuName = "Spells/New Bloodsteal Spell")]
public class BloodstealSpell : Spell
{

    public override bool CastSpell(Unit spellCaster, Unit target)
    {
        bool successfulBaseChecks = base.CastSpell(spellCaster, target);
        if (successfulBaseChecks)
        {

            //int missChance = Random.Range(0, 9);
            //if (missChance > 6)
            //{
            //    //Debug.Log("Miss!");
            //    if (state == BattleState.ENEMYTURN)
            //    {
            //        state = BattleState.PLAYERTURN;
            //        PlayerTurn();
            //    }
            //    else
            //        state = BattleState.ENEMYTURN;
            //    return;
            //}
            //int critChance = Random.Range(0, 9);
            //if (critChance > 6)
            //{
            //    damage *= 2;
            //    //Debug.Log("Critical!");
            //}
            target.TakeDamage(damage, spellCaster, element);
            spellCaster.Heal(5);
            return true;
        }
        else
            return false;
        //ParticleSystem blood = Instantiate(effect[0], target.transform.position, target.transform.rotation);
        //Destroy(blood.gameObject, 1f);
        //enemyHUD.SetHP(enemyUnit.currentHP);
        //StartCoroutine(InformationBarManager.instance.UpdateText(unitAttacking.unitName + "'s attack is successful!"));
    }
}
