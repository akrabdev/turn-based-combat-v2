using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public bool TakeDamage(int dmg)
	{
        int randDmg = Random.Range(dmg - 5, dmg + 5);
		currentHP -= randDmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
        int randHeal = Random.Range(amount - 5, amount + 5);
		currentHP += randHeal;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}
