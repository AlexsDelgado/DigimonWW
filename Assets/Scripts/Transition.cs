using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public AudioClip transition;
    public event Action<AudioClip> sfxTransition;
    public void animacionTransicion()
    {
        
        GameManager.Instance.ColosseumStart();
        
    }

    private void Start()
    {
        //sfxTransition(transition);
        AudioManager.Instance.PlaySound(transition);
    }
}
