using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnitSaveManager : MonoBehaviour
{
    private const string SaveFileName = "UnitSaveData.json";

    private UnitSaveDataList saveDataList = new();
    private readonly Dictionary<string, UnitSaveData> saveDataById = new();

    private string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    void Awake()
    {
        Load();
    }

    public UnitSaveData GetOrCreateUnitSaveData(UnitData unitData)
    {
        if (unitData == null)
        {
            Debug.LogWarning("UnitData가 null입니다.");
            return null;
        }

        if (string.IsNullOrEmpty(unitData.entityId))
        {
            Debug.LogWarning($"{unitData.name}의 entityId가 비어 있습니다.");
            return null;
        }

        if (saveDataById.TryGetValue(unitData.entityId, out UnitSaveData saveData))
        {
            return saveData;
        }

        UnitSaveData newSaveData = new UnitSaveData
        {
            unitId = unitData.entityId,
            currentLevel = 1,
            isUnlocked = unitData.defaultUnlocked,
            duplicateShardCount = 0
        };

        saveDataList.units.Add(newSaveData);
        saveDataById[newSaveData.unitId] = newSaveData;

        Save();

        return newSaveData;
    }

    public bool IsUnlocked(UnitData unitData)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);
        return saveData != null && saveData.isUnlocked;
    }

    public int GetCurrentLevel(UnitData unitData)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);
        return saveData != null ? saveData.currentLevel : 1;
    }

    public int GetDuplicateShardCount(UnitData unitData)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);
        return saveData != null ? saveData.duplicateShardCount : 0;
    }

    public void UnlockUnit(UnitData unitData)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);

        if (saveData == null)
        {
            return;
        }

        saveData.isUnlocked = true;
        Save();
    }

    public void AddDuplicateShard(UnitData unitData, int amount)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);

        if (saveData == null)
        {
            return;
        }

        saveData.duplicateShardCount += amount;

        if (saveData.duplicateShardCount < 0)
        {
            saveData.duplicateShardCount = 0;
        }

        Save();
    }

    public bool TryLevelUp(UnitData unitData, ref int playerGold)
    {
        UnitSaveData saveData = GetOrCreateUnitSaveData(unitData);

        if (saveData == null)
        {
            return false;
        }

        if (!saveData.isUnlocked)
        {
            Debug.Log($"{unitData.entityName} 유닛은 잠금 해제되지 않았습니다.");
            return false;
        }

        if (saveData.currentLevel >= unitData.maxLevel)
        {
            Debug.Log($"{unitData.entityName} 유닛은 이미 최대 레벨입니다.");
            return false;
        }

        int levelUpCost = unitData.GetLevelUpCost(saveData.currentLevel);

        if (playerGold < levelUpCost)
        {
            Debug.Log("골드가 부족합니다.");
            return false;
        }

        playerGold -= levelUpCost;
        saveData.currentLevel++;

        Save();

        return true;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(saveDataList, true);

        string directoryPath = Path.GetDirectoryName(SavePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(SavePath, json);

        Debug.Log($"유닛 저장 완료: {SavePath}");
    }

    public void Load()
    {
        saveDataList = new UnitSaveDataList();
        saveDataById.Clear();

        if (!File.Exists(SavePath))
        {
            Save();
            return;
        }

        string json = File.ReadAllText(SavePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            Save();
            return;
        }

        UnitSaveDataList loadedData = JsonUtility.FromJson<UnitSaveDataList>(json);

        if (loadedData == null || loadedData.units == null)
        {
            Save();
            return;
        }

        saveDataList = loadedData;

        foreach (UnitSaveData unitSaveData in saveDataList.units)
        {
            if (unitSaveData == null)
            {
                continue;
            }

            if (string.IsNullOrEmpty(unitSaveData.unitId))
            {
                continue;
            }

            if (saveDataById.ContainsKey(unitSaveData.unitId))
            {
                Debug.LogWarning($"중복된 unitId 저장 데이터가 있습니다: {unitSaveData.unitId}");
                continue;
            }

            saveDataById.Add(unitSaveData.unitId, unitSaveData);
        }

        Debug.Log($"유닛 저장 데이터 로드 완료: {SavePath}");
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }

        saveDataList = new UnitSaveDataList();
        saveDataById.Clear();

        Save();

        Debug.Log("유닛 저장 데이터가 초기화되었습니다.");
    }

    public string GetSavePath()
    {
        return SavePath;
    }
}
