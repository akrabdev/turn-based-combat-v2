using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Pathfinding;
// using

public class PlayerTasks : MonoBehaviour
{

    public static BattleSystem instance;
    // public AIPath aIPath;
    // public AstarPath astarPath;
    // public Path path;
    // public PathFinder finder;
    public Unit player;
    public Unit enemy;
    public Spell fireSpell;
    public Spell lightBoltSpell;
    public Spell healSpell;

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

    [Task]
    void TestTask()
    {
        var task = Task.current;
        Vector3 newv = GetNextMove();
        Move(player.transform.position, newv);
        task.Succeed();
    }

    [Task]
    Vector3 GetNextMove()
    {
        var path = seeker.StartPath(transform.position, enemy.transform.position);
        AstarPath.BlockUntilCalculated(path);
        // Debug.Log(Vector3)path.path[1].position);

        Vector3 newv = new Vector3(0, 0, 0);
        newv = (Vector3)path.path[1].position; //- player.transform.position;
        return newv;
        // player.transform.Translate(newv);

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



        //Left
        if (xNew < xOld && dy < 0.05)
        {
            playerController.moveLeft();

        }

        //Right
        Debug.Log(xNew);

        Debug.Log(xOld);
        Debug.Log(yNew);
        Debug.Log(yOld);

        if (xNew > xOld && dy < 0.05)
        {
            Debug.Log("here");
            playerController.moveRight();
        }
        //Up
        if (dx < 0.05 && yNew > yOld)
        {
            playerController.moveUp();
        }

        //Down
        if (dx < 0.05 && yNew < yOld)
        {
            playerController.moveDown();
        }
    }
}
