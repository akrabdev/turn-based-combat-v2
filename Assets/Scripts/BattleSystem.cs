using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

//public enum BattleState { IDLE, START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE }

/// <summary>
/// This class is responsible for managing the battle itself
/// </summary>
public enum BattleState { IDLE, ONGOING, WON, LOST };

public class BattleSystem : MonoBehaviour
{

    public static BattleSystem instance;

    public GameObject[] objects;
    public Unit[] objectsUnits;
    Agent[] objectsAgents;
    public int turn;
    
    public BattleState state;

    public GameObject DialoguePanel;
    public GameObject InformationBar;

    Agent agent;
    Agent playerAgent;

    //public BattleState state;
    int AddTurn()
    {
        turn++;
        if (turn == objects.Length)
            turn = 0;
        return turn;
    }

    public int nextTurn ()
    {
        int next = turn + 1;
        if (next == objects.Length)
            return 0;
        return next;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
        SetupBattle(objects);
    }

    void Start()
    {
        
    }

    public void SetupBattle(GameObject [] objects)
    {
        //SOUND MANAGEMENT
        //FindObjectOfType<AudioManager>().Play("Battle");
        //FindObjectOfType<AudioManager>().Stop("World");

        

        objectsUnits = new Unit[objects.Length];
        objectsAgents = new Agent[objects.Length];

        //GET AGENT AND UNIT COMPONENTS AND UPDATE HUD FOR EACH OBJECT IN BATTLE
        for (int i = 0; i < objects.Length; i++)
        {
            objectsUnits[i] = objects[i].GetComponent<Unit>();
            objectsUnits[i].SetHUD();
            objectsAgents[i] = objects[i].GetComponent<Agent>();
            
        }


        //StartCoroutine(InformationBarManager.instance.UpdateText("You have entered a battle against " + enemyUnit.unitName));
        //if (!agent.trainingMode)
        //{
        //    agent.UnfreezeAgent();
        //    DialoguePanel.SetActive(true);
        //}
        //else
        //{
        //enemyUnit.currentHP = enemyUnit.maxHP;
        //enemyUnit.SetHP();
        //playerUnit.currentHP = playerUnit.maxHP;
        //playerUnit.SetHP();
        //}
        //agent.BattleSystemSc = this;

        //STATE MUST BE IDLE IN ORDER TO SETUP BATTLE (IN CASE SetupBattle IS CALLED IN A WRONG PLACE)
        if (state == BattleState.IDLE)
        {
            state = BattleState.ONGOING;
            foreach (Unit unit in objectsUnits)
            {
                unit.currentHP = unit.maxHP;
                unit.SetHP();
            }
            turn = Random.Range(0, 2);
            objectsAgents[turn].RequestDecision();
        }
    }

    

    public void SwitchTurn()
    {
        CooldownManager.instance.SwitchTurn();

        if (state == BattleState.IDLE)
        {
            state = BattleState.ONGOING;
            foreach (Unit unit in objectsUnits)
            {
                unit.currentHP = unit.maxHP;
                unit.SetHP();
            }
            AddTurn();
            objectsAgents[turn].RequestDecision();
            return;
        }

        

        AddTurn();
        objectsAgents[turn].RequestDecision();
    }

    public void Death()
    {
        //IF PLAYER ISN'T DEAD -> STATE: WON
        if (!objectsUnits[0].isDead)
        {
            state = BattleState.WON;
            objectsAgents[0].AddReward(1f);
            objectsAgents[1].AddReward(-1f);
            //objects[1].transform.position = new Vector3(0.5f, 0.5f, 0);
            EndBattle();
        }
        else
        {
            state = BattleState.LOST;
            objectsAgents[1].AddReward(1f);
            objectsAgents[0].AddReward(-1f);
            //objects[0].transform.position = new Vector3(0.5f, 0.5f, 0);
            EndBattle();
        }
    }



    // FOR AUTOMATION
    //void Attack(Unit unitAttacking, Unit unitBeingAttacked)
    //{
    //    if (Vector3.Distance(enemy.transform.position, player.transform.position) >= 1.5)
    //    {
    //        StartCoroutine(InformationBarManager.instance.UpdateText("Must be in range"));
    //        SwitchTurn();
    //        return;
    //    }

