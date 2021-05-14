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
            if (BattleSystem.instance.state == BattleState.IDLE)
            {
                moveLeft();
            }
            else if (BattleSystem.instance.state == BattleState.PLAYERTURN)
            {
                moveLeft();
                BattleSystem.instance.SwitchTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (BattleSystem.instance.state == BattleState.IDLE)
            {
                moveRight();
            }
            else if (BattleSystem.instance.state == BattleState.PLAYERTURN)
            {
                moveRight();
                BattleSystem.instance.SwitchTurn();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (BattleSystem.instance.state == BattleState.IDLE)
            {
                moveDown();
            }
            else if (BattleSystem.instance.state == BattleState.PLAYERTURN)
            {
                moveDown();
                BattleSystem.instance.SwitchTurn();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (BattleSystem.instance.state == BattleState.IDLE)
            {
                moveUp();
            }
            else if (BattleSystem.instance.state == BattleState.PLAYERTURN)
            {
                moveUp();
                BattleSystem.instance.SwitchTurn();
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
