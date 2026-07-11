using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/CombatOptionData")]
public class CombatOptionData : DefinitionBase
{
    [SerializeField] private CombatOptionType combatOptionType;
    public CombatOptionType CombatOptionType => combatOptionType;
}
