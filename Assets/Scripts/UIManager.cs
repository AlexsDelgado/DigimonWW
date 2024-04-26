using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerLevel;
    public Slider playerHpSlider;


    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyLevel;
    public Slider enemyHpSlider;
    public void SetHUD(Unit unit)
    {
        if (unit.isPlayer)
        {
            playerName.text = unit.unitName;
            //playerLevel.text = "Lvl " + unit.unitLevel;
            playerHpSlider.maxValue = unit.maxHP;
            playerHpSlider.value = unit.currentHP;
        }
        else
        {
            enemyName.text = unit.unitName;
            //enemyLevel.text = "Lvl " + unit.unitLevel;
            enemyHpSlider.maxValue = unit.maxHP;
            enemyHpSlider.value = unit.currentHP; 
        }
     
    }

    public void SetEnemyHP(int hp)
    {
        enemyHpSlider.value = hp;
    }
        public void SetPlayerHP(int hp)
    {
        playerHpSlider.value = hp;
    }
    
    
}
