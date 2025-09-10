using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isPlayer;
    public string unitName;
    public int unitLevel;

    public int damage;
    public int defense;
    public int maxHP;
    
    public int currentHP;

    public void setUnit(UnitData unitData)
    {
        unitName = unitData.unitName;
        unitLevel = unitData.unitLevel;
        
        
        //subir stats digimon
        defense = unitData.baseDef * unitLevel;
        maxHP = unitData.baseMaxHp * unitLevel;
        currentHP = maxHP;
        damage = unitData.baseDamage * unitLevel;
    }
    public void TakeDamage(int dmg)
    {
        this.GetComponent<Animator>().SetTrigger("Dmg");
        currentHP -= dmg;
        // if (currentHP <= 0)
        // {
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }
    }

    public bool isDead()
    {
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