    //    //int missChance = Random.Range(0, 9);
    //    //if (missChance > 6)
    //    //{
    //    //    //Debug.Log("Miss!");
    //    //    if (state == BattleState.ENEMYTURN)
    //    //    {
    //    //        state = BattleState.PLAYERTURN;
    //    //        PlayerTurn();
    //    //    }
    //    //    else
    //    //        state = BattleState.ENEMYTURN;
    //    //    return;
    //    //}
    //    int damage = unitAttacking.damage;
    //    //int critChance = Random.Range(0, 9);
    //    //if (critChance > 6)
    //    //{
    //    //    damage *= 2;
    //    //    //Debug.Log("Critical!");
    //    //}
    //    bool isDead = unitBeingAttacked.TakeDamage(damage);
    //    ParticleSystem blood = Instantiate(bloodEffect, unitBeingAttacked.transform.position, unitBeingAttacked.transform.rotation);
    //    Destroy(blood.gameObject, 1f);
    //    //enemyHUD.SetHP(enemyUnit.currentHP);
    //    StartCoroutine(InformationBarManager.instance.UpdateText(unitAttacking.unitName + "'s attack is successful!"));

    //    if (isDead && unitBeingAttacked.gameObject.CompareTag("Player"))
    //    {
    //        state = BattleState.LOST;
    //        EndBattle();
    //    }
    //    else if (isDead && unitBeingAttacked.gameObject.CompareTag("Enemy"))
    //    {
    //        state = BattleState.WON;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        SwitchTurn();   
    //    }
    //}

    //public void EnemyTurn(float[] vectorAction)
    //{

    //    if (vectorAction != null)
    //    {
    //        if (vectorAction[0] == 0)
    //        {
    //            //Attack(enemyUnit, playerUnit);
    //            enemyUnit.spells[0].CastSpell(enemyUnit, playerUnit);
    //        }
    //        else if (vectorAction[0] == 1)
    //        {
    //            enemyUnit.spells[1].CastSpell(enemyUnit, playerUnit);
    //            //Heal(enemyUnit);
    //        }
    //        agent.AddReward(enemyUnit.currentHP / enemyUnit.maxHP);
    //        agent.AddReward(-(playerUnit.currentHP / playerUnit.maxHP));
    //        SwitchTurn();
    //        //else if (vectorAction[2] == 1)
    //        //{
    //        //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves left!"));
    //        //    enemy.transform.Translate(new Vector3(-1, 0, 0));
    //        //    state = BattleState.PLAYERTURN;
    //        //    PlayerTurn();
    //        //}
    //        //else if (vectorAction[3] == 1)
    //        //{
    //        //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves right!"));
    //        //    enemy.transform.Translate(new Vector3(1, 0, 0));
    //        //    state = BattleState.PLAYERTURN;
    //        //    PlayerTurn();
    //        //}
    //        //else if (vectorAction[4] == 1)
    //        //{
    //        //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves up!"));
    //        //    enemy.transform.Translate(new Vector3(0, 1, 0));
    //        //    state = BattleState.PLAYERTURN;
    //        //    PlayerTurn();
    //        //}
    //        //else if (vectorAction[5] == 1)
    //        //{
    //        //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves down!"));
    //        //    enemy.transform.Translate(new Vector3(0, -1, 0));
    //        //    state = BattleState.PLAYERTURN;
    //        //    PlayerTurn();
    //        //}
    //    }

    //}

    public void EndBattle()
    {
        //SOUND MANAGEMENT
        //FindObjectOfType<AudioManager>().Play("World");
        //FindObjectOfType<AudioManager>().Stop("Battle");

        //if (agent.trainingMode)
        //{
        //    enemyUnit.currentHP = enemyUnit.maxHP;
        //    enemyUnit.SetHP();
        //    playerUnit.currentHP = playerUnit.maxHP;
        //    playerUnit.SetHP();
        //}

        if (state == BattleState.WON)
        {
            Debug.Log("Unit 0 win");

            //Update player after winning
            //StartCoroutine(InformationBarManager.instance.UpdateText("You won the battle!"));
            //if (!agent.trainingMode)
            //{
            //    playerUnit.addExperience(50 * enemyUnit.unitLevel);
            //    Destroy(enemy);
            //}
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("Enem(y/ies) won");

            //StartCoroutine(InformationBarManager.instance.UpdateText("You were defeated."));
            //if (!agent.trainingMode)
            //{
            //    enemyUnit.currentHP = enemyUnit.maxHP;
            //    enemyUnit.SetHP();
            //    playerUnit.removeExperience(10 * enemyUnit.unitLevel);
            //    player.transform.position = new Vector3(0.51f, 0.49f, 0);
            //    playerUnit.currentHP = playerUnit.maxHP;
            //    playerUnit.currentMana = playerUnit.maxMana;
            //    playerUnit.SetHP();
            //    playerUnit.SetMana();
            //    playerUnit.isDead = false;
            //}
        }
        //else if (state == BattleState.ESCAPE)
        //{
        //    Debug.Log("Player escaped");
        //    StartCoroutine(InformationBarManager.instance.UpdateText("You escaped the fight."));
        //    enemyUnit.currentHP = enemyUnit.maxHP;
        //    enemyUnit.SetHP();
        //}
        state = BattleState.IDLE;
        SetupBattle(objects);
        //if (!agent.trainingMode)
        //    DialoguePanel.SetActive(false);
        //else
        //{
        //    SwitchTurn();
        //}

    }

