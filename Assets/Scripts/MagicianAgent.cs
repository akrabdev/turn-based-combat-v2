using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Actuators;

public class MagicianAgent : Agent
{
    public bool isFrozen;
    BehaviorParameters agentBehavior;
    [SerializeField]
    GameObject opponent;
    Unit selfUnit;
    Unit opponentUnit;
    Rigidbody2D rigidbodyComponent;
    Animator anim;
    int teamId;
    public LayerMask colliderLayerMask;

    public override void Initialize()
    {

        agentBehavior = GetComponent<BehaviorParameters>();
        teamId = agentBehavior.TeamId;
        selfUnit = GetComponent<Unit>();
        opponentUnit = opponent.GetComponent<Unit>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //if (!TrainingManager.instance.trainingMode)
        //{
        //    MaxStep = 0;
        //}

    }

    public override void OnEpisodeBegin()
    {
        if (TrainingManager.instance.trainingMode)
            //Initiate battle if team id = 0
            if(teamId == 0 && BattleSystem.instance.state == BattleState.IDLE)
                BattleSystem.instance.SetupBattle(opponent, gameObject);
    }
    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        Collider2D hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(1, 0), new Vector2(0.15f, 0.15f), 0f, colliderLayerMask);
        if (hitCollider)
        {
            //Debug.Log(gameObject.name + " Hit : " + hitCollider.name);
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
            {
                //Debug.Log(gameObject.name + " masking action 5");
                actionMask.SetActionEnabled(0, 5, false);
            }
        }

        hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(0, -1), new Vector2(0.15f, 0.15f), 0f, colliderLayerMask);
        if (hitCollider)
        {
            //Debug.Log(gameObject.name + " Hit : " + hitCollider.name);
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
            {
                //Debug.Log(gameObject.name + " masking action 6");
                actionMask.SetActionEnabled(0, 6, false);
            }
        }

        hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(0, 1), new Vector2(0.15f, 0.15f), 0f, colliderLayerMask);
        if (hitCollider)
        {
            //Debug.Log(gameObject.name + " Hit : " + hitCollider.name);
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
            {
                //Debug.Log(gameObject.name + " masking action 7");
                actionMask.SetActionEnabled(0, 7, false);
            }
        }

        hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(-1, 0), new Vector2(0.15f, 0.15f), 0f, colliderLayerMask);
        if (hitCollider)
        {
            //Debug.Log(gameObject.name + " Hit : " + hitCollider.name);
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
            {
                //Debug.Log(gameObject.name + " masking action 8");
                actionMask.SetActionEnabled(0, 8, false);
            }
        }

        for (int i = 0; i < selfUnit.spells.Count; i++)
        {
            Spell spell = selfUnit.spells[i];
            if (spell.IsSpellReady() && selfUnit.currentMana >= spell.manaCost)
                if (!spell.onSelf)
                    if (Vector3.Distance(opponent.transform.position, selfUnit.transform.position) <= spell.maxRange)
                        actionMask.SetActionEnabled(0, i, true);
                    else
                        actionMask.SetActionEnabled(0, i, false);
                else
                    actionMask.SetActionEnabled(0, i, true);
            else
                actionMask.SetActionEnabled(0, i, false);
        }
        
        ////Mask up
        //if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(0, -1f)))
        //{
        //    actionMask.SetActionEnabled(0, 2, false);
        //}
        ////Mask down
        //else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(0, 1f)))
        //{
        //    actionMask.SetActionEnabled(0, 3, false);
        //}
        ////Mask left
        //else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(1f, 0)))
        //{
        //    actionMask.SetActionEnabled(0, 4, false);
        //}
        ////Mask right
        //else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(-1f, 0)))
        //{
        //    actionMask.SetActionEnabled(0, 5, false);
        //}


    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        {
            //Debug.Log(gameObject.name + " took action: " + vectorAction.DiscreteActions[0]);
            if (vectorAction.DiscreteActions[0] == 0)
                selfUnit.spells[0].CastSpell(selfUnit, opponentUnit);
            else if (vectorAction.DiscreteActions[0] == 1)
                selfUnit.spells[1].CastSpell(selfUnit, opponentUnit);
            else if (vectorAction.DiscreteActions[0] == 2)
                selfUnit.spells[2].CastSpell(selfUnit, opponentUnit);
            else if (vectorAction.DiscreteActions[0] == 3)
                selfUnit.spells[3].CastSpell(selfUnit, opponentUnit);
            else if (vectorAction.DiscreteActions[0] == 4)
                selfUnit.spells[4].CastSpell(selfUnit, opponentUnit);
            else if (vectorAction.DiscreteActions[0] == 5)
            {
                rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(1f, 0));
                anim.SetTrigger("MoveRight");
            }
            else if (vectorAction.DiscreteActions[0] == 6)
            {
                rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(0, -1));
                anim.SetTrigger("MoveDown");
            }
            else if (vectorAction.DiscreteActions[0] == 7)
            {
                rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(0, 1));
                anim.SetTrigger("MoveUp");
            }
            else if (vectorAction.DiscreteActions[0] == 8)
            {
                rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(-1, 0));
                anim.SetTrigger("MoveLeft");
            }

            //AddReward(enemyUnit.currentHP / enemyUnit.maxHP);
            //AddReward(-(playerUnit.currentHP / playerUnit.maxHP));
            //else if (vectorAction[3] == 1)
            //{
            //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves right!"));
            //    enemy.transform.Translate(new Vector3(1, 0, 0));
            //    state = BattleState.PLAYERTURN;
            //    PlayerTurn();
            //}
            //else if (vectorAction[4] == 1)
            //{
            //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves up!"));
            //    enemy.transform.Translate(new Vector3(0, 1, 0));
            //    state = BattleState.PLAYERTURN;
            //    PlayerTurn();
            //}
            //else if (vectorAction[5] == 1)
            //{
            //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves down!"));
            //    enemy.transform.Translate(new Vector3(0, -1, 0));
            //    state = BattleState.PLAYERTURN;
            //    PlayerTurn();
            //}

            if (!opponentUnit.isDead)
                BattleSystem.instance.SwitchTurn();
            else
                BattleSystem.instance.TargetDead(opponentUnit);

        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(opponentUnit.currentHP / opponentUnit.maxHP);
        sensor.AddObservation(selfUnit.currentHP / selfUnit.maxHP);
        sensor.AddObservation(opponentUnit.currentMana / opponentUnit.maxMana);
        sensor.AddObservation(selfUnit.currentMana / selfUnit.maxMana);
        sensor.AddObservation((Vector2)(opponentUnit.transform.position - selfUnit.transform.position).normalized);
        sensor.AddObservation(Vector2.Distance(opponentUnit.transform.position, selfUnit.transform.position) / 10);
    }

    public void FreezeAgent()
    {
        Debug.Assert(TrainingManager.instance.trainingMode == false, "Freeze/unfreeze not supported in training");
        isFrozen = true;
        //GetComponent<Rigidbody>().Sleep();
    }

    public void UnfreezeAgent()
    {
        Debug.Assert(TrainingManager.instance.trainingMode == false, "Freeze/unfreeze not supported in training");
        isFrozen = false;
        //GetComponent<Rigidbody>().Sleep();
    }
}
