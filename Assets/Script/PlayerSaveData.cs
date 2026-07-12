using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    [SerializeField] private int saveVersion = 1;
    [SerializeField] private int gold;
    [SerializeField] private int gem;
    [SerializeField] private List<UnitProgressData> unitProgressList = new List<UnitProgressData>();

    [System.NonSerialized]
    private Dictionary<string, UnitProgressData> unitProgressLookup;

    public int SaveVersion => saveVersion;
    public int Gold => gold;
    public int Gem => gem;
    public IReadOnlyList<UnitProgressData> UnitProgressList => unitProgressList;

    public void InitializeRuntimeData()
    {
        if (unitProgressList == null)
        {
            unitProgressList = new List<UnitProgressData>();
        }

        unitProgressLookup = new Dictionary<string, UnitProgressData>();

        foreach (UnitProgressData progressData in unitProgressList)
        {
            if (progressData == null || string.IsNullOrWhiteSpace(progressData.UnitId))
            {
                continue;
            }

            if (unitProgressLookup.ContainsKey(progressData.UnitId))
            {
                Debug.LogWarning($"중복된 유닛 저장 데이터가 있습니다: {progressData.UnitId}");
                continue;
            }

            unitProgressLookup.Add(progressData.UnitId, progressData);
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

            if (unitProgressLookup.TryGetValue(unitData.entityId, out UnitProgressData progressData))
            {
                progressData.Validate(unitData);
                continue;
            }

            UnitProgressData newProgressData = new UnitProgressData(unitData);

            unitProgressList.Add(newProgressData);

            unitProgressLookup.Add(unitData.entityId, newProgressData);
        }
    }

    public UnitProgressData GetOrCreateUnitProgress(UnitData unitData)
    {
        EnsureLookup();

        if (unitProgressLookup.TryGetValue(unitData.entityId, out UnitProgressData progressData))
        {
            progressData.Validate(unitData);
            return progressData;
        }

        progressData = new UnitProgressData(unitData);

        unitProgressList.Add(progressData);
        unitProgressLookup.Add(unitData.entityId, progressData);

        return progressData;
    }

    public bool TryGetUnitProgress(string unitId, out UnitProgressData progressData)
    {
        EnsureLookup();

        return unitProgressLookup.TryGetValue(unitId, out progressData);
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
        UnitProgressData progressData = GetOrCreateUnitProgress(unitData);
        progressData.AddFragments(unitData, amount);
    }

    public bool TryLevelUpUnit(UnitData unitData)
    {
        UnitProgressData progressData = GetOrCreateUnitProgress(unitData);
        return progressData.TryLevelUp(unitData, ref gold);
    }

    public void UnlockUnit(UnitData unitData)
    {
        UnitProgressData progressData = GetOrCreateUnitProgress(unitData);
        progressData.Unlock();
    }

    private void EnsureLookup()
    {
        if (unitProgressLookup == null)
        {
            InitializeRuntimeData();
        }
    }
}
