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
    None, FrontOnly, MiddleOnly, BackOnly, FrontMiddle, AllAllies
}

public enum AttributeType
{
    Fixed, Fire, Water, Natura, Electro, Ice, Wind, Poison, Light, Darkness
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
    DamagePower, TargetMaxHp, TargetCurrentHp, TargetMissingHp, SelfMaxHp, Fixed
}