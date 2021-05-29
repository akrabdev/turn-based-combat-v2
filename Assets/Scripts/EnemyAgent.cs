using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyAgent : Agent
{
    public bool trainingMode;
    public bool isFrozen;
    GameObject Player;
    Unit EnemyUnit;
    Unit PlayerUnit;

    public override void Initialize()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyUnit = GetComponent<Unit>();
        PlayerUnit = Player.GetComponent<Unit>();
        if(!trainingMode)
        {
            MaxStep = 0;
        }
        
    }

    public override void OnEpisodeBegin()
    {
        //Debug.Log("Episode beginning");
        if(trainingMode) 
            BattleSystem.instance.SetupBattle(Player, gameObject);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (isFrozen)
            return;
        //Debug.Log("Received agent action");
        //Debug.Log("Enter Action Received");
        //Debug.Log(actions.Equals(null));
        if (BattleSystem.instance.state == BattleState.ENEMYTURN)
        {
            BattleSystem.instance.EnemyTurn(actions);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(PlayerUnit.currentHP);
        sensor.AddObservation(EnemyUnit.currentHP);
    }

    public void FreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/unfreeze not supported in training");
        isFrozen = true;
        //GetComponent<Rigidbody>().Sleep();
    }

    public void UnfreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/unfreeze not supported in training");
        isFrozen = false;
        //GetComponent<Rigidbody>().Sleep();
    }
}
