using System.Collections;
using System.Collections.Generic;
using AbstractFactory;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PluginShop : MonoBehaviour
{
    public TextMeshProUGUI uiGold;
    public TextMeshProUGUI[] uiCost;
    private int cost;
    public AudioClip upgrade;

    public GameObject HP_sprite;
    public GameObject WSD_sprite;
    public GameObject DMG_sprite;
    
    
    
    
    public Sprite HP_basic;
    public Sprite HP_intermediate;
    public Sprite HP_advanced;
    public Sprite HP_ultimate;

    public Sprite WSD_basic;
    public Sprite WSD_intermediate;
    public Sprite WSD_advanced;
    public Sprite WSD_ultimate;

    public Sprite DMG_basic;
    public Sprite DMG_intermediate;
    public Sprite DMG_advanced;
    public Sprite DMG_ultimate;
    
    
    
    
    
    
    
    //evento de que se gasto gold
    //evento post batalla para recibir exp y actualizar ui
    void Start()
    {
        
        uiGold.text = GameManager.Instance.Gold.ToString();
        cost = GameManager.Instance.costPlugin;
        foreach (var i in uiCost)
        {
            i.text = cost.ToString();
            if (i.name == "DV")
            {
                i.text = "40";
            }
        }
        CheckPluginSprite();
    }

    public void CheckPluginSprite()
    {
        Sprite auxHP = null;
        Sprite auxWSD=null;
        Sprite auxDMG=null;
        if (cost < 10)
        {
            auxHP = HP_basic;
            auxWSD = WSD_basic;
            auxDMG = DMG_basic;
        }

        if (cost >=10)
        {
            auxHP = HP_intermediate;
            auxWSD = WSD_intermediate;
            auxDMG = DMG_intermediate;
        }

        if (cost>=20)
        {
            auxHP = HP_advanced;
            auxWSD = WSD_advanced;
            auxDMG = DMG_advanced;
        }

        if (cost >= 40)
        {
            auxHP = HP_ultimate;
            auxWSD = WSD_ultimate;
            auxDMG = DMG_ultimate;
        }
        HP_sprite.GetComponent<Image>().sprite=auxHP;
        WSD_sprite.GetComponent<Image>().sprite=auxWSD;
        DMG_sprite.GetComponent<Image>().sprite=auxDMG;
        
    }
    
    public void updateGold()
    {
        GameManager.Instance.Gold -= GameManager.Instance.costPlugin;
        uiGold.text = GameManager.Instance.Gold.ToString();
        cost = GameManager.Instance.costePlugin();
        foreach (var i in uiCost)
        {
            i.text = cost.ToString();
            if (i.name == "DV")
            {
                if (GameManager.Instance.DV ==false)
                {
                    i.text = "40";
                }else
                {
                    i.text = "Bought";
                }
               
            }
        }
        CheckPluginSprite();

    }


    public void factoryPlugin(string stat)
    {
        if (GameManager.Instance.Gold>=GameManager.Instance.costPlugin)
        {
            AudioManager.Instance.PlaySound(upgrade);
            var nuevoPlugin = new PluginFactory();
            Plugin pluginComprado = nuevoPlugin.GetPlugin(stat);
            Debug.Log($"Se obtuvo plugin {pluginComprado.tier.ToString()}, de {pluginComprado.stat}, que aumenta un {pluginComprado.ammount} ");
            ImportPluginDigimon(pluginComprado);
        }
       
    }

    public void ImportPluginDigimon(Plugin recentPlugin)
    {
        switch (recentPlugin.stat)
        {
            case "HP":
                pluginHP(recentPlugin.ammount);
                break;
            case "DMG":
               pluginDMG(recentPlugin.ammount);
               break;
            case "WSD":
                pluginWsd(recentPlugin.ammount);
                break;
        }
        
    }
    public void pluginHP(int pluginStats)
    {
            GameManager.Instance.playerDigimon.baseMaxHp += pluginStats;
            updateGold();
    }

    public void pluginWsd(int pluginStats)
    {
            GameManager.Instance.playerDigimon.wisdom += pluginStats;
            updateGold();
    }
    public void pluginDMG(int pluginStats)
    {
            GameManager.Instance.playerDigimon.baseDamage +=pluginStats;
            updateGold();
    }

        
    public void pluginDV()
    {
        if (GameManager.Instance.Gold>=40 && GameManager.Instance.DV ==false)
        {
            GameManager.Instance.DV = true;
            
            GameManager.Instance.Gold -= GameManager.Instance.costPlugin;
            uiGold.text = GameManager.Instance.Gold.ToString();
            foreach (var i in uiCost)
            {
                i.text = cost.ToString();
                if (i.name == "DV")
                {
                    i.text = "Bought";
                }
            }
        }
    }

}
