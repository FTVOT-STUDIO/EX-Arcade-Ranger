using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSaveData
{
    public string unitId;
    public int currentLevel;
    public bool isUnlocked;
    public int duplicateShardCount;
}

[System.Serializable]
public class UnitSaveDataList
{
    public List<UnitSaveData> units = new List<UnitSaveData>();
}
