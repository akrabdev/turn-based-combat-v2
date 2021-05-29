using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToEnemy : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().AddState("CloseToEnemy", 1);
        GWorld.Instance.GetWorld().ModifyState("FarFromEnemy", -1);
        return true;
    }
}
