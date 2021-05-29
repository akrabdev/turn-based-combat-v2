using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndHeal : GAction
{
    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Healed", -1);
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("CloseToEnemy", -1);
        GWorld.Instance.GetWorld().AddState("FarFromEnemy", 1);
        GWorld.Instance.GetWorld().AddState("Healed", 1);
        return true;
    }
}