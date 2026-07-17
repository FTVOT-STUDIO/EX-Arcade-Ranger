using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntityData : DefinitionBase
{
    public string lore;
    public Sprite portrait;
    public JobData jobData;
    public PositionData positionData;
    public AttackRangeData attackRangeData;
    public SupportRangeData supportRangeData;
    public AttributeData attributeData;
    public DamageFormulaData damageFormulaData;

    public AbilityActionData abilityActionData;
    public BasicAttackAreaData basicAttackAreaData;
    public TargetPriorityData targetPriorityData;

    public StatSet baseStats;
    public CombatOptionSet combatOptionSet;

    public AttributeDefenseProfile attributeDefenseProfile;

    public GameObject prefab;

    [ContextMenu("기초 스탯 생성")]
    private void SetStat()
    {
        StatType[] statTypes = (StatType[])Enum.GetValues(typeof(StatType));
        for (int i = 0; i < Enum.GetValues(typeof(StatType)).Length; i++)
        {
            baseStats.AddStats(statTypes[i]);
        }
    }
}
