using TMPro;
using UnityEngine;

public interface ICurrency
{

    void UseCurrency(string a);
    void GetCurrency();
    void SetCurrencyUI();

    void SetCurrencyUI(TextMeshProUGUI ActualCurrency,int actualCurrencyValue,TextMeshProUGUI[] items,int cost )
    {
        ActualCurrency.text = actualCurrencyValue.ToString();
        foreach (var i in items)
        {
            i.text = cost.ToString();
        }
    }

    // void TrainHP(int ActualCurency,int cost)
    // {
    //     if (ActualCurency>=cost)
    //     {
    //         GameManager.Instance.playerDigimon.baseMaxHp += cost;
    //         Debug.Log("HP actual de bearmon :" + GameManager.Instance.playerDigimon.baseMaxHp);
    //         UpdateCurrency();
    //     }
    //     else
    //     {
    //         Debug.Log("Tu digimon no puede hacer eso");
    //     }
    // }
    void TrainHP();
    void TrainDMG();
    void TrainWSD();
    void UpdateCurrency();
}
