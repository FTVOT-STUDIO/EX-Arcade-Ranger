using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    [SerializeField] private int saveVersion = 1;
    [SerializeField] private int gold;
    [SerializeField] private int gem;
    [SerializeField] private List<UnitGrowthData> unitGrowthList = new List<UnitGrowthData>();

    [System.NonSerialized]
    private Dictionary<string, UnitGrowthData> unitProgressLookup;

    public int SaveVersion => saveVersion;
    public int Gold => gold;
    public int Gem => gem;
    public IReadOnlyList<UnitGrowthData> UnitGrowthList => unitGrowthList;

    public void InitializeRuntimeData()
    {
        if (unitGrowthList == null)
        {
            unitGrowthList = new List<UnitGrowthData>();
        }

        unitProgressLookup = new Dictionary<string, UnitGrowthData>();

        foreach (UnitGrowthData unitGrowthData in unitGrowthList)
        {
            if (unitGrowthData == null || string.IsNullOrWhiteSpace(unitGrowthData.UnitId))
            {
                continue;
            }

            if (unitProgressLookup.ContainsKey(unitGrowthData.UnitId))
            {
                Debug.LogWarning($"중복된 유닛 저장 데이터가 있습니다: {unitGrowthData.UnitId}");
                continue;
            }

            unitProgressLookup.Add(unitGrowthData.UnitId, unitGrowthData);
        }
    }

    public void SyncUnitProgress(UnitDatabase unitDatabase)
    {
        EnsureLookup();

        foreach (UnitData unitData in unitDatabase.Units)
        {
            if (unitData == null)
            {
                continue;
            }

            if (unitProgressLookup.TryGetValue(unitData.entityId, out UnitGrowthData unitGrowthData))
            {
                unitGrowthData.Validate(unitData);
                continue;
            }

            UnitGrowthData newProgressData = new UnitGrowthData(unitData);

            unitGrowthList.Add(newProgressData);

            unitProgressLookup.Add(unitData.entityId, newProgressData);
        }
    }

    public UnitGrowthData GetOrCreateUnitGrowth(UnitData unitData)
    {
        EnsureLookup();

        if (unitProgressLookup.TryGetValue(unitData.entityId, out UnitGrowthData unitGrowthData))
        {
            unitGrowthData.Validate(unitData);
            return unitGrowthData;
        }

        unitGrowthData = new UnitGrowthData(unitData);

        unitGrowthList.Add(unitGrowthData);
        unitProgressLookup.Add(unitData.entityId, unitGrowthData);

        return unitGrowthData;
    }

    public bool TryGetUnitProgress(string unitId, out UnitGrowthData unitGrowthData)
    {
        EnsureLookup();

        return unitProgressLookup.TryGetValue(unitId, out unitGrowthData);
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        gold += amount;
    }

    public bool TrySpendGold(int amount)
    {
        if (amount < 0 || gold < amount)
        {
            return false;
        }
        gold -= amount;
        return true;
    }

    public void AddGem(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        gem += amount;
    }

    public bool TrySpendGem(int amount)
    {
        if (amount < 0 || gem < amount)
        {
            return false;
        }
        gem -= amount;
        return true;
    }

    public void AddUnitFragments(UnitData unitData, int amount)
    {
        UnitGrowthData unitGrowthData = GetOrCreateUnitGrowth(unitData);
        unitGrowthData.AddFragments(unitData, amount);
    }

    public bool TryLevelUpUnit(UnitData unitData)
    {
        UnitGrowthData unitGrowthData = GetOrCreateUnitGrowth(unitData);
        return unitGrowthData.TryLevelUp(unitData, ref gold);
    }

    public void UnlockUnit(UnitData unitData)
    {
        UnitGrowthData unitGrowthData = GetOrCreateUnitGrowth(unitData);
        unitGrowthData.Unlock();
    }

    private void EnsureLookup()
    {
        if (unitProgressLookup == null)
        {
            InitializeRuntimeData();
        }
    }
}
