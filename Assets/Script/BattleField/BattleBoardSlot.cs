using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoardSlot : MonoBehaviour
{
    [SerializeField] private int sortingOrder;
    [SerializeField] private CombatEntity placedEntity;
    public int SortingOrder => sortingOrder;
    public CombatEntity PlacedEntity => placedEntity;
    public bool IsEntityPlaced => placedEntity != null;

}
