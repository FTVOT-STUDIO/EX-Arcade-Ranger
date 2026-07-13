using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitGrowthData
{
    [SerializeField] private string unitId;
    [SerializeField] private string unitName;
    [SerializeField] private int level = 1;
    [SerializeField] private int currentFragments;
    [SerializeField] private int overflowFragments;
    [SerializeField] private bool unlocked;

    public string UnitId => unitId;
    public string UnitName => unitName;
    public int Level => level;
    public int CurrentFragments => currentFragments;
    public int OverflowFragments => overflowFragments;
    public bool Unlocked => unlocked;

    public UnitGrowthData(UnitData unitData)
    {
        unitId = unitData.entityId;
        unitName = unitData.entityName;
        level = 1;
        currentFragments = 0;
        overflowFragments = 0;
        unlocked = unitData.DefaultUnlocked;
    }

    public void Unlock()
    {
        unlocked = true;
    }

    public void AddFragments(UnitData unitData, int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (level >= unitData.UnitGradeData.MaxLevel)
        {
            overflowFragments += amount;
            return;
        }

        currentFragments += amount;
    }

    public bool CanLevelUp(UnitData unitData, int playerGold, out LevelUpRequirement requirement)
    {
        requirement = null;

        if (!Unlocked)
        {
            return false;
        }

        UnitGradeData gradeData = unitData.UnitGradeData;

        if (level >= gradeData.MaxLevel)
        {
            return false;
        }

        if (!gradeData.TryGetLevelUpRequirement(level, out requirement))
        {
            return false;
        }

        return currentFragments >= requirement.RequiredFragments && playerGold >= requirement.GoldCost;
    }

    public bool TryLevelUp(UnitData unitData, ref int playerGold)
    {
        if (!CanLevelUp(unitData, playerGold, out LevelUpRequirement requirement))
        {
            return false;
        }

        currentFragments -= requirement.RequiredFragments;
        playerGold -= requirement.GoldCost;
        level++;

        if (level >= unitData.UnitGradeData.MaxLevel)
        {
            overflowFragments += currentFragments;
            currentFragments = 0;
        }

        return true;
    }

    public void Validate(UnitData unitData)
    {
        level = Mathf.Clamp(level, 1, unitData.UnitGradeData.MaxLevel);
        currentFragments = Mathf.Max(0, currentFragments);
        overflowFragments = Mathf.Max(0, overflowFragments);

        if (level >= unitData.UnitGradeData.MaxLevel && currentFragments > 0)
        {
            overflowFragments += currentFragments;
            currentFragments = 0;
        }
    }


}
