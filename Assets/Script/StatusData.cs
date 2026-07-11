using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/StatusData")]
public class StatusData : DefinitionBase
{
    [SerializeField] private StatusType statusType;
    [SerializeField] private GameObject prefab;
    public StatusType StatusType => statusType;
    public GameObject Prefab => prefab;
}
