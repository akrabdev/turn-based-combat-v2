using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : StatusEffect
{
    Unit shieldedUnit;
    // Start is called before the first frame update
    public Shield(int max, Unit shieldedUnit) : base(max)
    {
        this.shieldedUnit = shieldedUnit;
        this.shieldedUnit.isShielded = true;
    }

    public override void Timer()
    {
        if (duration == maxDuration)
        {
            shieldedUnit.isShielded = false;
            shieldedUnit.statusEffects.Remove(this);
            return;
        }
        base.Timer();
        DamagePopupManager.instance.Setup("SHIELDED!", Color.white, shieldedUnit.transform);

    }
}
