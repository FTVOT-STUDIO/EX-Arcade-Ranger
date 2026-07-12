using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeStats
{
    private readonly Dictionary<StatType, float> baseStats = new();
    private readonly Dictionary<StatType, float> flatModifiers = new();
    private readonly Dictionary<StatType, float> percentModifiers = new();

    public IReadOnlyDictionary<StatType, float> BaseStats => baseStats;
    public IReadOnlyDictionary<StatType, float> FlatModifiers => flatModifiers;
    public IReadOnlyDictionary<StatType, float> PercentModifiers => percentModifiers;

    public RuntimeStats(StatSet statSet)
    {
        foreach (StatType statType in System.Enum.GetValues(typeof(StatType)))
        {
            float baseValue = statSet.GetValue(statType);
            baseStats[statType] = baseValue;
            flatModifiers[statType] = 0;
            percentModifiers[statType] = 0;
        }
    }

    public float GetValue(StatType statType)
    {
        float baseValue = baseStats[statType];
        float flatValue = flatModifiers[statType];
        float percentValue = percentModifiers[statType];

        return (baseValue + flatValue) * (1f + percentValue);
    }

    public void AddFlatModifier(StatType statType, float value)
    {
        flatModifiers[statType] += value;
    }

    public void RemoveFlatModifier(StatType statType, float value)
    {
        flatModifiers[statType] -= value;
    }

    public void AddPercentModifier(StatType statType, float value)
    {
        percentModifiers[statType] += value;
    }

    public void RemovePercentModifier(StatType statType, float value)
    {
        percentModifiers[statType] -= value;
    }

    public void AddFlatModifiers(StatSet statSet)
    {
        foreach (StatEntry entry in statSet.GetEntries())
        {
            AddFlatModifier(entry.statType, entry.value);
        }
    }
}
