using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;
using UnityEngine.SceneManagement;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    private bool _isInitialized = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (Exception e)
        {
            Debug.LogError($"Analytics initialization failed: {e}");
        }
    }

    public void GiveConsent()
    {
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
        Debug.Log("Analytics consent given! Data collection started.");
    }

    public void Metric_Defeat(int defeatCount, string battleType)
    {
        if (!_isInitialized)
        {
            return;
        }
        
        DefeatEvent evt = new DefeatEvent(defeatCount, battleType);
        
        AnalyticsService.Instance.CustomData(evt.eventName, evt.parameters);
        AnalyticsService.Instance.Flush();
    }

    public void Metric_Attack(string attackType, int damage)
    {
        if (!_isInitialized)
        {
            return;
        }
        
        AttackEvent evt = new AttackEvent(attackType, damage);
        
        AnalyticsService.Instance.CustomData(evt.eventName, evt.parameters);
        AnalyticsService.Instance.Flush();
    }

    public void Metric_Win(int winCount, string battleType)
    {
        if (!_isInitialized)
        {
            return;
        }
        
        WinEvent evt = new WinEvent(winCount, battleType);
        
        AnalyticsService.Instance.CustomData(evt.eventName, evt.parameters);
        AnalyticsService.Instance.Flush();
    }

    public void Metric_Skill(string skillName , int skillCount)
    {
        if (!_isInitialized)
        {
            return;
        }
        
        SkillEvent evt = new SkillEvent(skillName, skillCount);
        
        AnalyticsService.Instance.CustomData(evt.eventName, evt.parameters);
        AnalyticsService.Instance.Flush();
    }

    public void Metric_Heal(int healAmount = 0, int healCount = 1)
    {
        if (!_isInitialized)
        {
            return;
        }
        
        HealEvent evt = new HealEvent(healAmount, healCount);
        
        AnalyticsService.Instance.CustomData(evt.eventName, evt.parameters);
        AnalyticsService.Instance.Flush();
    }
}
