using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroManager : MonoBehaviour
{
    GameObject Battle;
    BattleSystem bs;
    // Start is called before the first frame update
    void Start()
    {
        Battle = GameObject.FindGameObjectWithTag("BattleSystem");
        bs = Battle.GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BattleSystem battleSystem = Instantiate(BattleInstance);
        if(collision.gameObject.CompareTag("Player"))
            bs.SetupBattle(collision.gameObject, this.gameObject);
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(bs.state == BattleState.WON || bs.state == BattleState.LOST)
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
