using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpRequirement
{
    [SerializeField] private int requiredFragments;
    [SerializeField] private int goldCost;

    public int RequiredFragments => requiredFragments;
    public int GoldCost => goldCost;
}

[CreateAssetMenu(menuName = "Ex Arcade Ranger/Definition/UnitGradeData")]
public class UnitGradeData : DefinitionBase
{
    [SerializeField] private UnitGrade gradeType;
    [SerializeField] private int battleShopPrice;
    [SerializeField] private int lobbyShopPrice;
    [SerializeField] private int maxLevel;
    [SerializeField] private List<LevelUpRequirement> levelUpRequirements = new List<LevelUpRequirement>();

    public UnitGrade GradeType => gradeType;
    public int BattleShopPrice => battleShopPrice;
    public int LobbyShopPrice => lobbyShopPrice;

    public IReadOnlyList<LevelUpRequirement> LevelUpRequirements => levelUpRequirements;

    public int MaxLevel => maxLevel;

    //현재 레벨에서 다음 레벨로 올라갈 때 필요한 조각 갯수 와 골드 비용을 가져오는 함수
    public bool TryGetLevelUpRequirement(int currentLevel, out LevelUpRequirement requirement)
    {
        if (currentLevel < 1 || currentLevel >= MaxLevel)
        {
            requirement = null;
            return false;
        }

        int index = currentLevel - 1;
        requirement = levelUpRequirements[index];

        return true;
    }
}
