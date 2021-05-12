using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //[System.Serializable]
    //public struct buffsStruct
    //{
    //    public string name;
    //    public int turn;
    //}

    //public buffsStruct[] buffsArray;

    //public Dictionary<string, int> buffs;

    private void Start()
    {
        SetHUD();
        //buffs = new Dictionary<string, int>();
        //buffs.Add("Shield", 0);

    }

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

	public void Heal()
	{
        int randHeal = Random.Range(magicPower - 5, magicPower + 5);
		currentHP += randHeal;
		if (currentHP > maxHP)
			currentHP = maxHP;
        SetHP();
	}

    public void addExperience(int amount)
    {
        experiencePoints += amount;
        if (experiencePoints >= Mathf.RoundToInt( 200 * Mathf.Log10(unitLevel) + 100))

        {
            levelUp();
        }
    }

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
        //\ 200\cdot\log\left(x\right)\ +\ 100
    }

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

    public void SetHUD()
    {
        nameText.text = unitName;
        levelText.text = "Lvl " + unitLevel;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }

    public void SetHP()
    {
        hpSlider.value = currentHP;
    }

    public void SetMana()
    {
        manaSlider.value = currentMana;
    }

}
