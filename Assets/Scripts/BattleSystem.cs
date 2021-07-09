using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

//public enum BattleState { IDLE, START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE }

/// <summary>
/// This class is responsible for managing the battle itself
/// </summary>
public enum BattleState { IDLE, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPE };

public class BattleSystem : MonoBehaviour
{

    //public bool trainingMode;

    public static BattleSystem instance;

    
    public GameObject player; //Player in battle
    public GameObject enemy;
    public Unit playerUnit; //Unit components of the objects
    public Unit enemyUnit;
    public Agent playerAgent;
    public Agent enemyAgent;
    //Agents of the objects
    /*List <Rigidbody2D> objectsBodies = new List<Rigidbody2D>();*/ //Rigidbodies of the objects

    /*public bool playerHasAgent = false; *///If there's an agent component on player

    //public int turn; //Current turn
    /*public bool turnPlayed;*/ //To check if the player has made a move during play mode
    [SerializeField]
    float time = 0; //Timer if the system is in play mode

    public float switchTurnTime = 2; //Time to switch turn automatically (even if no move was made)

    public BattleState state; //State of the battle

    public GameObject InformationBar;

    public bool playerPlayedTurn = false;

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

    //private void Start()
    //{
    //    if (TrainingManager.instance.trainingMode)
    //        SetupBattle(player, enemy);
    //    //SetupBattle(player, enemy);
    //}

    private void Update()
    {
        if(!TrainingManager.instance.trainingMode) //If the game is in play mode, count down for turns
            if(state != BattleState.IDLE)
                Timer();
        //if (player.transform.position.y <= -9.5f || enemy.transform.position.y <= -9.5f)
        //    state = BattleState.IDLE;
    }

    public void SetupBattle(GameObject player, GameObject enemy)
    {
        //STATE MUST BE IDLE IN ORDER TO SETUP BATTLE (IN CASE SetupBattle IS CALLED IN A WRONG PLACE)
        if (state == BattleState.IDLE)
        {
            if(!TrainingManager.instance.trainingMode)
            {
                FindObjectOfType<AudioManager>().Stop("World");
                FindObjectOfType<AudioManager>().Play("Battle");
                
            }
            state = BattleState.PLAYERTURN;
            time = 0;

            //GET AGENT AND UNIT COMPONENTS AND UPDATE HUD FOR EACH OBJECT IN BATTLE EXCEPT PLAYER
            //objectsBodies.Add(objects[i].GetComponent<Rigidbody2D>());
            //objectsBodies[i].constraints |= RigidbodyConstraints2D.FreezePosition;
            this.player = player;
            this.enemy = enemy;
            playerUnit = player.GetComponent<Unit>();
            enemyUnit = enemy.GetComponent<Unit>();

            //Update HUD for player and enemy
            playerUnit.SetHUD();
            enemyUnit.SetHUD();
            //Only reset player's HP if training mode is on
            if (TrainingManager.instance.trainingMode)
            {
                playerUnit.currentHP = playerUnit.maxHP;
                playerUnit.currentMana = playerUnit.maxMana;
                playerUnit.SetHP();
                playerUnit.SetMana();
            }

            enemyUnit.currentHP = enemyUnit.maxHP;
            enemyUnit.currentMana = enemyUnit.maxMana;
            enemyUnit.SetHP();
            enemyUnit.SetMana();

            playerAgent = player.GetComponent<Agent>();
            enemyAgent = enemy.GetComponent<Agent>();

            if(!TrainingManager.instance.trainingMode)
            {
                playerAgent.MaxStep = 0;
                enemyAgent.MaxStep = 0;
            }
            PlayTurn();
        }
    }

    /// <summary>
    /// Wait for a move, or turn to be switched automatically in playmode.
    /// Request agent's decision in train mode.
    /// </summary>
    private void PlayTurn()
    {

        //Player turn and train mode is on
        if (state == BattleState.PLAYERTURN && TrainingManager.instance.trainingMode && TrainingManager.instance.selfPlay)
        {
            if (!playerUnit.isStunned)
                playerAgent.RequestDecision();
            else
                SwitchTurn();
        }
        else if (state == BattleState.PLAYERTURN && TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay)
        {
            if (!playerUnit.isStunned)
                return;
            else
                SwitchTurn();
        }
        else if (state == BattleState.PLAYERTURN && !TrainingManager.instance.trainingMode)
        {
            if (!playerUnit.isStunned)
                return;
            else
                SwitchTurn();
        }
        else if (state == BattleState.ENEMYTURN)
            if (!enemyUnit.isStunned)
                enemyAgent.RequestDecision();
            else
                SwitchTurn();
        
        
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (time >= switchTurnTime)
        {
            SwitchTurn();
        }   
    }

    /// <summary>
    /// Update battle system state to other unit
    /// </summary>
    public void UpdateTurn()
    {
        if (state == BattleState.PLAYERTURN)
            state = BattleState.ENEMYTURN;
        else if (state == BattleState.ENEMYTURN)
        {
            if ((TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay) || !TrainingManager.instance.trainingMode)
            {
                playerPlayedTurn = false;
                state = BattleState.PLAYERTURN;
            }
            else
            {
                state = BattleState.PLAYERTURN;
            }
        }
    }


