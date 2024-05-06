using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public void ComenzarPelea(GameObject digimon)
    {
        GameManager.Instance.SetEnemyColosseum(digimon);
    }
}
