using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/StatData")]
public class StatData : DefinitionBase
{
    [SerializeField] private StatType statType;
    [SerializeField] private bool showPercent;
    [SerializeField] private string suffix;

    public StatType StatType => statType;
    public bool ShowPercent => showPercent;
    public string Suffix => suffix;

}
