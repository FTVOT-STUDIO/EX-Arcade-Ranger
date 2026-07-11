using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/BasicAttackAreaData")]
public class BasicAttackAreaData : DefinitionBase
{
    [SerializeField] private BasicAttackAreaType basicAttackAreaType;
    public BasicAttackAreaType BasicAttackAreaType => basicAttackAreaType;
}
