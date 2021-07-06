using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public float maxRange;
    public int followSpeed;
    public bool onSelf;
    public bool multiple;
    public int multipleNumber;

    [Header("Spell graphics")]
    public GameObject projectile;
    public ParticleSystem[] effect;

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
    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        // builder.Append("<size=35>").Append(item.ColouredName).Append("</size>").AppendLine();
        builder.Append(element.Name).AppendLine();

        builder.Append(description).AppendLine();

        builder.Append("Damage: ").Append(damage).AppendLine();
        builder.Append("Mana Cost: ").Append(manaCost).AppendLine();
        builder.Append("Cooldown: ").Append(maxCooldown).AppendLine();
        builder.Append("Max Range: ").Append(maxRange).AppendLine();
        return builder.ToString();
    }

    public virtual bool CastSpell(Unit spellCaster, Unit target)
    {
        if (IsSpellReady() && spellCaster.currentMana >= manaCost)
        {
            if(!onSelf)
            {

                if (Vector3.Distance(target.transform.position, spellCaster.transform.position) <= maxRange)
                {
                    PutOnCooldown();
                    spellCaster.currentMana -= manaCost;
                    spellCaster.SetMana();
                    return true;
                }
                else
                    return false;
            }
            else
            {
                PutOnCooldown();
                spellCaster.currentMana -= manaCost;
                spellCaster.SetMana();
                return true;
            }
        }
        else
            return false;
            
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
