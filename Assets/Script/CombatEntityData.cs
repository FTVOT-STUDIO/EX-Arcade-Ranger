using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntityData : ScriptableObject
{
    public string entityId;
    public string entityName;

    public string desc;
    public string lore;

    public Sprite portrait;
    public Sprite icon;

    public JobData jobData;
    public PositionData positionData;
    public AttackRangeData attackRangeData;
    public SupportRangeData supportRangeData;
    public AttributeData attributeData;
    public DamageFormulaData damageFormulaData;

    public StatSet baseStats;

    public AttributeDefenseProfile attributeDefenseProfile;

    public GameObject prefab;


    // - 타겟 우선순위 데이터 추가
    //   ex: 체력 낮은 대상, 체력 높은 대상, 무작위 대상, 공격력 높은 대상
    //
    // - 기본 공격 범위 데이터 추가
    //   ex: 단일 공격, 포지션 전체 공격, 전체 공격, 무작위 N명 공격
    //
    // - 강제 타겟 적용 여부 추가
    //   ex: 도발, 페트라 목표 지정 명령에 영향을 받을지 여부
    //
    // - 행동 자원 방식 추가
    //   ex: 마나 사용, 쿨타임 사용, 마나+쿨타임 사용, 자원 없음
    //
    // - 기본 공격 가능 여부 추가
    //   ex: 순수 힐러/버퍼는 기본 공격 불가
    //
    // - 타겟 지정 가능 여부 추가
    //   ex: 잠입 상태, 무적 상태, 더미 오브젝트
}
