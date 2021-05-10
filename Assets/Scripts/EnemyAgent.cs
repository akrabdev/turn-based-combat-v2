using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyAgent : Agent
{
    public bool trainingMode;
    public GameObject Player;
    public GameObject BattleSystemOb;
    Unit EnemyUnit;
    Unit PlayerUnit;
    BattleSystem BattleSystemSc;

    public override void Initialize()
    {
        EnemyUnit = this.gameObject.GetComponent<Unit>();
        PlayerUnit = Player.GetComponent<Unit>();
        BattleSystemSc = BattleSystemOb.GetComponent<BattleSystem>();
        if(!trainingMode)
        {
            MaxStep = 0;
        }
        
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode beginning");
        BattleSystemSc.SetupBattle();
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log("Received agent action");
        //Debug.Log(vectorAction[0]);
        //Debug.Log(vectorAction[1]);
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
}
