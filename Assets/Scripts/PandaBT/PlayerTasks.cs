using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

public class PlayerTasks : MonoBehaviour
{

    public static BattleSystem instance;
    public NavMeshAgent agent;
    public Unit player;
    public Unit enemy;
    public Spell fireSpell;
    public Spell lightBoltSpell;
    public Spell healSpell;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Task]
    bool IsPlayerTurn()
    {

        if (instance.state == BattleState.PLAYERTURN)
        {
            return true;
        }
        return false;
    }


    [Task]
    void CastFireSpell()
    {
        var task = Task.current;
        if (fireSpell.currentCooldown > 0)
        {
            task.Fail();
            return;
        }
        fireSpell.CastSpell(player, enemy);

        task.Succeed();

    }

    [Task]
    void CastHeal()
    {
        healSpell.CastSpell(player, player);
    }

    [Task]
    void CastLightBoltSpell()
    {
        var task = Task.current;
        if (lightBoltSpell.currentCooldown > 0)
        {
            task.Fail();
            return;
        }
        lightBoltSpell.CastSpell(player, enemy);

        task.Succeed();

    }


    [Task]
    void GoToEnemy()
    {
        var task = Task.current;
        agent.destination = enemy.transform.position;
        if (Vector3.Distance(agent.transform.position, enemy.transform.position) <= 1)
        {
            task.Succeed();

        }

    }
}
