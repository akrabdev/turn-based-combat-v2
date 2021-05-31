using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager instance;
    public bool trainingMode;
    public GameObject player1;
    public GameObject player2;
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
            PlayerAgent agent1 = player1.GetComponent<PlayerAgent>();
            PlayerAgent agent2 = player2.GetComponent<PlayerAgent>();
            PlayerController2 playerController = player1.GetComponent<PlayerController2>();
            agent1.enabled = true;
            playerController.enabled = false;
            BattleSystem.instance.switchTurnTime = 0;
        }
    }
}
