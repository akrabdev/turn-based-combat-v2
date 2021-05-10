using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public GameObject objectToFollow;

    public void Update()
    {
        transform.position = objectToFollow.transform.position + new Vector3(0, 1f, 0);
    }

}