    /// <summary>
    /// Code for switching turns
    /// </summary>
    public void SwitchTurn()
    {
        if(TrainingManager.instance.trainingMode && TrainingManager.instance.selfPlay)
        {
            // Negative reward for opponent HP ratio
            //playerAgent.AddReward(-(enemyUnit.currentHP / enemyUnit.maxHP));
            //enemyAgent.AddReward(-(playerUnit.currentHP / playerUnit.maxHP));

            // Positive reward for self HP ratio
            //playerAgent.AddReward(playerUnit.currentHP / playerUnit.maxHP);
            //enemyAgent.AddReward(enemyUnit.currentHP / enemyUnit.maxHP);

            // Existential reward
            playerAgent.AddReward(-0.001f);
            enemyAgent.AddReward(-0.001f);
        }
        else if (TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay)
        {
            enemyAgent.AddReward(-0.001f);
        }

        time = 0;
        CooldownManager.instance.SwitchTurn();
        UpdateTurn();

        // Update status effect timers
        if (state == BattleState.PLAYERTURN)
            for (int i = 0; i < playerUnit.statusEffects.Count; i++)
                playerUnit.statusEffects[i].Timer();
        else if (state == BattleState.ENEMYTURN)
            for (int i = 0; i < enemyUnit.statusEffects.Count; i++)
                enemyUnit.statusEffects[i].Timer();

        // Mana regeneration every turn
        playerUnit.currentMana += 2;
        if (playerUnit.currentMana > playerUnit.maxMana)
            playerUnit.currentMana = playerUnit.maxMana;
        enemyUnit.currentMana += 2;
        if (enemyUnit.currentMana > enemyUnit.maxMana)
            enemyUnit.currentMana = enemyUnit.maxMana;


        // Request an action
        PlayTurn();

    }

    public void TargetDead(Unit deadUnit )
    {
        //If player isn't dead -> state: won
        if (playerUnit == deadUnit)
        {
            Debug.Log("Player Lost");
            if (TrainingManager.instance.trainingMode && TrainingManager.instance.selfPlay)
            {
                playerAgent.SetReward(-1f);
                enemyAgent.SetReward(1f);
            }
            else if (TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay)
            {
                enemyAgent.SetReward(1f);
            }
            //objectsUnits[0].transform.position = new Vector2(0.5f, 0.5f);
            playerUnit.isDead = false;
        }
        else
        {
            //TODO: Get unit index then remove all of its components from the lists and destroy it (if not training)
            Debug.Log("Player won");
            if (TrainingManager.instance.trainingMode && TrainingManager.instance.selfPlay)
            {
                playerAgent.SetReward(1f);
                enemyAgent.SetReward(-1f);
            }
            else if (TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay)
            {
                enemyAgent.SetReward(-1f);
            }
            enemyUnit.isDead = false;
        }
        state = BattleState.IDLE;
        if (TrainingManager.instance.trainingMode && TrainingManager.instance.selfPlay)
        {

            playerAgent.EndEpisode();
            enemyAgent.EndEpisode();
        }
        else if (TrainingManager.instance.trainingMode && !TrainingManager.instance.selfPlay)
        {
            enemyAgent.EndEpisode();
        }
        else
        {
            SpawnManager.SMInstance.AfterBattle();
        }
        FindObjectOfType<AudioManager>().Stop("Battle");
        FindObjectOfType<AudioManager>().Play("World");
        

        //SetupBattle(player, enemy);

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

    //public void EndBattle()
    //{
    //    //SOUND MANAGEMENT
    //    //FindObjectOfType<AudioManager>().Play("World");
    //    //FindObjectOfType<AudioManager>().Stop("Battle");

    //    //if (agent.trainingMode)
    //    //{
    //    //    enemyUnit.currentHP = enemyUnit.maxHP;
    //    //    enemyUnit.SetHP();
    //    //    playerUnit.currentHP = playerUnit.maxHP;
    //    //    playerUnit.SetHP();
    //    //}

    //    if (state == BattleState.WON)
    //    {
    //        Debug.Log("Unit 0 win");

    //        //Update player after winning
    //        //StartCoroutine(InformationBarManager.instance.UpdateText("You won the battle!"));
    //        //if (!agent.trainingMode)
    //        //{
    //        //    playerUnit.addExperience(50 * enemyUnit.unitLevel);
    //        //    Destroy(enemy);
    //        //}
    //    }
    //    else if (state == BattleState.LOST)
    //    {
    //        Debug.Log("Enem(y/ies) won");

    //        //StartCoroutine(InformationBarManager.instance.UpdateText("You were defeated."));
    //        //if (!agent.trainingMode)
    //        //{
    //        //    enemyUnit.currentHP = enemyUnit.maxHP;
    //        //    enemyUnit.SetHP();
    //        //    playerUnit.removeExperience(10 * enemyUnit.unitLevel);
    //        //    player.transform.position = new Vector3(0.51f, 0.49f, 0);
    //        //    playerUnit.currentHP = playerUnit.maxHP;
    //        //    playerUnit.currentMana = playerUnit.maxMana;
    //        //    playerUnit.SetHP();
    //        //    playerUnit.SetMana();
    //        //    playerUnit.isDead = false;
    //        //}
    //    }
    //    //else if (state == BattleState.ESCAPE)
    //    //{
    //    //    Debug.Log("Player escaped");
    //    //    StartCoroutine(InformationBarManager.instance.UpdateText("You escaped the fight."));
    //    //    enemyUnit.currentHP = enemyUnit.maxHP;
    //    //    enemyUnit.SetHP();
    //    //}
    //    state = BattleState.IDLE;
    //    SetupBattle(objects);
    //    //if (!agent.trainingMode)
    //    //    DialoguePanel.SetActive(false);
    //    //else
    //    //{
    //    //    SwitchTurn();
    //    //}

    //}

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
