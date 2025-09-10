using System;
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

    public TextMeshProUGUI currentHPUI;
    public TextMeshProUGUI maxHPUI;
    public GameObject buttonDV;

    public CombatManager CM_manager;

    [Header("HP Lerp Settings")]
    [SerializeField] private float hpLerpSpeed = 100f; // unidades de HP por segundo
    private Coroutine playerHPLerpCoroutine;
    private Coroutine enemyHPLerpCoroutine;

    private void Awake()
    {
        CM_manager.updateHPEnemy += SetEnemyHP;
        CM_manager.updateHPPlayer += SetPlayerHP;
    }

    private void OnDestroy()
    {
        if (CM_manager != null)
        {
            CM_manager.updateHPEnemy -= SetEnemyHP;
            CM_manager.updateHPPlayer -= SetPlayerHP;
        }
    }

    public void SetHUD(Unit unit)
    {
        if (unit.isPlayer)
        {
            playerName.text = unit.unitName;
            //playerLevel.text = "Lvl " + unit.unitLevel;
            playerHpSlider.maxValue = unit.maxHP;
            playerHpSlider.value = unit.currentHP;
            maxHPUI.text = unit.maxHP.ToString();
            currentHPUI.text = playerHpSlider.value.ToString();
        }
        else
        {
            enemyName.text = unit.unitName;
            enemyLevel.text = unit.unitLevel.ToString();
            enemyHpSlider.maxValue = unit.maxHP;
            enemyHpSlider.value = unit.currentHP; 
        }
     
    }

    public void SetEnemyHP(int hp)
    {
        StartHPLerp(ref enemyHPLerpCoroutine, enemyHpSlider, hp);
    }
        public void SetPlayerHP(int hp)
    {
        StartHPLerp(ref playerHPLerpCoroutine, playerHpSlider, hp, updatePlayerCurrentLabel: true);
    }

    private void StartHPLerp(ref Coroutine runningCoroutine, Slider slider, int targetValue, bool updatePlayerCurrentLabel = false)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(LerpSliderValue(slider, targetValue, updatePlayerCurrentLabel));
    }

    private IEnumerator LerpSliderValue(Slider slider, int targetValue, bool updatePlayerCurrentLabel)
    {
        float target = targetValue;
        // Asegura límites válidos
        target = Mathf.Clamp(target, 0f, slider.maxValue);

        while (!Mathf.Approximately(slider.value, target))
        {
            float step = hpLerpSpeed * Time.deltaTime;
            slider.value = Mathf.MoveTowards(slider.value, target, step);
            if (updatePlayerCurrentLabel)
            {
                currentHPUI.text = Mathf.RoundToInt(slider.value).ToString();
            }
            yield return null;
        }

        // Ajuste final por si queda un delta mínimo
        slider.value = target;
        if (updatePlayerCurrentLabel)
        {
            currentHPUI.text = Mathf.RoundToInt(slider.value).ToString();
        }
    }

    public void isDV(int newHP)
    {
        buttonDV.SetActive(false);
        playerHpSlider.maxValue =newHP;
        playerHpSlider.value = newHP;
        currentHPUI.text = playerHpSlider.value.ToString();

    }


    
    
    
}
