using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New WeaponData",menuName = "ScriptableObject/WeaponData")]
public class ScriptableWeaponData : ScriptableObject
{
    
    [Header("총이름")] // Handgun(권총) - Repeater(연발총) - Shotgun - SniperRifle(저격총)
    [SerializeField] string gunName;
    public string GunName { get { return gunName; } }

    [Header("총 이미지")]
    [SerializeField] Sprite gunImg;
    public Sprite GunImg { get { return gunImg; } }
    
    [Header("사거리")] // 사거리 15 - 25 - 20 - 40
    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }

    [Header("날라가는 속도")] // 속도 30 - 40 - 50 - 70
    [SerializeField] float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [Header("공격력")] // 공격력 10 - 15 - (20~10) - 30
    [SerializeField] float attackDamage;
    public float AttackDamage { get { return attackDamage; } }

    [Header("기본 총알 개수")] // 기본 제공 총알 수 15 - 20 - 30 - 10
    [SerializeField] float numberOfBullet;
    public float NumberOfBullet { get { return numberOfBullet; } }

    [Header("연사속도")] // 연사속도 1.5 - 0.5 - 1 - 2.5
    [SerializeField] float attackInterval;
    public float AttackInterval { get { return attackInterval; } }

  
}
