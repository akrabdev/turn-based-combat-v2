using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rarity : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private Color textColour;

    public string Name { get { return name; } }

    public Color TextColour { get { return textColour; } }



}
