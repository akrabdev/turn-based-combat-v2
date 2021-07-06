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
     *      b) Increased attacks for some time -> Done
     *      c) Temporary shield -> 
     *      d) Freeze Agent for one turn
     *      e) Increase XP -> Done
     *      f) Level up (very rare) -> Done
     *      g)
     *    3) Allow player to access object -> Done
     *    4) Remove object after some time -> Change to per turn
     *    5) Add priorities to different objects
     *    6) Add max times a player can take spawn or spawn a number of times per 1 battle -> Done (Add UI)
     *    7) Add Particle System for all items //////
     *    8) Make Heal/Attack functions public to change healing/attacking factor for different levels and to make it more modular ??
     *    9) Maybe spawn some items in Player's turn only
     *    10) Change randomness to be more specific to objects as well
     *    11) Spawn more than one object together -> Done
     *    12) Change while(true) -> check this https://www.youtube.com/watch?v=pKN8jVnSKyM
     *    13) Change code to handle more than one enemy spawn
     *    14) Clean up code
     *    15) Fade in/out border -> Later
     */
    public List<GameObject> spanwedObjects;
    Unit PlayerUnit;
    Unit EnemyUnit;
    BattleSystem BS;
    List<GameObject> obj = new List<GameObject>();
    public static SpawnManager SMInstance;

    public List<GameObject> spawnedEnemies;
    List<GameObject> enemyObj = new List<GameObject>();
    BoxCollider2D bc;

    Dictionary<string, int> timesTaken;

    float top;
    float btm;
    float left;
    float right;

    GameObject Border;

    void Awake()
    {
        if (SMInstance == null)
        {
            SMInstance = this;
        }
        else if (SMInstance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);


    }
    //Start is called before the first frame update
    void Start()
    {
        Border = GameObject.FindGameObjectWithTag("Border");
        Border.SetActive(false);
        bc = GetComponent<BoxCollider2D>();
        Vector3 worldPosition = transform.TransformPoint(bc.offset);

        top = worldPosition.y + (bc.size.y / 2f);
        btm = worldPosition.y - (bc.size.y / 2f);
        left = worldPosition.x - (bc.size.x / 2f);
        right = worldPosition.x + (bc.size.x / 2f);

        Debug.Log("left"+left);


        //topLeft = new Vector3(left, top, worldPosition.z);
        //topRight = new Vector3(right, top, worldPosition.z);
        //btmLeft = new Vector3(left, btm, worldPosition.z);
        //btmRight = new Vector3(right, btm, worldPosition.z);
        BS = BattleSystem.instance;
        PlayerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        timesTaken = new Dictionary<string, int>(spanwedObjects.Count);
        EnemyUnit = spawnedEnemies[0].GetComponent<Unit>();//GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
        for (int i = 0; i < spanwedObjects.Count; i++)
        {
            timesTaken.Add(spanwedObjects[i].tag, 0);
            spanwedObjects[i].SetActive(false);
        }
        //StartCoroutine(ObjectSpawn());
        
    }

    private Vector3 GetRandomPosition()
    {

        Vector3 randomPosition = new Vector3(Random.Range(left+0.5f, right-0.5f), Random.Range(top-0.5f, btm+0.5f), -1f);

        return randomPosition;

    }

    IEnumerator ObjectSpawn()
    {
        while (true)
        {
            Debug.Log("Spawning");
            //Change time to be more realistic
            int i = Random.Range(0, spanwedObjects.Count);
            int j = Random.Range(0, spanwedObjects.Count);
            Debug.Log(i);
            Debug.Log(j);
            int TimeToCreate = Random.Range(2, 5);
            for (int k = i; k <= j; k++)
            {
                if (spanwedObjects[k].tag == "LevelUp" && timesTaken[spanwedObjects[k].tag] > 1)
                    yield break;
                Vector3 objectSpawn = GetRandomPosition(); //new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), -1f);
                obj.Add(Instantiate(spanwedObjects[k], objectSpawn, Quaternion.identity));
                obj[obj.Count-1].SetActive(true);
            }
            for(int k = 0; k < obj.Count; k++)
            {
                int TimeToDestroy = Random.Range(2, 5);
                yield return new WaitForSeconds(TimeToDestroy);
                Destroy(obj[k], TimeToDestroy);

            }
            yield return new WaitForSeconds(TimeToCreate);

        }
    }

    public IEnumerator CollectObject(GameObject obj)
    {
        if(obj.CompareTag("Health"))
        {
            if (timesTaken["Health"] > 2)
                yield break;
            else
                timesTaken["Health"]++;
            Destroy(obj);
            PlayerUnit.Heal(10);
            //ParticleSystem healing = Instantiate(BS.healEffect, PlayerUnit.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            //Destroy(healing.gameObject, 1f);
            Debug.Log("Healed");
            yield return new WaitForSeconds(0);
        }
        else if(obj.CompareTag("Armor"))
        {
            //Change to every turn
            Destroy(obj);
            int TimeToDampen = Random.Range(6, 9);
            var temp = EnemyUnit.damagePower;
            EnemyUnit.damagePower /= 5;
            yield return new WaitForSeconds(TimeToDampen);
            EnemyUnit.damagePower = temp;
        }

        else if(obj.CompareTag("XP"))
        {
            if (timesTaken["XP"] > 2)
                yield break;
            else
                timesTaken["XP"]++;
            Destroy(obj);
            PlayerUnit.addExperience(10);
        }
        else if(obj.CompareTag("Attacks"))
        {
            if (timesTaken["Attacks"] > 2)
                yield break;
            else
                timesTaken["Attacks"]++;
            Destroy(obj);
            PlayerUnit.damagePower *= 2;
            int TimeToInc = Random.Range(6, 9);
            yield return new WaitForSeconds(TimeToInc);
            PlayerUnit.damagePower /= 2;

        }
        else if(obj.CompareTag("Shield"))
        {
            if (timesTaken["Shield"] > 2)
                yield break;
            else
                timesTaken["Shield"]++;
            Destroy(obj);
            int TimeToDampen = Random.Range(6, 9);
            var temp = EnemyUnit.damagePower;
            EnemyUnit.damagePower = 0;
            yield return new WaitForSeconds(TimeToDampen);
            EnemyUnit.damagePower = temp;
        }
        else if(obj.CompareTag("LevelUp"))
        {
            if (timesTaken["LevelUp"] > 1)
                yield break;
            else
                timesTaken["LevelUp"]++;
            PlayerUnit.levelUp();
        }
        //else if(obj.CompareTag("Freeze"))
        //{
        //    Destroy(obj);
        //    if (BS.state == BattleState.ENEMYTURN)
        //    {

        //    }
        //}
    }


    //Handling only one enemy for now
    public void EnemySpawn()
    {
        Vector3 objectSpawn = GetRandomPosition();
        GameObject Enemy = Instantiate(spawnedEnemies[0], objectSpawn, Quaternion.identity);
        //FOR NOW
        Enemy.SetActive(true);
        enemyObj.Add(Enemy);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BattleSystem battleSystem = Instantiate(BattleInstance);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enemyObj.Count == 0)
                EnemySpawn();
            Border.SetActive(true);
            enemyObj[0].SetActive(true);
            //Fade(false, Border);
            BS.SetupBattle(collision.gameObject, enemyObj[0]);
            StartCoroutine(ObjectSpawn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (BS.state == BattleState.WON || BS.state == BattleState.LOST || BS.state == BattleState.IDLE)
        {
            AfterBattle(false);
            Border.SetActive(false);
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            AfterBattle(true);
            Border.SetActive(false);
            BS.state = BattleState.ESCAPE;
            //BS.EndBattle();
            //BS.TargetDead(PlayerUnit);
        }
    }

    private void AfterBattle(bool escaped)
    {
        for(int i = 0; i < obj.Count; i++)
        {
            if(!escaped)
                timesTaken[obj[i].tag] = 0;
            Destroy(obj[i]);
        }
    }
    //private void Fade(bool fade, GameObject go)
    //{
        //Color borderColor = go.GetComponent<Renderer>().material.color;
        //if (fade)
        //{
        //    borderColor.a -= Time.deltaTime * 0.7f;
        //    go.GetComponent<Renderer>().material.color = borderColor;
        //}
        //else
        //{
        //    borderColor.a += Time.deltaTime * 1;
        //    go.GetComponent<Renderer>().material.color = borderColor;
        //}
    //}

}
