using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/AttackRangeData")]
public class AttackRangeData : DefinitionBase
{
    [SerializeField] private AttackRangeType attackRangeType;
    [SerializeField] private List<PositionType> attackablePositions = new List<PositionType>();

    public AttackRangeType AttackRangeType => attackRangeType;
    public IReadOnlyList<PositionType> AttackablePositions => attackablePositions;
}
