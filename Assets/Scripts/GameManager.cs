using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private GameObject PlayerPF;
    //[SerializeField] public SceneInfo SceneInfo;
    [SerializeField] public UnitData playerDigimon;

    [SerializeField] public GameObject enemyPrefab = null;
    [SerializeField] public UnitData unitSO = null;
    [SerializeField] public int EXP = 0;
    [SerializeField] public int Gold = 0;
    [SerializeField] public int costTrainning;
    [SerializeField] public int costPlugin;
    [SerializeField] public bool DV = false;
    
    
    
    [SerializeField] private UnitData[] enemySO;
    //evento de que se gasto puntos de exp
    //evento de que se gasto gold
    
    
    
    private void Awake()
    {
        GameObject[] objs =GameObject.FindGameObjectsWithTag("GameController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (Instance != null && Instance!=this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;    
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void setPlayerDefaultStats()
    {
        playerDigimon.unitLevel = 1;
        playerDigimon.baseDamage = 10;
        playerDigimon.baseDef = 5;
        playerDigimon.baseMaxHp = 35;
        playerDigimon.wisdom = 5;
        
        
        
    }

    private void Start()
    {
        setPlayerDefaultStats();
        costTrainning = 5;
        costPlugin = 5;

    }

    public void returnMainIsland(bool battle)
    {
        SceneManager.LoadScene("Main Scene");
        unitSO = null;
        
        
    }

    public void SetEnemyColosseum(GameObject prefabEnemy)
    {
        //enemyName = prefabEnemy.name;
        enemyPrefab = prefabEnemy;
        unitSO = prefabEnemy.GetComponent<Enemy>().unitData;
    }
    public void ColosseumStart()
    {
        
        SceneManager.LoadScene("BattleScene");
    }

    public int costePlugin()
    {
        costPlugin = costPlugin * 2;
        return costPlugin;
    }

    public int costeTrainning()
    {
        costTrainning = costTrainning * 2;
        return costTrainning;
    }
    
}
