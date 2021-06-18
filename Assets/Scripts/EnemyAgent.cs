using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Actuators;

public class EnemyAgent : Agent
{
    public bool trainingMode;
    public bool isFrozen;
    GameObject Player;
    Unit enemyUnit;
    Unit playerUnit;
    BattleSystem battleSystem;

    public override void Initialize()
    {
        battleSystem = BattleSystem.instance;
        enemyUnit = battleSystem.enemyUnit;
        playerUnit = battleSystem.playerUnit;
        if (!trainingMode)
        {
            MaxStep = 0;
        }

    }

    public override void OnEpisodeBegin()
    {
        if (trainingMode)
            battleSystem.SetupBattle(Player, gameObject);
    }
    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        //Debug.Log("Received agent action");
        //Debug.Log(vectorAction[0]);
        {
            if (vectorAction.DiscreteActions[0] == 0)
            {
                //Attack(enemyUnit, playerUnit);
                enemyUnit.spells[0].CastSpell(enemyUnit, playerUnit);
            }
            else if (vectorAction.DiscreteActions[0] == 1)
            {
                enemyUnit.spells[1].CastSpell(enemyUnit, playerUnit);
                //Heal(enemyUnit);
            }
            AddReward(enemyUnit.currentHP / enemyUnit.maxHP);
            AddReward(-(playerUnit.currentHP / playerUnit.maxHP));
            battleSystem.SwitchTurn();
            //else if (vectorAction[2] == 1)
            //{
            //    StartCoroutine(infoBarManager.UpdateText(enemyUnit.unitName + " moves left!"));
            //    enemy.transform.Translate(new Vector3(-1, 0, 0));
            //    state = BattleState.PLAYERTURN;
            //    PlayerTurn();
            //}
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

        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(playerUnit.currentHP);
        sensor.AddObservation(enemyUnit.currentHP);
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
