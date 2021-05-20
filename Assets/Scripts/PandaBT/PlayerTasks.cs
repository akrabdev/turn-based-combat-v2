using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class PlayerTasks : MonoBehaviour
{

    public static BattleSystem instance;
    public Unit player;
    public Unit enemy;
    public Spell fireSpell;
    public Spell lightBoltSpell;
    public Spell healSpell;
    // Start is called before the first frame update
    void Start()
    {

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
        }
        fireSpell.CastSpell(player, enemy);

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
        }
        lightBoltSpell.CastSpell(player, enemy);

    }
}
