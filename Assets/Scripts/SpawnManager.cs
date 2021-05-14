using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
     * Goals
     *    1) Spawn objects according to level/Increase object effect according to level
     *    2) Spawn different types of objects per level
     *      a) Health
     *      b) Increased attacks for some time
     *      c) Temporary shield
     *      d) Freeze Agent for one turn
     *      e) 
     *    3) Allow player to access object
     *    4) Remove object after some time
     *    5) Add priorities to different objects
     */
    public GameObject spanwedObject;
    //Start is called before the first frame update
    void Start()
    {
        spanwedObject.SetActive(false);
        StartCoroutine(objectSpawn());
    }

    // Update is called once per frame
    IEnumerator objectSpawn()
    {
        while (true)
        {
            Vector3 objectSpawn = new Vector3(Random.Range(-8f, 8f), 0f, 0f);
            GameObject obj = Instantiate(spanwedObject, objectSpawn, Quaternion.identity);
            obj.SetActive(true);
            yield return new WaitForSeconds(5);
            Destroy(obj);

        }
    }
}
