using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : StatusEffect
{
    Unit dotUnit;
    int dmg;
    Element element;
    // Start is called before the first frame update
    public DOT(int max, int dmg, Element element, Unit dotUnit) : base(max)
    {
        this.dotUnit = dotUnit;
        this.dmg = dmg;
        this.element = element;
    }

    public override void Timer()
    {
        if (duration == maxDuration)
        {
            dotUnit.statusEffects.Remove(this);
            return;
        }
        base.Timer();
        dotUnit.TakeDamage(dmg, null, element);

    }
}
