using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/UnitGradeData")]
public class UnitGradeData : DefinitionBase
{
    [SerializeField] private UnitGrade gradeType;
    [SerializeField] private int defalutBuyPrice;

    public UnitGrade GradeType => gradeType;
    public int DefalutBuyPrice => defalutBuyPrice;
}
