using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public float value;
}

[System.Serializable]
public class StatSet
{
    [SerializeField] private List<StatEntry> stats = new List<StatEntry>();

    public float GetValue(StatType statType)
    {
        foreach (StatEntry stat in stats)
        {
            if (stat.statType == statType)
            {
                return stat.value;
            }
        }
        return 0;
    }

    public void AddValue(StatType statType, float value)
    {
        StatEntry entry = FindEntry(statType);

        if (entry == null)
        {
            StatEntry stat = new StatEntry();
            stat.value = value;
            stat.statType = statType;
            stats.Add(stat);
            return;
        }

        entry.value += value;
    }

    public void AddStats(StatType type)
    {
        StatEntry entry = new StatEntry();
        entry.statType = type;
        entry.value = 0f;
        stats.Add(entry);
    }

    public StatSet Clone()
    {
        StatSet statSet = new StatSet();

        foreach (StatEntry entry in stats)
        {
            statSet.AddValue(entry.statType, entry.value);
        }

        return statSet;
    }

    private StatEntry FindEntry(StatType statType)
    {
        foreach (StatEntry entry in stats)
        {
            if (entry.statType == statType)
            {
                return entry;
            }
        }

        return null;
    }

    public IEnumerable<StatEntry> GetEntries()
    {
        return stats;
    }
}