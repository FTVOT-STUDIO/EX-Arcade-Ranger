using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/DefinitionDataBase")]
public class DefinitionDataBase : ScriptableObject
{
    [SerializeField] private List<StatData> statDatas = new List<StatData>();
    [SerializeField] private List<AttributeData> attributeDatas = new List<AttributeData>();
    [SerializeField] private List<JobData> jobDatas = new List<JobData>();
    [SerializeField] private List<AttackRangeData> attackRangeDatas = new List<AttackRangeData>();
    [SerializeField] private List<SupportRangeData> supportRangeDatas = new List<SupportRangeData>();
    [SerializeField] private List<DamageFormulaData> damageFormulaDatas = new List<DamageFormulaData>();
    [SerializeField] private List<UnitGradeData> unitGrades = new List<UnitGradeData>();

    public StatData GetStatData(StatType statType)
    {
        return statDatas.Find(x => x.StatType == statType);
    }

    public AttributeData GetAttributeData(AttributeType attributeType)
    {
        return attributeDatas.Find(x => x.AttributeType == attributeType);
    }

    public JobData GetJobData(JobType jobType)
    {
        return jobDatas.Find(x => x.JobType == jobType);
    }

    public AttackRangeData GetAttackRangeData(AttackRangeType attackRangeType)
    {
        return attackRangeDatas.Find(x => x.AttackRangeType == attackRangeType);
    }

    public SupportRangeData GetSupportRangeData(SupportRangeType supportRangeType)
    {
        return supportRangeDatas.Find(x => x.SupportRangeType == supportRangeType);
    }

    public DamageFormulaData GetDamageFormulaData(DamageFormulaType damageFormulaType)
    {
        return damageFormulaDatas.Find(x => x.DamageFormulaType == damageFormulaType);
    }

    public UnitGradeData GetUnitGradeData(UnitGrade gradeType)
    {
        return unitGrades.Find(x => x.GradeType == gradeType);
    }
}
