using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using TMPro;
using TypeObject;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool dv = false;
    private bool accion = true;
    private bool skill = true;

    [SerializeField] public Enemy typeEnemy;
    [SerializeField] private AudioClip musicBoss;
    [SerializeField] private AudioClip musicColo;
    [SerializeField] private AudioClip Win;
    [SerializeField] private AudioClip Lose;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip attackPlayer;
    [SerializeField] private AudioClip attackBoss;
    
    
    //eventos para UI y Audoi

    public event Action<int> updateHPEnemy;
    public event Action<int> updateHPPlayer;
    public event Action<AudioClip> music;
    public event Action<AudioClip> sfx;
    
    private void Start()
    {
        AudioManager.Instance.StartCombat();
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
        typeEnemy = enemyGameObject.GetComponent<BossEnemy>();
        if (typeEnemy is BossEnemy)
        {
            //es el boss final musica epica
            music(musicBoss);
        }
        else
        {
            // musica coliseo
            music(musicColo);
        }

        enemyUnit.setUnit(enemyData);
        playerUnit.setUnit(playerData);

        //enemyGameObject = Instantiate(enemyPF, enemyPosition);
        //enemyUnit = enemyGameObject.GetComponent<Unit>();

        texto.text = "Preparate para pelear contra " + enemyUnit.unitName;

        UI_instance.SetHUD(playerUnit); //setea la info de player en el UI
        UI_instance.SetHUD(enemyUnit); //setea la info de enemigo en el UI
        yield return new WaitForSeconds(2f);

        state = BattleSate.PLAYER;
        skill = true;
        PlayerTurn();

    }

    // private void ProcessCommand(ICommand comando)
    // {
    //     //execute
    // }
    
    
    IEnumerator SkillAttack()
    {
        var skillCommand = new SkillAttack(playerUnit.damage, enemyUnit, playerGameObject, playerGameObject.GetComponent<Animator>(), attackPosition);
        EventQueue.Instance.QueueCommand(skillCommand);
        //playerGameObject.transform.position = attackPosition.position;
        //playerGameObject.GetComponent<Animator>().Play("attack1");
       
       
        
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyUnit.TakeDamage(playerUnit.damage*2);
        
        bool isDead = enemyUnit.isDead();
        
        texto.text = playerUnit.unitName + " ataca a " + enemyUnit.unitName;
        sfx(attackPlayer);
        updateHPEnemy(enemyUnit.currentHP);
        
        // Registrar métrica de habilidad especial del jugador
        if(dv)
        {
            AnalyticsManager.instance.Metric_Skill("Grizzly Claw", 1);
        }
        else
        {
            AnalyticsManager.instance.Metric_Skill(playerData.skill, 1);
        }
        //AnalyticsManager.instance.Metric_Skill(playerUnit.skill, 1);
        
        yield return new WaitForSeconds(2f);
  
        playerGameObject.transform.position = playerPosition.position;
        if (isDead)
        {
            if (typeEnemy is BossEnemy)
            { 
                SceneManager.LoadScene("EndGame");//end game
            }
            state = BattleSate.WIN;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleSate.ENEMY;
            StartCoroutine(EnemyTurn());
        }

    }

    
    IEnumerator Attack()
    {
        var comandoAttack = new AttackCommand(playerUnit.damage, enemyUnit, playerGameObject, playerGameObject.GetComponent<Animator>(), attackPosition);
        EventQueue.Instance.QueueCommand(comandoAttack);
        //playerGameObject.transform.position = attackPosition.position;
        //playerGameObject.GetComponent<Animator>().Play("attack1");
       
       
        
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyUnit.TakeDamage(playerUnit.damage);
        bool isDead = enemyUnit.isDead();
        sfx(attackPlayer);
        texto.text = playerUnit.unitName + " ataca a " + enemyUnit.unitName;
        updateHPEnemy(enemyUnit.currentHP);
        
        // Registrar métrica de ataque del jugador
        //AnalyticsManager.instance.Metric_Attack();
        AnalyticsManager.instance.Metric_Attack("basic", playerUnit.damage);
        
        yield return new WaitForSeconds(2f);
        playerGameObject.transform.position = playerPosition.position;
        if (isDead)
        {
            if (typeEnemy is BossEnemy)
            { 
                SceneManager.LoadScene("EndGame");//end game
            }
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
    
        yield return new WaitForSeconds(1f);
        if (typeEnemy is BossEnemy)
        {
            sfx(attackBoss); 
        }
        else
        {
            sfx(attack);
        }

        playerGameObject.GetComponent<Animator>().SetTrigger("Dmg");
        playerUnit.TakeDamage(enemyUnit.damage);
        bool isDead = playerUnit.isDead();
        //UI_instance.SetPlayerHP(playerUnit.currentHP);
        updateHPPlayer(playerUnit.currentHP);
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
            sfx(Win);
            GameManager.Instance.EXP += enemyData.reward;
            GameManager.Instance.Gold += enemyData.reward;
            // DEBUG_REMOVED: Debug.Log("exp :" + GameManager.Instance.EXP);
            // DEBUG_REMOVED: Debug.Log("gold :" + GameManager.Instance.Gold);
            
            // Registrar métrica de victoria del jugador
            //AnalyticsManager.instance.Metric_Win();
            AnalyticsManager.instance.Metric_Win(1, enemyUnit.unitName);
            Debug.Log("Metric_Win: " + enemyUnit.unitName);
            yield return new WaitForSeconds(2f);
            GameManager.Instance.returnMainIsland(true);

        }
        else if (state == BattleSate.LOSE)
        {
            playerGameObject.GetComponent<Animator>().SetTrigger("Lose");
            enemyGameObject.GetComponent<Animator>().SetTrigger("Win");
            texto.text = "Perdiste";
            sfx(Lose);
            yield return new WaitForSeconds(2f);
            GameManager.Instance.returnMainIsland(false);
            //AnalyticsManager.instance.Metric_Defeat();
            AnalyticsManager.instance.Metric_Defeat(1, enemyUnit.unitName);
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
        
        // Registrar métrica de curación del jugador
        AnalyticsManager.instance.Metric_Heal();
        
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

    public void OnSkillButton()
    {
        if (state != BattleSate.PLAYER || accion == false|| skill==false)
        {
            return;
        }

        accion = false;
        skill = false;
        StartCoroutine(SkillAttack());
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
                dv = true;
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
