using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveWithKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode = KeyCode.None;
    [SerializeField] private GameObject toggledObject = null;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            toggledObject.SetActive(!toggledObject.activeSelf);
        }
    }

}
