using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleSate {START, PLAYER, ENEMY, WIN, LOSE}

public class CombatManager : MonoBehaviour
{

    //refactor unit.cs a UnitData.cs


    public GameObject playerPF;
    public GameObject enemyPF;

    private GameObject playerGameObject;
    private GameObject enemyGameObject;

    public Transform playerPosition;
    public Transform enemyPosition;

    public Transform attackPosition;

    private Unit playerUnit;
    private Unit enemyUnit;

    public TextMeshProUGUI texto;

    public UIManager UI_instance;
    public BattleSate state;


    [SerializeField] public SceneInfo sceneInfo;
    [SerializeField] public UnitData playerData;
    [SerializeField] public UnitData enemyData;

    [SerializeField] public GameObject EvolutionPf;
    private bool accion = true;

    private void Start()
    {

        state = BattleSate.START;
        enemyData = GameManager.Instance.unitSO;
        playerData = GameManager.Instance.playerDigimon;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGameObject = Instantiate(playerPF, playerPosition);
        playerUnit = playerGameObject.GetComponent<Unit>();

        enemyGameObject = Instantiate(GameManager.Instance.enemyPrefab, enemyPosition);
        enemyUnit = enemyGameObject.GetComponent<Unit>();

        enemyUnit.setUnit(enemyData);
        playerUnit.setUnit(playerData);

        //enemyGameObject = Instantiate(enemyPF, enemyPosition);
        //enemyUnit = enemyGameObject.GetComponent<Unit>();

        texto.text = "Preparate para pelear contra " + enemyUnit.unitName;

        UI_instance.SetHUD(playerUnit); //setea la info de player en el UI
        UI_instance.SetHUD(enemyUnit); //setea la info de enemigo en el UI
        yield return new WaitForSeconds(2f);

        state = BattleSate.PLAYER;
        PlayerTurn();

    }

    IEnumerator Attack()
    {
        playerGameObject.transform.position = attackPosition.position;
        playerGameObject.GetComponent<Animator>().Play("attack1");
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);


        UI_instance.SetEnemyHP(enemyUnit.currentHP);
        texto.text = playerUnit.unitName + " ataca a " + enemyUnit.unitName;
        yield return new WaitForSeconds(2f);
        playerGameObject.transform.position = playerPosition.position;
        if (isDead)
        {
            state = BattleSate.WIN;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleSate.ENEMY;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        texto.text = enemyUnit.unitName + " ataca a " + playerUnit.unitName;
        enemyGameObject.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerGameObject.GetComponent<Animator>().SetTrigger("Dmg");
        UI_instance.SetPlayerHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleSate.LOSE;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleSate.PLAYER;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleSate.WIN)
        {
            playerGameObject.GetComponent<Animator>().SetTrigger("Win");
            enemyGameObject.GetComponent<Animator>().SetTrigger("Lose");
            texto.text = "Ganaste";
            GameManager.Instance.EXP += enemyData.reward;
            GameManager.Instance.Gold += enemyData.reward;
            Debug.Log("exp :" + GameManager.Instance.EXP);
            Debug.Log("gold :" + GameManager.Instance.Gold);
            yield return new WaitForSeconds(2f);
            GameManager.Instance.returnMainIsland(true);

        }
        else if (state == BattleSate.LOSE)
        {
            playerGameObject.GetComponent<Animator>().SetTrigger("Lose");
            enemyGameObject.GetComponent<Animator>().SetTrigger("Win");
            texto.text = "Perdiste";
            yield return new WaitForSeconds(2f);
            GameManager.Instance.returnMainIsland(false);
        }
    }

    void PlayerTurn()
    {
        texto.text = "Elige una accion para " + playerUnit.unitName;
        accion = true;
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerData.wisdom);
        UI_instance.SetPlayerHP(playerUnit.currentHP);
        texto.text = playerUnit.unitName + " recupera un poco de fuerza";
        yield return new WaitForSeconds(2f);

        state = BattleSate.ENEMY;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleSate.PLAYER || accion == false)
        {
            return;
        }

        accion = false;
        StartCoroutine(Attack());
    }

    public void OnHealButton()
    {
        if (state != BattleSate.PLAYER || accion == false)
        {
            return;
        }

        accion = false;
        StartCoroutine(PlayerHeal());
    }

    public void terminaAttack()
    {

        enemyPF.GetComponent<Animator>().SetTrigger("Dmg");
        playerPF.transform.position = playerPosition.position;
    }

    public void Digievolution()
    {
        if (state  == BattleSate.PLAYER && accion==true)
        {
            if (GameManager.Instance.DV == true)
            {
                playerGameObject.SetActive(false);
          
                playerGameObject = Instantiate(EvolutionPf, playerPosition);
                playerUnit.damage = playerUnit.damage * 2;
                playerUnit.currentHP = playerUnit.maxHP * 2;
            

            
                UI_instance.isDV( playerUnit.currentHP);
                //UI_instance.SetPlayerHP(playerUnit.currentHP);
            }
            else
            {
                texto.text = "Tu digimon no ha desbloqueado su digievolucion";
            }
        }
 
    }


}
