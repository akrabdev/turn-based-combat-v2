using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Pathfinding;
// using

public class PlayerTasks : MonoBehaviour
{

    // public static BattleSystem battleSystem;
    // public AIPath aIPath;
    // public AstarPath astarPath;
    // public Path path;
    // public PathFinder finder;
    public Unit player;
    public Unit enemy;
    public Spell fireSpell;
    public Spell lightBoltSpell;
    public Spell healSpell;
    public Spell meleeAttack;
    private PandaBehaviour pandaBehaviour;

    private Path path;
    private Seeker seeker;
    private PlayerController playerController;
    // private nextLocation;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(aIPath.destination);
        seeker = GetComponent<Seeker>();
        playerController = GetComponent<PlayerController>();
        pandaBehaviour = GetComponent<PandaBehaviour>();




    }

    // public void OnPathComplete(Path p)
    // {
    //     if (p.error)
    //     {
    //         Debug.Log("Invalid path");

    //     }
    //     else
    //     {
    //         path = p;
    //     }


    // }

    // Update is called once per frame
    void Update()
    {
        // var path = seeker.StartPath(transform.position, enemy.transform.position);
        // AstarPath.BlockUntilCalculated(path);
        // Debug.Log(path.path[0].position);


    }

    [Task]
    bool IsPlayerTurn()
    {
        Debug.Log(BattleSystem.instance.state == BattleState.PLAYERTURN);

        if (BattleSystem.instance.state == BattleState.PLAYERTURN)
        {
            return true;
        }
        return false;
    }


    [Task]
    void CastFireSpell()
    {
        var task = Task.current;
        if (fireSpell.currentCooldown > 0 || player.currentMana < 5)
        {
            Debug.Log(fireSpell.currentCooldown);
            task.Fail();
            return;
        }
        fireSpell.CastSpell(player, enemy);
        pandaBehaviour.Reset();
        // BattleSystem.instance.SwitchTurn();

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
        if (lightBoltSpell.currentCooldown > 0 || player.currentMana < 10)
        {
            task.Fail();
            return;
        }
        lightBoltSpell.CastSpell(player, enemy);
        // BattleSystem.instance.SwitchTurn();

    }

    [Task]
    void ReachTarget()
    {
        var task = Task.current;
        Vector3 newv = GetNextMove();
        Debug.Log(newv);
        Vector3 v999 = new Vector3(-999.0f, -999.0f, -999.0f);
        Debug.Log(v999);
        if (v999.Equals(newv))
        {
            Debug.Log("IM HERE");

            task.Succeed();
            return;
        }
        Move(player.transform.position, newv);
        pandaBehaviour.Reset();
        BattleSystem.instance.SwitchTurn();
        task.Fail();
        // task.Succeed();
    }

    [Task]
    Vector3 GetNextMove()
    {
        var path = seeker.StartPath(transform.position, enemy.transform.position);
        AstarPath.BlockUntilCalculated(path);
        // Debug.Log(Vector3)path.path[1].position);

        Vector3 newv = new Vector3(0, 0, 0);
        if (path.path.Count >= 2)
        {
            if (Vector3.Distance((Vector3)path.path[1].position, enemy.transform.position) >= 1.1)
            {

                newv = (Vector3)path.path[1].position; //- player.transform.position;
                return newv;
                // player.transform.Translate(newv);
            }


        }
        return new Vector3(-999.0f, -999.0f, -999.0f);

    }

    void Move(Vector3 current, Vector3 next)
    {
        Debug.Log(current);
        Debug.Log(next);
        float xOld = current.x + 0.01f;
        float yOld = current.y + 0.01f;
        float xNew = next.x;
        float yNew = next.y;
        float dy = Mathf.Abs(yNew - yOld);
        float dx = Mathf.Abs(xNew - xOld);



        Debug.Log("In Move");


        //Left
        if (xNew < xOld && dy < 0.05)
        {
            Debug.Log("Left");
            playerController.moveLeft();

        }

        //Right
        Debug.Log(xNew);

        Debug.Log(xOld);
        Debug.Log(dy);
        // Debug.Log(yNew);
        // Debug.Log(yOld);

        // if (xNew > xOld && dy < 0.05)
        if (xNew > xOld)
        {
            Debug.Log("Right");
            // playerController.moveRight();
            transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
        }
        //Up
        if (dx < 0.05 && yNew > yOld)
        {

            Debug.Log("Up");
            playerController.moveUp();
        }

        //Down
        if (dx < 0.05 && yNew < yOld)
        {
            Debug.Log("Down");
            playerController.moveDown();
        }
    }

    [Task]
    bool IsEnoughMana(int minMana)
    {
        if (player.currentMana < 10)
        {
            return false;
        }
        return true;
    }
    [Task]
    bool IsNowPlayerTurn()
    {
        if (BattleSystem.instance.state == BattleState.PLAYERTURN)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    [Task]
    bool MeleeAttack()
    {
        meleeAttack.CastSpell(player, enemy);
        pandaBehaviour.Reset();
        BattleSystem.instance.SwitchTurn();
        return true;

    }
    [Task]
    bool SwitchTurn()
    {
        pandaBehaviour.Reset();
        BattleSystem.instance.SwitchTurn();
        return false;
    }
}
