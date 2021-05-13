using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for controlling the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    //For animation
    Animator anim;

    //To check states
    public BattleSystem bs;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Every frame the key is checked. If a player is in battle a move is ends his turn.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (bs.state == BattleState.IDLE)
            {
                moveLeft();
            }
            else if (bs.state == BattleState.PLAYERTURN)
            {
                moveLeft();
                bs.state = BattleState.ENEMYTURN;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (bs.state == BattleState.IDLE)
            {
                moveRight();
            }
            else if (bs.state == BattleState.PLAYERTURN)
            {
                moveRight();
                bs.state = BattleState.ENEMYTURN;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (bs.state == BattleState.IDLE)
            {
                moveDown();
            }
            else if (bs.state == BattleState.PLAYERTURN)
            {
                moveDown();
                bs.state = BattleState.ENEMYTURN;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (bs.state == BattleState.IDLE)
            {
                moveUp();
            }
            else if (bs.state == BattleState.PLAYERTURN)
            {
                moveUp();
                bs.state = BattleState.ENEMYTURN;
            }
        }
    }

    /*
     * Set of functions to change the transform of the object according to user input
     */

    void moveLeft()
    {
        anim.SetTrigger("MoveLeft");
        transform.Translate(-1f, 0, 0);
    }

    void moveRight()
    {
        anim.SetTrigger("MoveRight");
        transform.Translate(1f, 0, 0);
    }

    void moveUp()
    {
        anim.SetTrigger("MoveUp");
        transform.Translate(0, 1f, 0);
    }

    void moveDown()
    {
        anim.SetTrigger("MoveDown");
        transform.Translate(0, -1f, 0);
    }
}
