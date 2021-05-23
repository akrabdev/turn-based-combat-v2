using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public abstract class GAction : MonoBehaviour
{
    public string actionName = "Move";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0f;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
   // public Seeker seeker; //= GameObject.FindGameObjectWithTag("Player").GetComponent<Seeker>();

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> aftereffects;

    public WorldStates agentBeliefs;

    public bool running = false;
    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        aftereffects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        
        if (preConditions != null)
        {
            foreach(WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }

        {
            foreach (WorldState w in afterEffects)
            {
                aftereffects.Add(w.key, w.value);

            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if(!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();

}
