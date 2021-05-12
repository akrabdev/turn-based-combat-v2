using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyAgent : Agent
{
    public bool trainingMode;
    public bool isFrozen;
    GameObject Player;
    GameObject BattleSystem;
    Unit EnemyUnit;
    Unit PlayerUnit;
    public BattleSystem BattleSystemSc;

    public override void Initialize()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyUnit = GetComponent<Unit>();
        PlayerUnit = Player.GetComponent<Unit>();
        BattleSystem = GameObject.FindGameObjectWithTag("BattleSystem");
        BattleSystemSc = BattleSystem.GetComponent<BattleSystem>();
        if(!trainingMode)
        {
            MaxStep = 0;
        }
        
    }

    public override void OnEpisodeBegin()
    {
        //Debug.Log("Episode beginning");
        BattleSystemSc.SetupBattle(Player, gameObject);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        if (isFrozen)
            return;
        //Debug.Log("Received agent action");
        //Debug.Log(vectorAction[0]);
        if (BattleSystemSc.state == BattleState.ENEMYTURN)
        {
            BattleSystemSc.EnemyTurn(vectorAction);
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
