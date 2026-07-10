using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/PositionData")]
public class PositionData : DefinitionBase
{
    [SerializeField] private PositionType positionType;
    public PositionType PositionType => positionType;
}
