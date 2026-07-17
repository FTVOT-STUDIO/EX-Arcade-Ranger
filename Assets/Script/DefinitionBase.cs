using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefinitionBase : ScriptableObject, IDataId
{
    [SerializeField] private string dataName;
    [SerializeField] private string dataId;
    [SerializeField] private Sprite icon;
    [SerializeField] private string desc;

    public string DataName => dataName;
    public string DataId => dataId;
    public Sprite Icon => icon;
    public string Desc => desc;


}
