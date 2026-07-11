using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/TargetPriorityData")]
public class TargetPriorityData : DefinitionBase
{
    [SerializeField] private TargetPriorityType targetPriorityType;
    public TargetPriorityType TargetPriorityType => targetPriorityType;
}
