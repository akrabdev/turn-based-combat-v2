using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager instance;
    private List<Spell> spellsOnCooldown = new List<Spell>();
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void SwitchTurn()
    {
        for (int i = 0; i < spellsOnCooldown.Count; i++)
        {
            spellsOnCooldown[i].currentCooldown -= 1;
            if (spellsOnCooldown[i].currentCooldown <= 0)
            {
                spellsOnCooldown[i].currentCooldown = 0;
                spellsOnCooldown.Remove(spellsOnCooldown[i]);
            }
        }
    }

    public void StartCooldown(Spell spell)
    {
        if (!spellsOnCooldown.Contains(spell))
        {
            if (spell.maxCooldown == 0)
                return;
            spell.currentCooldown = spell.maxCooldown;
            spellsOnCooldown.Add(spell);
        }
    }
}
// Cooldown Manager Com