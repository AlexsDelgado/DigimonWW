    using Unity.Services.Analytics;
using System.Collections.Generic;

public class DefeatEvent
{
    public string eventName = "defeat";
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    
    public DefeatEvent(int defeatCount, string battleType)
    {
        parameters["defeat_count"] = defeatCount;
        parameters["battle_type"] = battleType;
    }
}

public class AttackEvent
{
    public string eventName = "attack";
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    
    public AttackEvent(string attackType, int damage)
    {
        parameters["attack_type"] = attackType;
        parameters["damage"] = damage;
    }
}

public class WinEvent
{
    public string eventName = "win";
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    
    public WinEvent(int winCount, string battleType)
    {
        parameters["win_count"] = winCount;
        parameters["battle_type"] = battleType;
    }
}

public class SkillEvent
{
    public string eventName = "skill";
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    
    public SkillEvent(string skillName = "Bear Fist", int skillCount = 1)
    {
        parameters["skill_name"] = skillName;
        parameters["skill_count"] = skillCount;
    }
}

public class HealEvent
{
    public string eventName = "heal";
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    
    public HealEvent(int healAmount = 0, int healCount = 1)
    {
        parameters["heal_amount"] = healAmount;
        parameters["heal_count"] = healCount;
    }
}

