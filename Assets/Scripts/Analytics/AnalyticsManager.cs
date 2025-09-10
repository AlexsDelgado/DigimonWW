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
    void Start()
    {
        UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
    }

    public void Metric_Defeat()
    {
        // if (!_isInitialized)
        // {
        //     return;
        // }
        // Event myEvent = new Event("Defeat_Count", 1);
        // AnalyticsService.Instance.RecordInternalEvent(myEvent);
    }

    public void Metric_Attack()
    {
        // if (!_isInitialized)
        // {
        //     return;
        // }
        // Event myEvent = new Event("Attack_Count");
        // AnalyticsService.Instance.RecordInternalEvent(myEvent);
    }

    public void Metric_Win()
    {
        // if (!_isInitialized)
        // {
        //     return;
        // }
        // Event myEvent = new Event("Win_Count", 1);
        // AnalyticsService.Instance.RecordInternalEvent(myEvent);
    }

    public void Metric_Skill()
    {
        // if (!_isInitialized)
        // {
        //     return;
        // }
        // Event myEvent = new Event("Skill_Count", 1);
        // AnalyticsService.Instance.RecordInternalEvent(myEvent);
    }

    public void Metric_Heal()
    {
        if (!_isInitialized)
        {
            return;
        }
        // Event myEvent = new Event("Heal_Count", 1);
        // AnalyticsService.Instance.CustomData("Heal_Count",new Dictionary<string, object>());
        // AnalyticsService.Instance.RecordInternalEvent(myEvent);
        // AnalyticsService.Instance.
    }
}



//
// public class WinBattleEvent : Unity.Services.Analytics.Internal.Event
// {
//     public WinBattleEvent() : base("WinBattleEvent",1)
//     {
//     }
//     public int xx { set { Parameters(); }}
//     public string yy { set { SetParameter("yy", value); } }
//     
// }
