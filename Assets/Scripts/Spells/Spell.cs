using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : HotbarItem
{

    // Start is called before the first frame update
    [Header("About this spell")]
    public string description;
    //public enum Element { Fire, Energy };
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

    [Header("Spell sounds")]
    public string projectileSoundEffectName;


    public override string ColouredName
    {
        get
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(element.TextColour);
            return $"<color=#{hexColour}>{Name}</color>";
        }



    }
    public override string GetInfoDisplayText() {
        return description;
    }

    public virtual void CastSpell(Unit spellCaster, Unit target)
    {

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
