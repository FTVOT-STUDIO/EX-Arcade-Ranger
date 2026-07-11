using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleBoard : MonoBehaviour
{
    private readonly List<CombatEntity> allies = new();
    private readonly List<CombatEntity> enemies = new();

    public void Register(CombatEntity entity)
    {
        if (entity.TeamType == TeamType.Ally)
        {
            allies.Add(entity);
        }
        else
        {
            enemies.Add(entity);
        }
    }

    public void Unregister(CombatEntity entity)
    {
        allies.Remove(entity);
        enemies.Remove(entity);
    }

    public List<CombatEntity> GetEntities(TeamType teamType)
    {
        return teamType == TeamType.Ally ? allies : enemies;
    }

    public List<CombatEntity> GetAliveEntities(TeamType teamType)
    {
        return GetEntities(teamType).Where(entity => !entity.IsDead).ToList();
    }
}
