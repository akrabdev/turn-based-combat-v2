using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    //public bool trainingMode;
    PlayerController playerController;
    BehaviorParameters behaviorParameter;
    Unit caster;
    public Unit target;
    int agentId;
    int targetId;
    
    public override void Initialize()
    {
        playerController = GetComponent<PlayerController>();
        behaviorParameter = GetComponent<BehaviorParameters>();
        agentId = behaviorParameter.TeamId;

        if (agentId == 0)
            targetId = 1;
        else
            targetId = 0;

        caster = GetComponent<Unit>();

        //Debug.Log(agentId);
    }
    public override void OnEpisodeBegin()
    {
        if(BattleSystem.instance.state != BattleState.IDLE)
        {
            BattleSystem.instance.SetupBattle(BattleSystem.instance.player, BattleSystem.instance.enemy);
        }
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        //Mask up
        if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(0, -1f)))
        {
            actionMask.SetActionEnabled(0, 2, false);
        }
        //Mask down
        else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(0, 1f)))
        {
            actionMask.SetActionEnabled(0, 3, false);
        }
        //Mask left
        else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(1f, 0)))
        {
            actionMask.SetActionEnabled(0, 4, false);
        }
        //Mask right
        else if ((Vector2)caster.transform.position == ((Vector2)target.transform.position + new Vector2(-1f, 0)))
        {
            actionMask.SetActionEnabled(0, 5, false);
        }
        

    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        
        if (vectorAction.DiscreteActions[0] == 0)
        {
            //Debug.Log("Attack");
            bool successfulMove = caster.spells[0].CastSpell(caster, target);
            //if (successfulMove)
            //    AddReward(0.01f);
            //else
            //    AddReward(-0.01f);
        }
        else if (vectorAction.DiscreteActions[0] == 1)
        {
            //Debug.Log("Heal");
            bool successfulMove = caster.spells[1].CastSpell(caster, target);
            //if (successfulMove)
            //    AddReward(0.01f);
            //else
            //    AddReward(-0.01f);
            
        }
        else if (vectorAction.DiscreteActions[0] == 2)
        {
            playerController.moveUp();
        }
        else if (vectorAction.DiscreteActions[0] == 3)
        {
            playerController.moveDown();
        }
        else if (vectorAction.DiscreteActions[0] == 4)
        {
            playerController.moveLeft();
        }
        else if (vectorAction.DiscreteActions[0] == 5)
        {
            playerController.moveRight();
        }
        if (!target.isDead)
            BattleSystem.instance.SwitchTurn();
        else
            BattleSystem.instance.TargetDead(target);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(caster.currentHP/caster.maxHP);
        sensor.AddObservation(target.currentHP/target.maxHP);
        //sensor.AddObservation(target.gameObject.transform.position);
        //sensor.AddObservation(caster.gameObject.transform.position);
        sensor.AddObservation((Vector2)(target.transform.position - caster.transform.position).normalized);
        sensor.AddObservation(Vector2.Distance(target.transform.position, caster.transform.position)/16);
    }

    //public override void Heuristic(float[] actionsOut)
    //{
    //    if (Input.GetKeyDown(KeyCode.F1))
    //        actionsOut[0] = 0;
    //    else if (Input.GetKeyDown(KeyCode.F2))
    //        actionsOut[0] = 1;
    //    else if (Input.GetKeyDown(KeyCode.W))
    //        actionsOut[0] = 2;
    //    else if (Input.GetKeyDown(KeyCode.S))
    //        actionsOut[0] = 3;
    //    else if (Input.GetKeyDown(KeyCode.A))
    //        actionsOut[0] = 4;
    //    else if (Input.GetKeyDown(KeyCode.D))
    //        actionsOut[0] = 5;
    //}

    //public void FreezeAgent()
    //{
    //    Debug.Assert(trainingMode == false, "Freeze/unfreeze not supported in training");
    //    isFrozen = true;
    //    //GetComponent<Rigidbody>().Sleep();
    //}

    //public void UnfreezeAgent()
    //{
    //    Debug.Assert(trainingMode == false, "Freeze/unfreeze not supported in training");
    //    isFrozen = false;
    //    //GetComponent<Rigidbody>().Sleep();
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Obstacle"))
    //    {
    //        AddReward(-1f);
    //    }
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Obstacle"))
    //    {
    //        AddReward(-1f);
    //    }
    //}
}
