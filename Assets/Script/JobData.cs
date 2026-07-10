using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/JobData")]
public class JobData : DefinitionBase
{
    [SerializeField] private JobType jobType;
    public JobType JobType => JobType;

}
