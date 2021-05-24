using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : GAction
{
    public override bool PrePerform()
    {
        if(target.GetComponent<Unit>().currentHP == 0)
            return true;
        return false;
    }
    public override bool PostPerform()
    {
        return true;
    }
}
