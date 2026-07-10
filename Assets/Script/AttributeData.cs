using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/AttributeData")]
public class AttributeData : DefinitionBase
{
    [SerializeField] private AttributeType attributeType;

    public AttributeType AttributeType => attributeType;
}
