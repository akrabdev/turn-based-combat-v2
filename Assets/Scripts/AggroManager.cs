using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroManager : MonoBehaviour
{
    BattleSystem bs;
    // Start is called before the first frame update
    void Start()
    {
        bs = BattleSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BattleSystem battleSystem = Instantiate(BattleInstance);
        if(collision.gameObject.CompareTag("Player") && bs.state == BattleState.IDLE)
            bs.SetupBattle(collision.gameObject, gameObject.transform.parent.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(bs.state == BattleState.WON || bs.state == BattleState.LOST || bs.state == BattleState.IDLE)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            bs.state = BattleState.ESCAPE;
            bs.EndBattle();
        }
    }
}
