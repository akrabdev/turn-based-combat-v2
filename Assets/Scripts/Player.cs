using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("CloseToEnemy", 1, true);
        goals.Add(s1, 3);
        GWorld.Instance.GetWorld().AddState("FarFromEnemy", 1);
        GWorld.Instance.GetWorld().AddState("HighHealth", 1);
    }

    private void Update()
    {
        if (gameObject.GetComponent<Unit>().currentHP < 30)
        {
            if (!GWorld.Instance.GetWorld().HasState("LowHealth"))
            {
                GWorld.Instance.GetWorld().ModifyState("HighHealth", -1);
                GWorld.Instance.GetWorld().ModifyState("MediumHealth", -1);
                GWorld.Instance.GetWorld().AddState("LowHealth", 1);
                GWorld.Instance.GetWorld().ModifyState("Healed", -1);
            }
            SubGoal s2 = new SubGoal("Healed", 1, true);
            if (!goals.ContainsKey(s2))
                goals.Add(s2, 3);
        }
        else if(gameObject.GetComponent<Unit>().currentHP < 60)
        {
            if (!GWorld.Instance.GetWorld().HasState("MediumHealth"))
            {
                GWorld.Instance.GetWorld().ModifyState("HighHealth", -1);
                GWorld.Instance.GetWorld().ModifyState("LowHealth", -1);
                GWorld.Instance.GetWorld().AddState("MediumHealth", 1);
                GWorld.Instance.GetWorld().ModifyState("Healed", -1);
            }
            SubGoal s2 = new SubGoal("Healed", 1, true);
            if (!goals.ContainsKey(s2))
                goals.Add(s2, 3);
        }
        else
        {
            if (!GWorld.Instance.GetWorld().HasState("HighHealth"))
            {
                GWorld.Instance.GetWorld().ModifyState("MediumHealth", -1);
                GWorld.Instance.GetWorld().ModifyState("LowHealth", -1);
                GWorld.Instance.GetWorld().AddState("HighHealth", 1);
            }
        }

        if(GWorld.Instance.GetWorld().HasState("CloseToEnemy"))
        {
            SubGoal s1 = new SubGoal("EnemyHealthDecreased", 1, true);
            if (!goals.ContainsKey(s1))
                goals.Add(s1, 3);
        }
        else
        {
            SubGoal s1 = new SubGoal("CloseToEnemy", 1, true);
            if (!goals.ContainsKey(s1))
                goals.Add(s1, 3);
        }
    }
}
