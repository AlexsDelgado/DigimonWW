using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleSate {START, PLAYER, ENEMY, WIN, LOSE}
public class CombatManager : MonoBehaviour
{

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

    private void Start()
    {
        state = BattleSate.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGameObject = Instantiate(playerPF, playerPosition);
        playerUnit = playerGameObject.GetComponent<Unit>();
        
        enemyGameObject = Instantiate(enemyPF, enemyPosition);
        enemyUnit = enemyGameObject.GetComponent<Unit>();

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
        texto.text = playerUnit.unitName + " ataca a "+enemyUnit.unitName;
        yield return new WaitForSeconds(2f);
        playerGameObject.transform.position = playerPosition.position;
        if (isDead)
        {
            state = BattleSate.WIN;
            EndBattle();
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
            EndBattle();
        }
        else
        {
            state = BattleSate.PLAYER;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleSate.WIN)
        {
            playerGameObject.GetComponent<Animator>().SetTrigger("Win");
            enemyGameObject.GetComponent<Animator>().SetTrigger("Lose");
            texto.text = "Ganaste";
        }else if (state == BattleSate.LOSE)
        {
            playerGameObject.GetComponent<Animator>().SetTrigger("Lose");
            enemyGameObject.GetComponent<Animator>().SetTrigger("Win");
            texto.text = "Perdiste";
        }
    }

    void PlayerTurn()
    {
        texto.text = "Elige una accion para "+playerUnit.unitName;
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(15); //hardcode amount 
        UI_instance.SetPlayerHP(playerUnit.currentHP);
        texto.text = playerUnit.unitName + " recupera un poco de fuerza";
        yield return new WaitForSeconds(2f);

        state = BattleSate.ENEMY;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleSate.PLAYER)
        {
            return;
        }

        StartCoroutine(Attack());
    }

    public void OnHealButton()
    {
        if (state != BattleSate.PLAYER)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
    
    public void terminaAttack()
    {
       
        enemyPF.GetComponent<Animator>().SetTrigger("Dmg");
        playerPF.transform.position = playerPosition.position;
    }
    
    
}
