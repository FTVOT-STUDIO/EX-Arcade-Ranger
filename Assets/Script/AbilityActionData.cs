using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/AbilityActionData")]
public class AbilityActionData : DefinitionBase
{
    [SerializeField] private AbilityActionType abilityActionType;
    [SerializeField] private bool resetOnUse = true;
    public AbilityActionType AbilityActionType => abilityActionType;
    public bool ResetOnUse => resetOnUse;

}
