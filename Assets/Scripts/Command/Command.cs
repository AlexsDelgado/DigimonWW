using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICommand
{
    void Execute();
}


public class Command : MonoBehaviour
{

}

public class AttackCommand : ICommand
{
    // public int damage;
    // private Unit enemy;
    public GameObject player;
    public Animator playerAnimator;
    public Transform position;

    public AttackCommand(int dmg,Unit enemyUnit ,GameObject playerGameObject,Animator animatorPlayer,Transform attackPosition)
    {
        // damage = dmg;
        // enemy = enemyUnit;
        player = playerGameObject;
        playerAnimator = animatorPlayer;
        position = attackPosition;
    }
    
    
    public void Execute()
    {
        player.transform.position = position.position;
        playerAnimator.Play("attack1");
        // enemy.TakeDamage(damage);
    }
}


public class SkillAttack : ICommand
{
    // public int damage;
    // public Unit enemy;
    public GameObject player;
    public Animator playerAnimator;
    public Transform position;

    public SkillAttack(int dmg, Unit enemy,GameObject playerGameObject,Animator animatorPlayer,Transform attackPosition)
    {
     
        player = playerGameObject;
        playerAnimator = animatorPlayer;
        position = attackPosition;
    }
    
    
    public void Execute()
    {
        player.transform.position = position.position;
        playerAnimator.Play("attack2");
        // enemy.TakeDamage(damage*2);
    }
}


// private void ProcessCommand(ICommand command)
// {
//     command.Execute();
// }
