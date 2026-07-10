using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefinitionBase : ScriptableObject
{
    [SerializeField] private string dataName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string desc;

    public string DataName => dataName;
    public Sprite Icon => icon;
    public string Desc => desc;
}
