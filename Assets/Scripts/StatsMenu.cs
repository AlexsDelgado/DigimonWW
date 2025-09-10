using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using TMPro;
using UnityEngine;

public class StatsMenu : MonoBehaviour, ICurrency
{
    
    public TextMeshProUGUI uiWins;
    public TextMeshProUGUI uiCost;
    public TextMeshProUGUI DMG;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI WSD;
    public TextMeshProUGUI DV;
    
    public UnitData digimon;
    public int cost;
    public int wins;

    public AudioClip upgrade;
    private void Start()
    {
        digimon = GameManager.Instance.playerDigimon;
        GetCurrency();
        SetCurrencyUI();
    }

    public void LevelUpdate()
    {
    
    }
    public void UseCurrency()
    {
       
    }

    public void UseCurrency(string stat)
    {
        switch (stat)
        {
            case "HP":
                //trainHP();
                break;
            case "AT":
                //trainDmg();
                break;
            case "WSD":
                //trainWsd();
                break;
            default:
                // DEBUG_REMOVED: Debug.Log("Invalid Command");
                break;
        }
        SetCurrencyUI();
        //event que actualiza stats
    }
    

    public void GetCurrency()
    {
        wins = GameManager.Instance.ColoWins;
        cost = GameManager.Instance.costLevelUp;
        
    }

    public void SetCurrencyUI()
    {
        
        wins = GameManager.Instance.ColoWins;
        uiWins.text = wins.ToString();
        uiCost.text = cost.ToString();
        DMG.text = digimon.baseDamage.ToString();
        WSD.text = digimon.wisdom.ToString();
        HP.text = digimon.baseMaxHp.ToString();
        if (GameManager.Instance.DV)
        {
            DV.text = "Grizzmon";
        }
        else
        {
            DV.text = "??";
        }
}

    public void TrainHP()
    {
        GetCurrency();
        if (wins>=cost)
        {
            AudioManager.Instance.PlaySound(upgrade);
            GameManager.Instance.playerDigimon.baseMaxHp += cost;
            // DEBUG_REMOVED: Debug.Log("HP actual de bearmon :" + GameManager.Instance.playerDigimon.baseMaxHp);
            UpdateCurrency();
        }
        else
        {
            // DEBUG_REMOVED: Debug.Log("Tu digimon no tiene suficiente experiencia");
        }
    }
    public void TrainDMG()
    {
        GetCurrency();
        if (wins>=cost)
        {
            AudioManager.Instance.PlaySound(upgrade);
            GameManager.Instance.playerDigimon.baseDamage += cost;
            // DEBUG_REMOVED: Debug.Log("DMG actual de bearmon :" + GameManager.Instance.playerDigimon.baseDamage);
            UpdateCurrency();
        }
        else
        {
            // DEBUG_REMOVED: Debug.Log("Tu digimon no puede hacer eso");
        }
    }
    public void TrainWSD()
    {
        GetCurrency();
        if (wins>=cost)
        {
            AudioManager.Instance.PlaySound(upgrade);
            GameManager.Instance.playerDigimon.wisdom += cost;
            // DEBUG_REMOVED: Debug.Log("WSD actual de bearmon :" + GameManager.Instance.playerDigimon.wisdom);
            UpdateCurrency();
        }
        else
        {
            // DEBUG_REMOVED: Debug.Log("Tu digimon no puede hacer eso");
        }
    }
    public void UpdateCurrency()
    {
        
        cost = GameManager.Instance.costeLvlUp();
        GetCurrency();
        SetCurrencyUI();
    }
}
