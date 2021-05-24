using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : GAction
{
    public override bool PrePerform()
    {
        //if (Vector3.Distance(target.transform.position, transform.position) >= 1.5)
        //    return false;
        return true;
    }
    public override bool PostPerform()
    {
        //if (target.GetComponent<Unit>().currentHP == 0)
        //{
        //    GWorld.Instance.GetWorld().ModifyState("Dead", 1);
        //}
        return true;
    }
}
