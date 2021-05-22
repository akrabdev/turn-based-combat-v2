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
    Rigidbody2D rigidbody;
    BehaviorParameters behavior;
    int id = 0;
    public Unit target;
    Unit self;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        behavior = GetComponent<BehaviorParameters>();
        self = GetComponent<Unit>();
    }

    /// <summary>
    /// Every frame the key is checked. If a player is in battle a move ends his turn.
    /// </summary>
    void Update()
    {
        if (BattleSystem.instance.turn == id)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveUp();
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDown();
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveRight();
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveLeft();
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
            else if (Input.GetKeyDown(KeyCode.F1))
            {
                self.spells[0].CastSpell(self, target);
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                self.spells[1].CastSpell(self, target);
                StartCoroutine(BattleSystem.instance.SwitchTurn());
            }
        }
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
