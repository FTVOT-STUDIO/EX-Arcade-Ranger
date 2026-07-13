using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EX Arcade Ranger/Database/Unit Database")]
public class UnitDatabase : ScriptableObject
{
    [SerializeField] private List<UnitData> units = new List<UnitData>();

    private Dictionary<string, UnitData> unitLookup;

    public IReadOnlyList<UnitData> Units => units;

    public void Initialize()
    {
        unitLookup = new Dictionary<string, UnitData>();

        foreach (UnitData unitData in units)
        {
            if (unitLookup.ContainsKey(unitData.entityId))
            {
                Debug.LogError($"중복된 유닛 ID입니다: {unitData.entityId}", unitData);
                continue;
            }
            unitLookup.Add(unitData.entityId, unitData);
        }
    }

    public bool TryGetUnitData(string unitId, out UnitData unitData)
    {
        EnsureInitialized();

        return unitLookup.TryGetValue(unitId, out unitData);
    }

    public UnitData GetUnitData(string unitId)
    {
        EnsureInitialized();

        if (unitLookup.TryGetValue(unitId, out UnitData unitData))
        {
            return unitData;
        }

        Debug.LogError($"UnitData를 찾을 수 없습니다: {unitId}");
        return null;
    }

    private void EnsureInitialized()
    {
        if (unitLookup == null)
        {
            Initialize();
        }
    }


}
