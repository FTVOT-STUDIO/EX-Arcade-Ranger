using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatOptionEntry
{
    public CombatOptionData optionData;
    public bool isEnabled = true;
}

public class CombatOptionSet
{
    public List<CombatOptionEntry> optionEntries = new List<CombatOptionEntry>();

    public bool IsEnabled(CombatOptionType optionType)
    {
        foreach (CombatOptionEntry entry in optionEntries)
        {
            if (entry == null || entry.optionData == null)
            {
                continue;
            }

            if (entry.optionData.CombatOptionType == optionType)
            {
                return entry.isEnabled;
            }
        }

        return false;
    }

    public CombatOptionData GetOptionData(CombatOptionType optionType)
    {
        foreach (CombatOptionEntry entry in optionEntries)
        {
            if (entry == null || entry.optionData == null)
            {
                continue;
            }

            if (entry.optionData.CombatOptionType == optionType)
            {
                return entry.optionData;
            }
        }

        return null;
    }

    public IReadOnlyList<CombatOptionEntry> GetEntries()
    {
        return optionEntries;
    }
}
