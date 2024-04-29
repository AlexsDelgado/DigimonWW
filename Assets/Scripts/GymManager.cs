using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GymManager : MonoBehaviour
{
    
    public TextMeshProUGUI uiExp;
    public TextMeshProUGUI[] uiCost;

    private int precio;
    // Start is called before the first frame update
    void Start()
    {
        uiExp.text = GameManager.Instance.EXP.ToString();
        precio = GameManager.Instance.costTrainning;
        foreach (var i in uiCost)
        {
            i.text = precio.ToString();
            Debug.Log("cuesta esto entrenar: " + i.text);
        }
    }
    //evento de que se gasto puntos de exp
    //evento post batalla para recibir exp y actualizar ui
    public void updateExp()
    {
        GameManager.Instance.EXP -= GameManager.Instance.costTrainning;
        uiExp.text = GameManager.Instance.EXP.ToString();
        precio = GameManager.Instance.costeTrainning();
        //GameManager.Instance.costTrainning = GameManager.Instance.costTrainning * 2;
        foreach (var i in uiCost)
        {
            i.text = precio.ToString();
        }
      
    }

    public void trainHP()
    {
        if (GameManager.Instance.EXP>=GameManager.Instance.costTrainning)
        {
            GameManager.Instance.playerDigimon.baseMaxHp += GameManager.Instance.costTrainning;
            Debug.Log("HP actual de bearmon :" + GameManager.Instance.playerDigimon.baseMaxHp);
            updateExp();
        }
        else
        {
            Debug.Log("Tu digimon no tiene suficiente experiencia");
        }
    }

    public void trainDmg()
    {
        if (GameManager.Instance.EXP>=GameManager.Instance.costTrainning)
        {
            GameManager.Instance.playerDigimon.baseDamage += GameManager.Instance.costTrainning;
            Debug.Log("ataque actual de bearmon :" + GameManager.Instance.playerDigimon.baseDamage);
            updateExp();
        }
        else
        {
            Debug.Log("Tu digimon no tiene suficiente experiencia");
        }
    }

    public void trainWsd()
    {
        if (GameManager.Instance.EXP>=GameManager.Instance.costTrainning)
        {
            GameManager.Instance.playerDigimon.wisdom += GameManager.Instance.costTrainning;
            Debug.Log("Curacion acutal de bearmon :" + GameManager.Instance.playerDigimon.wisdom);
            updateExp();
        }
        else
        {
            Debug.Log("Tu digimon no tiene suficiente experiencia");
        }
    }
}
