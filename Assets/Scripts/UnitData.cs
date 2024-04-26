
using UnityEngine;
[CreateAssetMenu(fileName = "UnitData",menuName = "Unit Data")]
public class UnitData : ScriptableObject
{
    public bool isPlayer;
    public string unitName;
    public int unitLevel;

    public int baseDamage;
    public int baseDef;
    public int baseMaxHp;

    public int damage;
    public int defense;
    public int maxHp;
    
}
