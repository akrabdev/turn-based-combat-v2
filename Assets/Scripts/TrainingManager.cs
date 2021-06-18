using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager instance;
    public bool trainingMode;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

    }
    void Start()
    {
        
        if (trainingMode)
        {
            Agent playerAgent = player.GetComponent<Agent>();
            Agent enemyAgent = enemy.GetComponent<Agent>();
            playerAgent.enabled = true;
            BattleSystem.instance.switchTurnTime = 0;
        }
    }
}
