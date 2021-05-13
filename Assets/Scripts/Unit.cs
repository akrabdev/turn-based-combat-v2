using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class is responsible for the unit in the scene. A unit can be either a player or an enemy for now.
/// </summary>
public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;
    public int experiencePoints;

	public int damage;
    public int magicPower;

	public int maxHP;
	public int currentHP;

    public int maxMana;
    public int currentMana;

    public Text nameText;
    public Text levelText;

    public Slider hpSlider;
    public Slider manaSlider;

    //Just for deleveling for now
    private bool hpUpdated;
    

    private void Start()
    {
        SetHUD();
    }

    /// <summary>
    /// This function attacks current unit. It generates a random damage according to current damage value and decreases the currentHP. 
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns></returns>
    public bool TakeDamage(int dmg)
	{
        int randDmg = Random.Range(dmg - 5, dmg + 5);
		currentHP -= randDmg;
        SetHP();

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

    /// <summary>
    /// This function generates a random value to heal with according to the unit's magic power and increases the currentHP.
    /// </summary>
	public void Heal()
	{
        int randHeal = Random.Range(magicPower - 5, magicPower + 5);
		currentHP += randHeal;
		if (currentHP > maxHP)
			currentHP = maxHP;
        SetHP();
	}


    /// <summary>
    /// This function adds experience points to player which increases fast for smaller levels and plataus for higher levels. A function that exhibits
    /// this behavior is the log function. When a unit's experience points are bigger than a certain value the unit levels up.
    /// </summary>
    /// <param name="amount"></param>
    public void addExperience(int amount)
    {
        experiencePoints += amount;
        if (experiencePoints >= Mathf.RoundToInt( 200 * Mathf.Log10(unitLevel) + 100))

        {
            levelUp();
        }
    }

    /// <summary>
    /// This function removes experience points from the unit when it loses. When it reaches the experience points of the previous level it levels down.
    /// </summary>
    /// <param name="amount"></param>
    public void removeExperience(int amount)
    {
        experiencePoints -= amount;
        if(experiencePoints < 0)
        {
            experiencePoints = 0;
        }
        //level 2 exp 100, current lvl 1 exp 0
        if (unitLevel == 1)
            return;
        if (experiencePoints < Mathf.RoundToInt(200 * Mathf.Log10(unitLevel-1) + 100))
        {
            levelDown();
        }
    }

    /// <summary>
    /// This function levels up either the damage value or the maxHP randomly.
    /// </summary>
    public void levelUp()
    {
        unitLevel++;
        //Player chooses which stat to increase in the future
        int rand = Random.Range(0, 9);
        if (rand >= 5)
        {
            damage = Mathf.RoundToInt(20 * Mathf.Log10(unitLevel) + 10);
            hpUpdated = true;
        }
        else
        {
            maxHP = Mathf.RoundToInt(50 * Mathf.Log10(unitLevel) + 100);
            hpUpdated = false;
        }
    }

    /// <summary>
    /// This function levels down unit and its increased damage/maxHP
    /// </summary>
    public void levelDown()
    {
        unitLevel--;
        //Player chooses which stat to increase in the future
        int rand = Random.Range(0, 9);
        if (hpUpdated)
            damage = Mathf.RoundToInt(20 * Mathf.Log10(unitLevel) + 10);
        else
            maxHP = Mathf.RoundToInt(50 * Mathf.Log10(unitLevel) + 100);
        //\ 200\cdot\log\left(x\right)\ +\ 100
    }

    /// <summary>
    /// This function sets the attributes of the HUD, including the unit name, level, HP and Mana.
    /// </summary>
    public void SetHUD()
    {
        nameText.text = unitName;
        levelText.text = "Lvl " + unitLevel;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }


    /*
     * Some setters 
     */

    public void SetHP()
    {
        hpSlider.value = currentHP;
    }

    public void SetMana()
    {
        manaSlider.value = currentMana;
    }

}
