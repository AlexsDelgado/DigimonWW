using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerDigimon;

    [SerializeField] public SceneInfo SceneInfo;
    public void SetEnemyColosseum(GameObject prefabEnemy)
    {
        SceneInfo.enemyName = prefabEnemy.name;
        SceneInfo.prefab = prefabEnemy;
        SceneInfo.unitSO = prefabEnemy.GetComponent<Enemy>().unitData;



    }
    public void ColosseumStart()
    {
        
        SceneManager.LoadScene("BattleScene");
    }
    
}
