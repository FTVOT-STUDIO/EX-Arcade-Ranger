public enum TeamType
{
    Ally, Enemy
}

public enum PositionType
{
    Front, Middle, Back
}

public enum JobType
{
    Tanker, Berserker, Ranger, Assassin, Buffer, Healer, Magician
}

public enum AttackRangeType
{
    None, Melee, Ranged, LongRange
}

public enum SupportRangeType
{
    None, FrontOnly, MiddleOnly, BackOnly, AllAllies
}

public enum AttributeType
{
    Fixed, Fire, Water, Nature, Electro, Ice, Wind, Poison, Light, Darkness
}

public enum UnitGrade
{
    Common, Rare, Epic, Legendary, Apostle, Sephira
}

public enum ActionType
{
    Damage, Heal, Buff, Debuff
}

public enum DamageFormulaType
{
    BaseDamage, TargetMaxHp, TargetCurrentHp, TargetMissingHp, SelfMaxHp, Fixed
}

public enum TargetPriorityType
{
    LowestHp, HighestHp, LowestHpRate, HighestHpRate, HighestDefense, HighestBaseDamage, Random
}

public enum BasicAttackAreaType
{
    Single, Multi
}

public enum AbilityActionType
{
    None, CooldownOnly, ManaOnly
}

public enum CombatOptionType
{
    CanBasicAttack, CanBeTargeted, CanReceiveHeal, CanReceiveBuff, CanReceiveDebuff, UseTargetOverride, CanTakeDamage, IsInvincible
}

public enum StatusType
{
    Stun, Silence, Charm, Taunt, Poisoning
}