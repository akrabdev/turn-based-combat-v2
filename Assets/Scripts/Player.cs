using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("Dead", 1, true);
        goals.Add(s1, 3);
    }

}
