using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { IDLE, START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE}

public class BattleSystem : MonoBehaviour
{

	GameObject player;
	GameObject enemy;

    public GameObject DialoguePanel;
    public GameObject InformationBar;
    InformationBarManager infoBarManager;

    Unit playerUnit;
	Unit enemyUnit;

    EnemyAgent agent;

    

	public BattleState state;

    public ParticleSystem bloodEffect;

    //public CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.IDLE;
        infoBarManager = InformationBar.GetComponent<InformationBarManager>();
        //combatManager.player = playerUnit;
        //combatManager.enemy = enemyUnit;
        //SetupBattle();
    }

	public void SetupBattle(GameObject playerInBattle, GameObject enemyInBattle)
	{
        DialoguePanel.SetActive(true);
        player = playerInBattle;
        enemy = enemyInBattle;
        //dialogueText.text = "You have entered a battle against " + enemy.name;
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        StartCoroutine(infoBarManager.UpdateText("You have entered a battle against " + enemyUnit.unitName));
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
    void Attack(Unit unitAttacking, Unit unitBeingAttacked)
    {
        if (Vector3.Distance(enemy.transform.position, player.transform.position) >= 1.5)
        {
            StartCoroutine(infoBarManager.UpdateText("Must be in range"));
            return;
        }
            
        int missChance = Random.Range(0, 9);
        if (missChance > 6)
        {
            Debug.Log("Miss!");
            //Switch turn
            return;
        }
        int critChance = Random.Range(0, 9);
        int damage = unitAttacking.damage;
        if (critChance > 6)
        {
            damage *= 2;
            Debug.Log("Critical!");
        }
        bool isDead = unitBeingAttacked.TakeDamage(damage);
        ParticleSystem blood = Instantiate(bloodEffect, unitBeingAttacked.transform.position, unitBeingAttacked.transform.rotation);
        Destroy(blood.gameObject, 1f);
        //enemyHUD.SetHP(enemyUnit.currentHP);
        StartCoroutine(infoBarManager.UpdateText(unitAttacking.unitName + "'s attack is successful!"));

        if (isDead && unitBeingAttacked.gameObject.CompareTag("Player"))
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else if (isDead && unitBeingAttacked.gameObject.CompareTag("Enemy"))
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if (state == BattleState.ENEMYTURN)
                state = BattleState.PLAYERTURN;
            else
                state = BattleState.ENEMYTURN;
        }
    }

    public void EnemyTurn(float[] vectorAction)
	{
        
        if(vectorAction != null)
        {
            if (vectorAction[0] == 1)
            {
                Attack(enemyUnit, playerUnit);
                PlayerTurn();
            }
            else if (vectorAction[1] == 1)
            {
                Heal(enemyUnit);
                PlayerTurn();
            }

            else if (vectorAction[2] == 1)
            {
                StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves left!"));
                enemy.transform.Translate(new Vector3(-1, 0, 0));
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (vectorAction[3] == 1)
            {
                StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves right!"));
                enemy.transform.Translate(new Vector3(1, 0, 0));
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (vectorAction[4] == 1)
            {
                StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves up!"));
                enemy.transform.Translate(new Vector3(0, 1, 0));
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (vectorAction[5] == 1)
            {
                StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves down!"));
                enemy.transform.Translate(new Vector3(0, -1, 0));
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

    }

    public void EndBattle()
	{
		if(state == BattleState.WON)
		{
            Debug.Log("Agent lost");
            playerUnit.addExperience(50 * enemyUnit.unitLevel);
            if (agent.trainingMode)
                agent.AddReward(-1f);
            //dialogueText.text = "You won the battle!";
            StartCoroutine(infoBarManager.UpdateText("You won the battle!"));
            Destroy(enemy);
		} else if (state == BattleState.LOST)
		{
            Debug.Log("Agent won");
            playerUnit.removeExperience(10 * enemyUnit.unitLevel);
            if (agent.trainingMode)
                agent.AddReward(0.5f);
            StartCoroutine(infoBarManager.UpdateText("You were defeated."));
            //dialogueText.text = "You were defeated.";
		}
        else if (state == BattleState.ESCAPE)
        {
            Debug.Log("Player escaped");
            if (agent.trainingMode)
                agent.AddReward(0.25f);
            StartCoroutine(infoBarManager.UpdateText("You escaped the fight."));
            //dialogueText.text = "You escaped the fight.";
            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.SetHP();
        }
        DialoguePanel.SetActive(false);
        if (agent.trainingMode)
            agent.EndEpisode();
        //else
        //    SetupBattle();
        Debug.Log("Ended episode from battlesystem.cs");
	}

	public void PlayerTurn()
	{
        //StartCoroutine(infoBarManager.UpdateText("Choose an action."));
        //dialogueText.text = "Choose an action:";
        //For automation
        if (agent.trainingMode)
        {
            int rand = Random.Range(0, 9);
            if (rand > 4)
                Attack(playerUnit, enemyUnit);
            else
                Heal(playerUnit);
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
    void Heal(Unit healingUnit)
    {
        healingUnit.Heal();

        //playerHUD.SetHP(playerUnit.currentHP);
        StartCoroutine(infoBarManager.UpdateText("You feel renewed strength."));
        //dialogueText.text = "You feel renewed strength!";

        if (state == BattleState.ENEMYTURN)
            state = BattleState.PLAYERTURN;
        else
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
        Attack(playerUnit, enemyUnit);

    }

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

        //StartCoroutine(PlayerHeal());
        Heal(playerUnit);
	}

    

    //public void OnShieldButton()
    //{
    //    if (state != BattleState.PLAYERTURN)
    //        return;
    //    //StartCoroutine(PlayerHeal());
    //    PlayerShield();
    //}

}
