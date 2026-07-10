using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/DamageFormulaData")]
public class DamageFormulaData : DefinitionBase
{
    [SerializeField] private DamageFormulaType damageFormulaType;
    [SerializeField] private bool defaultCanCritical = true;
    [SerializeField] private bool defaultIgnoreDefense = false;

    public DamageFormulaType DamageFormulaType => damageFormulaType;
    public bool DefaultCanCritical => defaultCanCritical;
    public bool DefaultIgnoreDefense => defaultIgnoreDefense;
}