    //void Heal(Unit healingUnit)
    //{
    //    FindObjectOfType<AudioManager>().Play("HealSound");
    //    healingUnit.Heal();
    //    ParticleSystem healing = Instantiate(healEffect, healingUnit.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
    //    Destroy(healing.gameObject, 1f);
    //    StartCoroutine(InformationBarManager.instance.UpdateText("You feel renewed strength."));
    //    SwitchTurn();
    //}

    //public void PlayerTurn(float[] vectorAction, GameObject objectTurn)
    //{
    //    if (vectorAction != null)
    //    {
    //        if (vectorAction[0] == 0)
    //        {
    //            enemyUnit.spells[0].CastSpell(enemyUnit, playerUnit);
    //        }
    //        else if (vectorAction[0] == 1)
    //        {
    //            enemyUnit.spells[1].CastSpell(enemyUnit, playerUnit);
    //            //Heal(enemyUnit);
    //        }
    //        playerAgent.AddReward(-(playerUnit.currentHP / playerUnit.maxHP));
    //        playerAgent.AddReward(enemyUnit.currentHP / enemyUnit.maxHP);
    //        SwitchTurn();
    //    }

    //    //if (agent.trainingMode)
    //    //{
    //    //    int random = Random.Range(0, 9);
    //    //    if (random > 5)
    //    //    {
    //    //        playerUnit.spells[2].CastSpell(playerUnit, enemyUnit);
    //    //        //Attack(playerUnit, enemyUnit);
    //    //    }
    //    //    else
    //    //    {
    //    //        playerUnit.spells[3].CastSpell(playerUnit, enemyUnit);
    //    //    }
    //    //}

    //    //StartCoroutine(infoBarManager.UpdateText("Choose an action."));
    //    //dialogueText.text = "Choose an action:";
    //    //For automation
    //    //if (agent.trainingMode)
    //    //{
    //    //    if (playerUnit.currentHP/ playerUnit.maxHP > enemyUnit.currentHP/enemyUnit.maxHP)
    //    //        Attack(playerUnit, enemyUnit);
    //    //    else
    //    //    {
    //    //        int random = Random.Range(0, 9);
    //    //        {
    //    //            if(playerUnit.currentHP / playerUnit.maxHP > 0.5)
    //    //            {
    //    //                if (random > 5)
    //    //                    Attack(playerUnit, enemyUnit);
    //    //                else
    //    //                    Heal(playerUnit);
    //    //            }
    //    //            else if (playerUnit.currentHP / playerUnit.maxHP > 0.3)
    //    //            {
    //    //                if (random > 7)
    //    //                    Attack(playerUnit, enemyUnit);
    //    //                else
    //    //                    Heal(playerUnit);
    //    //            }
    //    //            else if (playerUnit.currentHP / playerUnit.maxHP > 0.1)
    //    //            {
    //    //                if (random > 9)
    //    //                    Attack(playerUnit, enemyUnit);
    //    //                else
    //    //                    Heal(playerUnit);
    //    //            }
    //    //            else
    //    //                Heal(playerUnit);
    //    //        }
    //    //    }    
    //    //}
    //}

    //    state = BattleState.ENEMYTURN;
    //}
    //public void OnSpellButton(Spell spell)
    //{
    //    if (state == BattleState.PLAYERTURN)
    //    {
    //        spell.CastSpell(playerUnit, enemyUnit);
    //        SwitchTurn();
    //    }
    //}

    //   public void OnAttackButton()
    //{
    //	if (state != BattleState.PLAYERTURN)
    //		return;
    //       Attack(playerUnit, enemyUnit);
    //   }

    //public void OnHealButton()
    //{
    //	if (state != BattleState.PLAYERTURN)
    //		return;
    //       Heal(playerUnit);
    //}


    //void PlayerShield()
    //{
    //    //playerUnit.Shield();
    //    //call ability manager (shield and player
    //    playerUnit.buffs["Shield"] = 1;
    //    dialogueText.text = "You shielded yourself!";

    //    state = BattleState.ENEMYTURN;
    //}

    //public void OnShieldButton()
    //{
    //    if (state != BattleState.PLAYERTURN)
    //        return;
    //    //StartCoroutine(PlayerHeal());
    //    PlayerShield();
    //}

}
