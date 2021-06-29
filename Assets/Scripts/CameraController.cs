using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Allows camera to follow player
    void Update()
    {
        Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
        transform.Translate(0, 0, player.transform.position.z - 0.5f);
    }
}
