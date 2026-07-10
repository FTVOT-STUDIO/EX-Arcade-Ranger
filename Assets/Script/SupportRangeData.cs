using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/SupportRangeData")]
public class SupportRangeData : DefinitionBase
{
    [SerializeField] private SupportRangeType supportRangeType;
    [SerializeField] private List<PositionType> supportablePositions = new List<PositionType>();

    public SupportRangeType SupportRangeType => supportRangeType;
    public IReadOnlyList<PositionType> SupportablePositions => supportablePositions;
}
