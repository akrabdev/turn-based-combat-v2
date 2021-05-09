using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject player;
	public GameObject enemy;

    public Animator anim;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

    private EnemyAgent agent;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
        agent = enemy.GetComponent<EnemyAgent>();
        anim = player.GetComponent<Animator>();
		//SetupBattle();
    }

	public void SetupBattle()
	{
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        playerUnit.currentHP = playerUnit.maxHP;
        enemyUnit.currentHP = enemyUnit.maxHP;
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
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
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //agent.AddReward(-0.2f);
            state = BattleState.ENEMYTURN;

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
                playerHUD.SetHP(playerUnit.currentHP);
                if (isDead)
                {
                    state = BattleState.LOST;
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
                enemyHUD.SetHP(enemyUnit.currentHP);
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

    void EndBattle()
	{
		if(state == BattleState.WON)
		{
            Debug.Log("Agent lost");
            if (agent.trainingMode)
                agent.AddReward(-1f);
            dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
            Debug.Log("Agent won");
            if(agent.trainingMode)
                agent.AddReward(0.5f);
            dialogueText.text = "You were defeated.";
		}
        if (agent.trainingMode)
            agent.EndEpisode();
        else
            SetupBattle();
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

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        state = BattleState.ENEMYTURN;
    }

    void moveLeft()
    {
        player.transform.Translate(-1f, 0, 0);
        dialogueText.text = "You move to the left!";

        state = BattleState.ENEMYTURN;
    }

    void moveRight()
    {
        player.transform.Translate(1f, 0, 0);
        dialogueText.text = "You move to the right!";

        state = BattleState.ENEMYTURN;
    }

    void moveUp()
    {
        player.transform.Translate(0, 1f, 0);
        dialogueText.text = "You move up!";

        state = BattleState.ENEMYTURN;
    }

    void moveDown()
    {
        player.transform.Translate(0, -1f, 0);
        dialogueText.text = "You move down!";

        state = BattleState.ENEMYTURN;
    }

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

    public void OnLeftButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //StartCoroutine(PlayerHeal());
        anim.SetTrigger("MoveLeft");
        moveLeft();
    }

    public void OnRightButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        anim.SetTrigger("MoveRight");
        //StartCoroutine(PlayerHeal());
        moveRight();
    }

    public void OnUpButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //StartCoroutine(PlayerHeal());
        anim.SetTrigger("MoveUp");
        moveUp();
        
    }

    public void OnDownButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        anim.SetTrigger("MoveDown");
        //StartCoroutine(PlayerHeal());
        moveDown();
    }

}
