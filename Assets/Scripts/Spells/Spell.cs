using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/New Spell")]
public class Spell : HotbarItem
{
    
    // Start is called before the first frame update
    [Header("About this spell")]
    public string description;
    public enum Element { Fire, Energy }
    public Element element;
    public bool isProjectile;
    public bool follow;

    [Header("Spell stats")]
    public int currentCooldown = 0;
    public int maxCooldown;
    public int damage;
    public int manaCost;
    public int maxRange;
    public int followSpeed;

    [Header("Spell graphics")]
    public GameObject projectile;

    public override string ColouredName { get; }
    public override string GetInfoDisplayText() {
        return description;
    }

    public void CastSpell(Unit spellCaster)
    {

        //PutOnCooldown();
    }

    public void CastSpell(Unit spellCaster, Unit target)
    {
        if(IsSpellReady())
        {
            PutOnCooldown();
            spellCaster.currentMana -= manaCost;
            spellCaster.SetMana();
            if (isProjectile)
            {
                GameObject instantiatedProj = Instantiate(projectile, spellCaster.transform.position, Quaternion.identity);
                Projectile instantiatedProjComponent = instantiatedProj.GetComponent<Projectile>();
                instantiatedProjComponent.Fire(spellCaster, target, damage, follow, followSpeed);
            }
        }
        else
        {

        }
        
    }

    

    public void PutOnCooldown()
    {
        CooldownManager.instance.StartCooldown(this);
    }

    public bool IsSpellReady()
    {
        if (currentCooldown <= 0)
            return true;
        else
            return false;
    }
}
