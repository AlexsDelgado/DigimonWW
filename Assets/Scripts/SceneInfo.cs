using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo",menuName = "MainLandScene Info")]
public class SceneInfo : ScriptableObject
{

    //public bool isNextScene = false;
    public string enemyName = "defaultValue";

    public GameObject prefab = null;
    [SerializeField] public UnitData unitSO;




}
