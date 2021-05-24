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
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Every frame the key is checked. If a player is in battle a move ends his turn.
    /// </summary>
    void Update()
    {
        if (BattleSystem.instance.state == BattleState.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveRight();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDown();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveUp();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveLeft();
            }
        }
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.F1))
        //    {
        //        moveRight();
                
        //    }
        //    else if (Input.GetKeyDown(KeyCode.F2))
        //    {
        //        moveDown();
        //    }
        //    //if (Input.GetKeyDown(KeyCode.RightArrow))
        //    //{
        //    //    moveRight();
        //    //    BattleSystem.instance.
        //    //}
        //    //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //    //{
        //    //    moveDown();
        //    //}
        //    //else if (Input.GetKeyDown(KeyCode.UpArrow))
        //    //{
        //    //    moveUp();
        //    //}
        //    //else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    //{
        //    //    moveLeft();
        //    //}
        //}
    }

    /*
     * Set of functions to change the transform of the object according to user input
     */

    public void moveLeft()
    {
        rigidbody.MovePosition((Vector2)transform.position + new Vector2(-1f, 0));
        //anim.SetTrigger("MoveLeft");
    }

    public void moveRight()
    {
        rigidbody.MovePosition((Vector2)transform.position + new Vector2(1f, 0));
        //anim.SetTrigger("MoveRight");
        
    }

    public void moveUp()
    {
        rigidbody.MovePosition((Vector2)transform.position + new Vector2(0, 1f));
        //anim.SetTrigger("MoveUp");
       
    }

    public void moveDown()
    {
        rigidbody.MovePosition((Vector2)transform.position + new Vector2(0, -1f));
        //anim.SetTrigger("MoveDown");
        
    }

    
}
