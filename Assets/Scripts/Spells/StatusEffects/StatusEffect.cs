using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public int maxDuration;
    public int duration;

    public StatusEffect(int max)
    {
        maxDuration = max;
        duration = 0;
    }
    
    public virtual void Timer()
    {
        duration += 1;
    }
}
