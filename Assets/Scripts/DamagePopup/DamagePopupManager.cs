using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager instance;
    public TextMeshPro textMesh;
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

    public void Setup(int damageAmount, Color color, Transform damageTaker)
    {
        textMesh.SetText(damageAmount.ToString());
        textMesh.color = color;
        Instantiate(textMesh, damageTaker.position, Quaternion.identity);
    }
}
