using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : StatusEffect
{
    Unit buffedUnit;
    int dmgBuff;
    // Start is called before the first frame update
    public Rage(int max, int dmgBuff, Unit buffedUnit) : base(max)
    {
        this.buffedUnit = buffedUnit;
        this.dmgBuff = dmgBuff;
        this.buffedUnit.damagePower += dmgBuff;
    }

    public override void Timer()
    {
        if (duration == maxDuration)
        {
            buffedUnit.damagePower -= dmgBuff;
            buffedUnit.statusEffects.Remove(this);
            return;
        }
        base.Timer();
    }
}
