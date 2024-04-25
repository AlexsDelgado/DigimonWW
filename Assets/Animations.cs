using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Animations : MonoBehaviour
{
    public GameObject bearmon;
    public Animator bearmonAnimator;
    public Transform attackPosition;
    public Vector3 ogPosition;
     
   
    public GameObject enemy;
    public Animator enemyAnimator;
    public void attack()
    {
        //se toma el transform og
        ogPosition = this.transform.position;
        
        //se toma el spot de ataque y se mueve a bearmon
        //Vector3 posicion = new Vector3(attackPosition.position.x, attackPosition.position.y, attackPosition.position.z);
        Vector3 posicion = new Vector3(attackPosition.position.x, attackPosition.position.y, attackPosition.position.z);
        //bearmon.transform.position = new Vector3(-0.5f,0,1);
        bearmon.transform.position = posicion;
        
        //animacion ataque
        bearmonAnimator.SetTrigger("Attack");
       
    }

    public void terminaAttack()
    {
        //Vector3 posicionOG = new Vector3(bearmon.transform.position.x, bearmon.transform.position.y, bearmon.transform.position.z);
        enemyAnimator.SetTrigger("Dmg");
        bearmon.transform.position = ogPosition;
    }
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Animation(String animation,GameObject actor)
    {
        actor.GetComponent<Animator>().SetTrigger(animation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
