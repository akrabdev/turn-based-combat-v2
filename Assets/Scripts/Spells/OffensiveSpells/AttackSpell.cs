using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSpell", menuName = "Spells/New Attack Spell")]
public class AttackSpell : Spell
{

    public override void CastSpell(Unit spellCaster, Unit target)
    {
        if (Vector3.Distance(target.transform.position, spellCaster.transform.position) >= 1.5)
        {
            InformationBarManager.instance.UpdateText("Must be in range");
            return;
        }

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
        int damage = spellCaster.damage;
        //int critChance = Random.Range(0, 9);
        //if (critChance > 6)
        //{
        //    damage *= 2;
        //    //Debug.Log("Critical!");
        //}
        target.TakeDamage(damage, element);
        //ParticleSystem blood = Instantiate(effect[0], target.transform.position, target.transform.rotation);
        //Destroy(blood.gameObject, 1f);
        //enemyHUD.SetHP(enemyUnit.currentHP);
        //StartCoroutine(InformationBarManager.instance.UpdateText(unitAttacking.unitName + "'s attack is successful!"));
    }
}
