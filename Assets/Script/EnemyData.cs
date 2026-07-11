using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EX Arcade Ranger/Data/Enemy Data")]
public class EnemyData : CombatEntityData
{
    public bool isElite;
    public bool isBoss;

    public float hpIncreasePerWave = 0.05f;
    public float attackIncreasePerWave = 0.05f;

    public void ApplyWaveScaling(RuntimeStats stats, int wave)
    {
        if (stats == null)
        {
            return;
        }

        float hpBonus = wave * hpIncreasePerWave;
        float attackBonus = wave * attackIncreasePerWave;

        stats.AddPercentModifier(StatType.MaxHp, hpBonus);
        stats.AddPercentModifier(StatType.BaseDamage, attackBonus);
    }
}
