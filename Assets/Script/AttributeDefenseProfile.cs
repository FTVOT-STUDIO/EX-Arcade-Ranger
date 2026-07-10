using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeDefenseProfile
{
    [SerializeField] private List<AttributeData> resistances = new List<AttributeData>();
    [SerializeField] private List<AttributeData> weaknesses = new List<AttributeData>();

    [Range(0, 1)]
    [SerializeField] private float resistanceReductionRate = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float weaknessBonusRate = 0.5f;

    public IReadOnlyList<AttributeData> Resistance => resistances;
    public IReadOnlyList<AttributeData> Weakness => weaknesses;

    public float GetAttributeMultiplier(AttributeData attributeData, float resistanceIgnore)
    {
        if (ContainsAttribute(weaknesses, attributeData))
        {
            return 1f + weaknessBonusRate;
        }

        if (ContainsAttribute(resistances, attributeData))
        {
            float reducedMultiplier = 1f - resistanceReductionRate;
            return Mathf.Lerp(reducedMultiplier, 1f, resistanceIgnore);
        }

        return 1f;
    }

    private bool ContainsAttribute(List<AttributeData> list, AttributeData target)
    {
        foreach (AttributeData element in list)
        {
            if (element == null)
            {
                continue;
            }

            if (element.AttributeType == target.AttributeType)
            {
                return true;
            }
        }

        return false;
    }
}
