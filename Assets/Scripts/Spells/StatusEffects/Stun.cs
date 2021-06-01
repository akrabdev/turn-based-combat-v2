using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    Unit stunnedUnit;
    public Stun(int max, Unit stunnedUnit):base(max)
    {
        this.stunnedUnit = stunnedUnit;
        this.stunnedUnit.isStunned = true;
    }

    public override void Timer()
    {
        if (duration == maxDuration)
        {
            stunnedUnit.isStunned = false;
            stunnedUnit.statusEffects.Remove(this);
            return;
        }
        base.Timer();
        DamagePopupManager.instance.Setup("STUNNED!", Color.magenta, stunnedUnit.transform);
        
    }

}
