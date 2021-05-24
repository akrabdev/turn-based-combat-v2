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
    Rigidbody2D[] objectsBodies;

    public bool playerHasAgent = false;

    public int turn;
    public float time = 0;
    public bool turnPlayed;
    
    public BattleState state;

    public GameObject DialoguePanel;
    public GameObject InformationBar;

    //public BattleState state;

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
        
    }

    private void Start()
    {
        SetupBattle(objects);
    }

    private void Update()
    {
        if(state == BattleState.ONGOING)
            Timer();
    }

    public void SetupBattle(GameObject [] objects)
    {
        //STATE MUST BE IDLE IN ORDER TO SETUP BATTLE (IN CASE SetupBattle IS CALLED IN A WRONG PLACE)
        if (state == BattleState.IDLE)
        {

            turn = 0;

            state = BattleState.ONGOING;

            objectsBodies = new Rigidbody2D[objects.Length];

            objectsUnits = new Unit[objects.Length];
            
            objectsAgents = new Agent[objects.Length];

            //GET AGENT AND UNIT COMPONENTS AND UPDATE HUD FOR EACH OBJECT IN BATTLE EXCEPT PLAYER
            for (int i = 0; i < objects.Length; i++)
            {
                objectsBodies[i] = objects[i].GetComponent<Rigidbody2D>();

                objectsUnits[i] = objects[i].GetComponent<Unit>();
                objectsUnits[i].SetHUD();
                objectsUnits[i].currentHP = objectsUnits[i].maxHP;
                objectsUnits[i].SetHP();
                objectsAgents[i] = objects[i].GetComponent<Agent>();
                if (i == 0)
                {
                    //DISABLE PLAYER AGENT IF NOT TRAINING AND SET PLAYER2 TO 0 MAX STEPS
                    if (objectsAgents[i].enabled)
                        playerHasAgent = true;
                    continue;
                }
                
                objectsBodies[i].constraints |= RigidbodyConstraints2D.FreezePosition;

            }
            
        }
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            SwitchTurn();
            time = 0;
        }
    }

    public void AddTurn()
    {
        turn++;
        if (turn == objects.Length)
        {
            turn = 0;
        }
    }

    public void SwitchTurn()
    {

        CooldownManager.instance.SwitchTurn();
        objectsBodies[turn].constraints |= RigidbodyConstraints2D.FreezePosition;
        AddTurn();
        objectsBodies[turn].constraints &= ~RigidbodyConstraints2D.FreezePosition;
        if (turn == 0)
        {
            //StartCoroutine(InformationBarManager.instance.UpdateText("Player Turn"));
            if (playerHasAgent)
                objectsAgents[turn].RequestDecision();
            else
                turnPlayed = false;
            
        }
        else
        {
            //StartCoroutine(InformationBarManager.instance.UpdateText("Enemy Turn"));
            objectsAgents[turn].RequestDecision();
        }

    }

    public void Death()
    {
        //IF PLAYER ISN'T DEAD -> STATE: WON
        if (objectsUnits[0].isDead)
        {
            Debug.Log("Player Lost");
            objectsAgents[0].AddReward(1f);
            objectsAgents[1].AddReward(-1f);
            objectsUnits[0].isDead = false;
            //objects[1].transform.position = new Vector3(0.5f, 0.5f, 0);
            
        }
        else
        {
            Debug.Log("Player won");
            objectsAgents[0].AddReward(-1f);
            objectsAgents[1].AddReward(1f);
            objectsUnits[1].isDead = false;
            //objects[0].transform.position = new Vector3(0.5f, 0.5f, 0);
        }
        //objectsAgents[0].EndEpisode();
        //objectsAgents[1].EndEpisode();
        state = BattleState.IDLE;
        SetupBattle(objects);

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
