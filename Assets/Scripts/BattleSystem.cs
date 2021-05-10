using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { IDLE, START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE}

public class BattleSystem : MonoBehaviour
{

	GameObject player;
	GameObject enemy;

	Unit playerUnit;
	Unit enemyUnit;

    EnemyAgent agent;

    public Text dialogueText;

	public BattleState state;

    

    //public CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.IDLE;
        
        //combatManager.player = playerUnit;
        //combatManager.enemy = enemyUnit;
        //SetupBattle();
    }

	public void SetupBattle(GameObject playerInBattle, GameObject enemyInBattle)
	{
        player = playerInBattle;
        enemy = enemyInBattle;
        dialogueText.text = "You have entered a battle against " + enemy.name;
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        //playerUnit.currentHP = playerUnit.maxHP;
        //enemyUnit.currentHP = enemyUnit.maxHP;
        playerUnit.SetHUD();
        enemyUnit.SetHUD();
        agent = enemy.GetComponent<EnemyAgent>();
        agent.BattleSystemSc = this;
        agent.UnfreezeAgent();
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //IEnumerator PlayerAttack()
    //{
    //	bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
    //       enemyHUD.SetHP(enemyUnit.currentHP);
    //       dialogueText.text = "The attack is successful!";

    //	yield return new WaitForSeconds(2f);

    //	if(isDead)
    //	{
    //		state = BattleState.WON;
    //		EndBattle();
    //	}
    //       else
    //	{
    //           //agent.AddReward(-0.2f);
    //           state = BattleState.ENEMYTURN;

    //           //StartCoroutine(EnemyTurn());
    //           //EnemyTurn();
    //       }
    //}

    // FOR AUTOMATION
    void PlayerAttack()
    {
        //0.2 = 0.6 * P(crit|attack)
        int missChance = Random.Range(0, 9);
        if (missChance > 6)
        {
            //dialogueText.text = "Miss!";
            Debug.Log("Miss!");
            return;
        }
        int critChance = Random.Range(0, 9);
        int damage = playerUnit.damage;
        if (critChance > 6)
        {
            damage *= 2;
            //dialogueText.text = "Critical!";
            Debug.Log("Critical!");
        }
        bool isDead = enemyUnit.TakeDamage(damage);
        //enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        if (isDead)
        {
            state = BattleState.WON;
            playerUnit.addExperience(50*enemyUnit.unitLevel);
            EndBattle();
        }
        else
        {
            //agent.AddReward(-0.2f);
            state = BattleState.ENEMYTURN;
            //combatManager.switchTurn();
            //StartCoroutine(EnemyTurn());
            //EnemyTurn();
        }
    }

    public void EnemyTurn(float[] vectorAction)
	{
        
        if(vectorAction != null)
        {
            if (vectorAction[0] == 1)
            {
                dialogueText.text = enemyUnit.unitName + " attacks!";
                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
                //playerHUD.SetHP(playerUnit.currentHP);
                if (isDead)
                {
                    state = BattleState.LOST;
                    playerUnit.removeExperience(10 * enemyUnit.unitLevel);
                    EndBattle();
                }
                else
                {
                    //agent.AddReward(0.5f);
                    state = BattleState.PLAYERTURN;

                    PlayerTurn();
                }
            }
            else if (vectorAction[1] == 1)
            {
                dialogueText.text = enemyUnit.unitName + " heals!";
                enemyUnit.Heal(10);
                //agent.AddReward(0.5f);
                //enemyHUD.SetHP(enemyUnit.currentHP);
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        

        //yield return new waitforseconds(1f);

        //bool isdead = playerunit.takedamage(enemyunit.damage);

        //playerhud.sethp(playerunit.currenthp);

        //yield return new waitforseconds(1f);

        //if (isdead)
        //{
        //    state = battlestate.lost;
        //    endbattle();
        //}
        //else
        //{
        //    state = battlestate.playerturn;
        //    playerturn();
        //}

    }

    public void EndBattle()
	{
		if(state == BattleState.WON)
		{
            Debug.Log("Agent lost");
            if (agent.trainingMode)
                agent.AddReward(-1f);
            dialogueText.text = "You won the battle!";
            Destroy(enemy);
		} else if (state == BattleState.LOST)
		{
            Debug.Log("Agent won");
            if(agent.trainingMode)
                agent.AddReward(0.5f);
            dialogueText.text = "You were defeated.";
		}
        else if (state == BattleState.ESCAPE)
        {
            Debug.Log("Player escaped");
            if (agent.trainingMode)
                agent.AddReward(0.25f);
            dialogueText.text = "You escaped the fight.";
            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.SetHP();
        }
        if (agent.trainingMode)
            agent.EndEpisode();
        //else
        //    SetupBattle();
        Debug.Log("Ended episode from battlesystem.cs");
	}

	public void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
        //For automation
        if (agent.trainingMode)
        {
            int rand = Random.Range(0, 9);
            if (rand > 4)
                PlayerAttack();
            else
                PlayerHeal();
        }
    }

    //IEnumerator PlayerHeal()
    //{
    //	playerUnit.Heal(7);

    //	playerHUD.SetHP(playerUnit.currentHP);
    //	dialogueText.text = "You feel renewed strength!";

    //	yield return new WaitForSeconds(2f);

    //	state = BattleState.ENEMYTURN;
    //}
    //FOR AUTOMATION
    void PlayerHeal()
    {
        playerUnit.Heal(10);

        //playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        state = BattleState.ENEMYTURN;
    }

    //void PlayerShield()
    //{
    //    //playerUnit.Shield();
    //    //call ability manager (shield and player
    //    playerUnit.buffs["Shield"] = 1;
    //    dialogueText.text = "You shielded yourself!";

    //    state = BattleState.ENEMYTURN;
    //}

    

    public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;
        //StartCoroutine(PlayerAttack());
        PlayerAttack();
		
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

        //StartCoroutine(PlayerHeal());
        PlayerHeal();
	}

    

    //public void OnShieldButton()
    //{
    //    if (state != BattleState.PLAYERTURN)
    //        return;
    //    //StartCoroutine(PlayerHeal());
    //    PlayerShield();
    //}

}
