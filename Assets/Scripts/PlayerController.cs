using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for controlling the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    //For animation
    public float movementSpeed;
    Animator anim;
    Rigidbody2D rigidbodyComponent;
    // Start is called before the first frame update
    void Start()
    {
        //anim.SetFloat("MovementSpeed", movementSpeed);
        anim = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Every frame the key is checked. If a player is in battle a move ends his turn.
    /// </summary>
    void Update()
    {

        if(BattleSystem.instance.state == BattleState.PLAYERTURN)
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Collider2D hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(1, 0) , new Vector2(0.15f, 0.15f), 0f);
                if (hitCollider)
                {
                    Debug.Log("Hit : " + hitCollider.name);
                    if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
                        BattleSystem.instance.SwitchTurn();
                    else
                    {
                        moveRight();
                        BattleSystem.instance.SwitchTurn();
                    }

                }
                
                else
                {
                    moveRight();
                    BattleSystem.instance.SwitchTurn();
                }
                
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Collider2D hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(0, -1), new Vector2(0.15f, 0.15f), 0f);
                if (hitCollider)
                {
                    Debug.Log("Hit : " + hitCollider.name);
                    if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
                        BattleSystem.instance.SwitchTurn();
                    else
                    {
                        moveDown();
                        BattleSystem.instance.SwitchTurn();
                    }

                }

                else
                {
                    moveDown();
                    BattleSystem.instance.SwitchTurn();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Collider2D hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(0, 1), new Vector2(0.15f, 0.15f), 0f);
                if (hitCollider)
                {
                    Debug.Log("Hit : " + hitCollider.name);
                    if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
                        BattleSystem.instance.SwitchTurn();
                    else
                    {
                        moveUp();
                        BattleSystem.instance.SwitchTurn();
                    }

                }

                else
                {
                    moveUp();
                    BattleSystem.instance.SwitchTurn();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Collider2D hitCollider = Physics2D.OverlapBox((Vector2)gameObject.transform.position + new Vector2(-1, 0), new Vector2(0.15f, 0.15f), 0f);
                if (hitCollider)
                {
                    Debug.Log("Hit : " + hitCollider.name);
                    if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Obstacle"))
                        BattleSystem.instance.SwitchTurn();
                    else
                    {
                        moveLeft();
                        BattleSystem.instance.SwitchTurn();
                    }

                }

                else
                {
                    moveLeft();
                    BattleSystem.instance.SwitchTurn();
                }
            }
            else if (Input.GetKeyDown(KeyCode.F1))
            {
                BattleSystem.instance.playerUnit.spells[0].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                BattleSystem.instance.playerUnit.spells[2].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                BattleSystem.instance.playerUnit.spells[6].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                BattleSystem.instance.playerUnit.spells[7].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                BattleSystem.instance.playerUnit.spells[8].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                BattleSystem.instance.playerUnit.spells[9].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F7))
            {
                BattleSystem.instance.playerUnit.spells[10].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.F8))
            {
                BattleSystem.instance.playerUnit.spells[11].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                BattleSystem.instance.playerUnit.spells[12].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                BattleSystem.instance.playerUnit.spells[13].CastSpell(BattleSystem.instance.playerUnit, BattleSystem.instance.enemyUnit);
                BattleSystem.instance.SwitchTurn();
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
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    Gizmos.DrawWireCube(transform.position + new Vector3(1, 0), transform.localScale);
    //}

    public void moveLeft()
    {
        rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(-1f, 0));
        
        anim.SetTrigger("MoveLeft");
        
    }

    public void moveRight()
    {
        rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(1f, 0));
        anim.SetTrigger("MoveRight");

    }

    public void moveUp()
    {
        //Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + 1);
        //transform.position = Vector2.Lerp(transform.position, targetPos, movementSpeed * Time.deltaTime);
        rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(0, 1f));
        anim.SetTrigger("MoveUp");
       
    }

    public void moveDown()
    {
        rigidbodyComponent.MovePosition((Vector2)transform.position + new Vector2(0, -1f));
        anim.SetTrigger("MoveDown");
        
    }

    
}
