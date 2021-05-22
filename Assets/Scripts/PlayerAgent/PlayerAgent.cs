using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

public class PlayerAgent : Agent
{
    public bool trainingMode;
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



        if (!trainingMode)
        {
            MaxStep = 0;
        }
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 0)
        {
            caster.spells[0].CastSpell(caster, target);
            AddReward(0.1f);
        }
        else if (vectorAction[0] == 1)
        {
            if (caster.currentHP == caster.maxHP)
                AddReward(-0.1f);
            caster.spells[1].CastSpell(caster, target);
        }
        else if (vectorAction[0] == 2)
        {
            playerController.moveUp();
        }
        else if (vectorAction[0] == 3)
        {
            playerController.moveDown();
        }
        else if (vectorAction[0] == 4)
        {
            playerController.moveLeft();
        }
        else if (vectorAction[0] == 5)
        {
            playerController.moveRight();
        }
        
        BattleSystem.instance.SwitchTurn();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(caster.currentHP/caster.maxHP);
        sensor.AddObservation(target.currentHP/target.maxHP);
        sensor.AddObservation(target.transform.position);
        sensor.AddObservation(caster.transform.position);
        sensor.AddObservation(Vector3.Distance(target.transform.position, caster.transform.position));
    }

    public override void Heuristic(float[] actionsOut)
    {
        //if (Input.GetKey(KeyCode.UpArrow))
        //    actionsOut[0] = 0;
        //else if (Input.GetKey(KeyCode.DownArrow))
        //    actionsOut[0] = 1;
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //    actionsOut[0] = 2;
        //else if (Input.GetKey(KeyCode.RightArrow))
        //    actionsOut[0] = 3;
    }

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            AddReward(-1f);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            AddReward(-1f);
        }
    }
}
