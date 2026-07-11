using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EX Arcade Ranger/Data/Unit Data")]
public class UnitData : CombatEntityData
{
    public UnitGradeData unitGradeData;

    public int maxLevel = 15;
    public bool defaultUnlocked;

    public List<UnitLevelInfo> levelTable = new List<UnitLevelInfo>();

    public UnitLevelInfo GetLevelInfo(int level)
    {
        foreach (UnitLevelInfo levelInfo in levelTable)
        {
            if (levelInfo.level == level)
            {
                return levelInfo;
            }
        }

        return null;
    }

    public StatSet GetLevelBonusStats(int level)
    {
        UnitLevelInfo levelInfo = GetLevelInfo(level);

        return levelInfo.bonusStats;
    }

    public int GetLevelUpCost(int currentLevel)
    {
        UnitLevelInfo levelInfo = GetLevelInfo(currentLevel);

        return levelInfo.levelUpCost;
    }

}

[System.Serializable]
public class UnitLevelInfo
{
    public int level;
    public int levelUpCost;
    public StatSet bonusStats;
}
