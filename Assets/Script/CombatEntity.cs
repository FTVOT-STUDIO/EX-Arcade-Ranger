using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    [SerializeField] private CombatEntityData entityData;

    [SerializeField] private TeamType teamType;

    public CombatEntityData EntityData => entityData;
    public TeamType TeamType => teamType;

    public JobData JobDefinition => entityData.jobData;
    public JobType JobType => entityData.jobData.JobType;

    public PositionData positionData => entityData.positionData;
    public PositionType positionType => entityData.positionData.PositionType;

    public AttackRangeData attackRangeData => entityData.attackRangeData;
    public AttackRangeType attackRangeType => entityData.attackRangeData.AttackRangeType;

    public SupportRangeData supportRangeData => entityData.supportRangeData;
    public SupportRangeType supportRangeType => entityData.supportRangeData.SupportRangeType;

    public AttributeData attributeData => entityData.attributeData;
    public AttributeType attributeType => entityData.attributeData.AttributeType;
    public AttributeDefenseProfile attributeDefenseProfile => entityData.attributeDefenseProfile;

    public DamageFormulaData damageFormulaData => entityData.damageFormulaData;
    public DamageFormulaType damageFormulaType => entityData.damageFormulaData.DamageFormulaType;

    public AbilityActionData abilityActionData => entityData.abilityActionData;
    public AbilityActionType abilityActionType => entityData.abilityActionData.AbilityActionType;

    public BasicAttackAreaData basicAttackAreaData => entityData.basicAttackAreaData;
    public BasicAttackAreaType basicAttackAreaType => entityData.basicAttackAreaData.BasicAttackAreaType;

    public TargetPriorityData targetPriorityData => entityData.targetPriorityData;
    public TargetPriorityType targetPriorityType => entityData.targetPriorityData.TargetPriorityType;

    public CombatOptionSet combatOptionSet => entityData.combatOptionSet;

    public RuntimeStats Stats { get; private set; }

    public float CurrentHp { get; private set; }
    public float CurrentShield { get; private set; }
    public float CurrentMana { get; private set; }

    public bool IsDead => CurrentHp <= 0f;

    void Awake()
    {
        Init(entityData, entityData is UnitData unitData ? TeamType.Ally : TeamType.Enemy, 1, 1);
    }

    public void Init(CombatEntityData data, TeamType team, int unitLevel, int wave)
    {
        entityData = data;
        teamType = team;

        Stats = new RuntimeStats(entityData.baseStats);

        if (entityData is UnitData unitData)
        {
            StatSet levelBonusStats = unitData.GetLevelBonusStats(unitLevel);
            Stats.AddFlatModifiers(levelBonusStats);
        }

        if (entityData is EnemyData enemyData)
        {
            enemyData.ApplyWaveScaling(Stats, wave);
        }

        CurrentHp = Stats.GetValue(StatType.MaxHp);
        CurrentShield = Stats.GetValue(StatType.MaxShield);
        CurrentMana = 0f;
    }

    public void TakeDamage(float finalDamage)
    {
        if (IsDead || combatOptionSet.GetOptionData(CombatOptionType.IsInvincible) || combatOptionSet.GetOptionData(CombatOptionType.CanTakeDamage))
        {
            return;
        }

        float remainingDamage = finalDamage;

        if (CurrentShield > 0f)
        {
            float shieldDamage = Mathf.Min(CurrentShield, remainingDamage);
            CurrentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        if (remainingDamage > 0f)
        {
            CurrentHp -= remainingDamage;
        }

        if (CurrentHp <= 0f)
        {
            CurrentHp = 0f;
            Dead();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead)
        {
            return;
        }

        float maxHp = Stats.GetValue(StatType.MaxHp);
        CurrentHp = Mathf.Min(CurrentHp + amount, maxHp);
    }

    public void AddShield(float amount)
    {
        if (IsDead)
        {
            return;
        }

        CurrentShield += amount;
    }

    public void AddMana(float amount)
    {
        if (IsDead)
        {
            return;
        }

        float maxMana = Stats.GetValue(StatType.MaxMana);
        CurrentMana = Mathf.Min(CurrentMana + amount, maxMana);
    }

    public void ResetMana()
    {
        CurrentMana = 0f;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
