using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloatEffect : MonoBehaviour
{
    // Update is called once per frame
    public float moveSpeed;
    public float disappearTime;
    private void Awake()
    {
        Destroy(gameObject, disappearTime);
    }
    void Update()
    {
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
    }
}
