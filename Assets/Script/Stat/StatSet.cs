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

    public void AddStats(StatType type)
    {
        StatEntry entry = new StatEntry();
        entry.statType = type;
        entry.value = 0f;
        stats.Add(entry);
    }

    public IEnumerable<StatEntry> GetEntries()
    {
        return stats;
    }
}