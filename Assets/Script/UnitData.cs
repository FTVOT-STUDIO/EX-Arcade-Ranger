using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatIncrease
{
    [SerializeField] private StatType statType;
    [SerializeField] private float increaseValue;

    public StatType StatType => statType;
    public float IncreaseValue => increaseValue;
}

[System.Serializable]
public class LevelStatIncrease
{
    [HideInInspector] public string levelLabel;

    [SerializeField] private List<StatIncrease> statIncreases = new List<StatIncrease>();
    public IReadOnlyList<StatIncrease> StatIncreases => statIncreases;
}

[CreateAssetMenu(menuName = "EX Arcade Ranger/Data/Unit Data")]
public class UnitData : CombatEntityData
{
    [SerializeField] private UnitGradeData unitGradeData;
    [SerializeField] private bool defaultUnlocked;
    [SerializeField] private List<LevelStatIncrease> levelStatIncreases = new List<LevelStatIncrease>();

    public UnitGradeData UnitGradeData => unitGradeData;
    public bool DefaultUnlocked => defaultUnlocked;
    public IReadOnlyList<LevelStatIncrease> LevelStatIncreases => levelStatIncreases;

    //특정 레벨에 도달했을 때 증가하는 능력치 데이터를 가져오는 함수
    public bool TryGetLevelStatIncrease(int targetLevel, out LevelStatIncrease levelStatIncrease)
    {
        if (targetLevel <= 1 || targetLevel > unitGradeData.MaxLevel)
        {
            levelStatIncrease = null;
            return false;
        }

        int index = targetLevel - 2;

        levelStatIncrease = levelStatIncreases[index];
        return true;
    }

    //지정한 레벨까지의 증가 능력치를 모두 누적하여 최종 능력치를 계산하는 함수
    public StatSet GetStatsAtLevel(int level)
    {
        StatSet resultStats = baseStats.Clone();

        for (int targetLevel = 2; targetLevel <= level; targetLevel++)
        {
            if (!TryGetLevelStatIncrease(targetLevel, out LevelStatIncrease levelIncrease))
            {
                continue;
            }

            foreach (StatIncrease statIncrease in levelIncrease.StatIncreases)
            {
                resultStats.AddValue(statIncrease.StatType, statIncrease.IncreaseValue);
            }
        }

        return resultStats;
    }

    [ContextMenu("레벨 테이블 생성")]
    private void CreateLevelUpStatTable()
    {
        levelStatIncreases.Clear();

        for (int i = 2; i <= unitGradeData.MaxLevel; i++)
        {
            LevelStatIncrease levelStat = new LevelStatIncrease();
            levelStat.levelLabel = "Lv " + i;
            levelStatIncreases.Add(levelStat);
        }
    }
}