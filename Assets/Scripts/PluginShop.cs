using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PluginShop : MonoBehaviour
{
    public TextMeshProUGUI uiGold;
    public TextMeshProUGUI[] uiCost;

    private int cost;
    //evento de que se gasto gold
    //evento post batalla para recibir exp y actualizar ui
    void Start()
    {
        uiGold.text = GameManager.Instance.Gold.ToString();
        cost = GameManager.Instance.costPlugin;
        foreach (var i in uiCost)
        {
            i.text = cost.ToString();
            //Debug.Log("cuesta esto comprar plugin: " + i.text);
            if (i.name == "DV")
            {
                i.text = "40";
            }
        }
    }
    
    
    public void updateGold()
    {
        GameManager.Instance.Gold -= GameManager.Instance.costPlugin;
        uiGold.text = GameManager.Instance.Gold.ToString();
        cost = GameManager.Instance.costePlugin();
        //GameManager.Instance.costTrainning = GameManager.Instance.costPlugin * 2;
        foreach (var i in uiCost)
        {
            i.text = cost.ToString();
            if (i.name == "DV")
            {
                i.text = "40";
            }
        }
      
    }
    
    
    public void pluginHP()
    {
        if (GameManager.Instance.Gold>=GameManager.Instance.costPlugin)
        {
            //agrega al inventario:
            GameManager.Instance.playerDigimon.baseMaxHp += GameManager.Instance.costPlugin;
            Debug.Log("HP actual de bearmon con plugin es:" + GameManager.Instance.playerDigimon.baseMaxHp);
            updateGold();
        }
        else
        {
            Debug.Log("No tienes suficientes recursos");
        }
    }
    
    
    
        
    public void pluginWsd()
    {
        if (GameManager.Instance.Gold>=GameManager.Instance.costPlugin)
        {
            //agrega al inventario:
            GameManager.Instance.playerDigimon.wisdom += GameManager.Instance.costPlugin;
            Debug.Log("la curacion de bearmon con plugin es:" + GameManager.Instance.playerDigimon.wisdom);
            updateGold();
        }
        else
        {
            Debug.Log("No tienes suficientes recursos");
        }
    }
    public void pluginDMG()
    {
        if (GameManager.Instance.Gold>=GameManager.Instance.costPlugin)
        {
            //agrega al inventario:
            GameManager.Instance.playerDigimon.baseDamage += GameManager.Instance.costPlugin;
            Debug.Log("ataque actual de bearmon con plugin es:" + GameManager.Instance.playerDigimon.baseDamage);
            updateGold();
        }
        else
        {
            Debug.Log("No tienes suficientes recursos");
        }
    }

    
        
    public void pluginDV()
    {
        if (GameManager.Instance.Gold>=40)
        {
            //compra update de digievolucion
            GameManager.Instance.DV = true;
            updateGold();
        }
        else
        {
            Debug.Log("No tienes suficientes recursos");
        }
    }

    
    
   
    
    
}
