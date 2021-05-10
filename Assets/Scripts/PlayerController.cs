using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveUp();
        }
    }

    void moveLeft()
    {
        anim.SetTrigger("MoveLeft");
        transform.Translate(-1f, 0, 0);
        //dialogueText.text = "You move to the left!";

        //state = BattleState.ENEMYTURN;
    }

    void moveRight()
    {
        anim.SetTrigger("MoveRight");
        transform.Translate(1f, 0, 0);
        //dialogueText.text = "You move to the right!";

        //state = BattleState.ENEMYTURN;
    }

    void moveUp()
    {
        anim.SetTrigger("MoveUp");
        transform.Translate(0, 1f, 0);
        //dialogueText.text = "You move up!";

        //state = BattleState.ENEMYTURN;
    }

    void moveDown()
    {
        anim.SetTrigger("MoveDown");
        transform.Translate(0, -1f, 0);
        //dialogueText.text = "You move down!";

        //state = BattleState.ENEMYTURN;
    }

    // Update is called once per frame
    //public void OnLeftButton()
    //{
    //    //if (state != BattleState.PLAYERTURN)
    //    //    return;

    //    //StartCoroutine(PlayerHeal());
    //    anim.SetTrigger("MoveLeft");
    //    moveLeft();
    //}

    //public void OnRightButton()
    //{
    //    //if (state != BattleState.PLAYERTURN)
    //    //    return;

    //    anim.SetTrigger("MoveRight");
    //    //StartCoroutine(PlayerHeal());
    //    moveRight();
    //}

    //public void OnUpButton()
    //{
    //    //if (state != BattleState.PLAYERTURN)
    //    //    return;

    //    //StartCoroutine(PlayerHeal());
    //    anim.SetTrigger("MoveUp");
    //    moveUp();

    //}

    //public void OnDownButton()
    //{
    //    //if (state != BattleState.PLAYERTURN)
    //    //    return;

    //    anim.SetTrigger("MoveDown");
    //    //StartCoroutine(PlayerHeal());
    //    moveDown();
    //}
}
