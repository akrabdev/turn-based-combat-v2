using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
     * Goals
     *    1) Spawn objects according to level/Increase object effect according to level
     *    2) Spawn different types of objects per level
     *      a) Health -> done
     *      b) Increased attacks for some time 
     *      c) Temporary shield
     *      d) Freeze Agent for one turn
     *      e) Increase XP -> Done
     *      f) Level up (very rare)
     *      g)
     *    3) Allow player to access object -> Done
     *    4) Remove object after some time -> Change to per turn
     *    5) Add priorities to different objects
     *    6) Add max times a player can take spawn or spawn a number of times per 1 battle
     *    7) Add Particle System for all items
     *    8) Make Heal/Attack functions public to change healing/attacking factor for different levels and to make it more modular
     *    9) Maybe spawn some items in Player's turn only
     *    10) Change randomness to be more specific to objects as well
     *    11) Spawn more than one object together
     *    12) Take care of loops (!!!IMPORTANT)
     *    13)
     */
    public List<GameObject> spanwedObjects;
    Unit PlayerUnit;
    Unit EnemyUnit;
    BattleSystem BS;
    List<GameObject> obj = new List<GameObject>();
    //Start is called before the first frame update
    void Start()
    {
        BS = BattleSystem.instance.GetComponent<BattleSystem>();
        PlayerUnit = gameObject.GetComponentInParent<Unit>();
        EnemyUnit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
        for(int i = 0; i < spanwedObjects.Count; i++)
            spanwedObjects[i].SetActive(false);
        StartCoroutine(ObjectSpawn());
    }

    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            //Change time to be more realistic
            int i = Random.Range(0, spanwedObjects.Count);
            int j = Random.Range(0, spanwedObjects.Count);
            int TimeToCreate = Random.Range(6, 9);
            Vector3 objectSpawn = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), -1f);
            for (int k = i; k < j; k++)
            {
                obj.Add(Instantiate(spanwedObjects[k], objectSpawn, Quaternion.identity));

            }
            for(int k = 0; k < obj.Count; k++)
            {
                int TimeToDestroy = Random.Range(2, 5);
                obj[k].SetActive(false);
                Destroy(obj[k], TimeToDestroy);
            }
            yield return new WaitForSeconds(TimeToCreate);

        }
    }

    IEnumerator CollectObject(GameObject obj)
    {
        if(obj.CompareTag("Health"))
        {
            Destroy(obj);
            PlayerUnit.Heal();
            ParticleSystem healing = Instantiate(BS.healEffect, PlayerUnit.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(healing.gameObject, 1f);
            Debug.Log("Healed");
            yield return new WaitForSeconds(0);
        }
        else if(obj.CompareTag("Armor"))
        {
            //Change to every turn
            Destroy(obj);
            int TimeToDampen = Random.Range(6, 9);
            Debug.Log(EnemyUnit.damage);
            EnemyUnit.damage /= 5;
            Debug.Log(EnemyUnit.damage);
            yield return new WaitForSeconds(TimeToDampen);
            EnemyUnit.damage *= 5;
        }
        else if(obj.CompareTag("Freeze"))
        {
            if(BS.state == BattleState.ENEMYTURN)
            {
                
            }
            Destroy(obj);
        }
        else if(obj.CompareTag("XP"))
        {
            PlayerUnit.addExperience(10);
            Destroy(obj);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        StartCoroutine(CollectObject(collision.gameObject));
    }


}
