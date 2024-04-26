using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animaciones;

    [SerializeField] public UnitData unitData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Animation(String animation)
    {
        animaciones.SetTrigger(animation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
