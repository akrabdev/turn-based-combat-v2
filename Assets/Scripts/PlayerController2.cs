using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;

/// <summary>
/// This class is responsible for controlling the player.
/// </summary>
public class PlayerController2 : MonoBehaviour
{
    //For animation
    Animator anim;
    BattleSystem battleSystem;
    Rigidbody2D rb;
    BehaviorParameters behavior;
    public Unit target;
    Unit self;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        behavior = GetComponent<BehaviorParameters>();
        self = GetComponent<Unit>();
    }

    /// <summary>
    /// Every frame the key is checked. If a player is in battle a move ends his turn.
    /// </summary>
    void Update()
    {
        if (BattleSystem.instance.turn == 0 && BattleSystem.instance.turnPlayed == false)
        {

            
                
            if (Input.GetKeyDown(KeyCode.F1))
            {
                self.spells[0].CastSpell(self, target);
                BattleSystem.instance.turnPlayed = true;
            }
                
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                self.spells[1].CastSpell(self, target);
                BattleSystem.instance.turnPlayed = true;
            }
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    moveUp();
            //    BattleSystem.instance.turnPlayed = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.DownArrow))
            //{
            //    moveDown();
            //    BattleSystem.instance.turnPlayed = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
            //    moveRight();
            //    BattleSystem.instance.turnPlayed = true;
            //}

            //else if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
            //    moveLeft();
            //    BattleSystem.instance.turnPlayed = true;
            //}

        }
    }

    /*
     * Set of functions to change the transform of the object according to user input
     */

    public void moveLeft()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(-1f, 0));
        //anim.SetTrigger("MoveLeft");
    }

    public void moveRight()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(1f, 0));
        //anim.SetTrigger("MoveRight");
        
    }

    public void moveUp()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(0, 1f));
        //anim.SetTrigger("MoveUp");
       
    }

    public void moveDown()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(0, -1f));
        //anim.SetTrigger("MoveDown");
        
    }

    
}
