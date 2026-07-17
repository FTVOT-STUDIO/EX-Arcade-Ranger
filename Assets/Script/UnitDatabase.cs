using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EX Arcade Ranger/Database/Unit Database")]
public class UnitDatabase : DataBaseCore<UnitData>
{
    public bool TryGetUnitData(string unitId, out UnitData unitData)
    {
        return TryGetData(unitId, out unitData);
    }

    public UnitData GetUnitData(string unitId)
    {
        return GetData(unitId);
    }
}
