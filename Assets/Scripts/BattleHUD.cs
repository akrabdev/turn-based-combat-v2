using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class makes the HUD follow the player and enemy.
/// </summary>
public class BattleHUD : MonoBehaviour
{
    public GameObject objectToFollow;

    public void Update()
    {
        transform.position = objectToFollow.transform.position + new Vector3(0, 1f, 0);
    }

}
